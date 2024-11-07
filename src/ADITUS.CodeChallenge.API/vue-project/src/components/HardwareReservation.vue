<template>
  <div class="reservation">
    <h1>Hardware reservieren</h1>
    <div class="reservation-form">
      <form @submit.prevent="submitReservation">
        <div class="hardware-data">
          <!--<div class="hardware-name">
            ausgewählten Hardware-Components: {{ checkedNames }}
          </div>-->
          <p class="warning"><strong>Hinweis! Die Reservierung soll mind. 4 Wochen der Event passieren!</strong></p><br />
          <div v-for="(hardware, index) in components" :key="hardware.id">
            <input type="checkbox"
                   :id="hardware.id"
                   :value="hardware.name"
                   v-model="checkedNames" />
            <label :for="hardware.id">
              <strong>{{ hardware.name }}</strong>
            </label>
            <p>
              Zur Verfügung: {{ hardware.amount }}
            </p>
            <div>
              <button @click.prevent="decrementCount(index)">
                -
              </button>
              <span>
                Anzahl: {{ counts[index] }}
              </span>
              <button @click.prevent="incrementCount(index)">
                +
              </button>
            </div>
          </div>
        </div>
          <label for="eventSelect">Bitte wählen Sie ein Event aus: </label>
          <select v-model="selectedEventId" id="eventSelect" required>
            <option v-for="event in events" :key="event.id" :value="event.id">
              {{ event.name }} ({{ event.startDate }} - {{ event.endDate }})
            </option>
          </select>
          <div class="button-send">
            <button type="submit">Reservar</button>
          </div>
      </form>
      <div v-if="reservationMessage" class="message">
        {{ reservationMessage }}
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
  import { ref, onMounted } from 'vue';
  import type { Hardware, Event, ReservationResponse } from '../types/reservationInterfaces.ts';

  const checkedNames = ref<string[]>([]);
  const components = ref<Hardware[]>([]);// Als Hardware-Array definieren
  const counts = ref<number[]>([]);
  const events = ref<Event[]>([]); 
  const selectedEventId = ref<string>(''); // speichert die ID des ausgewählten Ereignisses
  const reservationMessage = ref<string | null>(null); // Zum Speichern der Erfolgs- oder Fehlermeldung
  const errorMessage = ref('');

  onMounted(async () => {
    try {
      // Api Aufruf
      const [hardwareResponse, eventResponse] = await Promise.all([
        fetch('/reservation'),       // EventHardwareController
        fetch('/eventhardware')      // HardwareController
      ]);

      // Response prüfen
      if (!hardwareResponse.ok) {
        throw new Error(`HTTP error! status: ${hardwareResponse.status}`);
      }
      const hardwareData = await hardwareResponse.json();

      if (!eventResponse.ok) {
        throw new Error(`HTTP error! status: ${eventResponse.status}`);
      }
      events.value = await eventResponse.json();

      components.value = hardwareData;
      counts.value = Array(hardwareData.length).fill(0); // Count fängt bei 0 an

    } catch (error) {
      console.error("Error al obtener datos:", error);
    }
  });
  // Count Inkrementierung
  function incrementCount(index: number) {
    if (counts.value[index] < components.value[index].amount) {
      counts.value[index]++;
    }
  }
  // Count Dekrementierung
  function decrementCount(index: number) {
    if (counts.value[index] > 0) {
      counts.value[index]--;
    }
  }
  // Reservierung schicken
  async function submitReservation() {
    const selectedEvent = events.value.find(event => event.id === selectedEventId.value);
    if (!selectedEvent) {
      alert("Wählen Sie ein gültiges Event aus.");
      return;
    }
    // Filtra los componentes que están seleccionados y tienen una cantidad mayor que 0
    const selectedComponents = components.value.filter((_, index) => {
      return checkedNames.value.includes(components.value[index].name) && counts.value[index] > 0;
    });
    // erstellt die Arrays „hardwareIds“, „quantities“ und „selectedHardwareNames“.
    const hardwareIds = selectedComponents.map(hardware => hardware.id);
    const quantities = selectedComponents.map((_, index) => counts.value[index]);
    const selectedHardwareNames = selectedComponents.map(hardware => hardware.name);

    if (hardwareIds.length === 0) {
      alert('Wählen Sie mindestens eine Komponente mit einer Anzahl größer als 0 aus.');
      return;
    }

    try {
      // „POST“-Anfrage mit der URL im JSON-Text wird gesendet an den Controller
      const response = await fetch(`reservation/reserve/${selectedEventId.value}`, { 
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          eventId: selectedEvent.id,
          hardwareIds,
          quantities,
          eventName: selectedEvent.name,
          componentNames: selectedHardwareNames, 
          startDate: new Date(selectedEvent.startDate).toISOString(), 
          endDate: new Date(selectedEvent.endDate).toISOString(), // Datumkonvertierung zu ISO
        }),
      });

      if (!response.ok) {
        throw new Error(`Fehler bei der Reservierung: ${response.status}`);
      }

      const result: ReservationResponse = await response.json();
      reservationMessage.value = result.message;

      // Komponentenmengen nach erfolgreicher Reservierung aktualisieren
      components.value.forEach((component) => {
        const index = hardwareIds.findIndex(id => id === component.id);
        if (index !== -1) {
          component.amount -= quantities[index];
          if (component.amount < 0) {
            component.amount = 0;
          }
        }
      });
      // Zählwerte nach Reservierung zurücksetzen
      counts.value = Array(components.value.length).fill(0);
    } catch (error) {
      console.error('Fehler bei der Reservierung:', error);
    }
  }
</script>

<style>
  .reservation {
    max-width: 500px;
    margin: 20px auto;
    padding: 20px;
    background-color: #f7f7f7;
    border-radius: 8px;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
    font-family: Arial, sans-serif;
    font-size: medium;
  }
  .reservation h1 {
    font-size: 24px;
    margin-bottom: 1.5rem;
    text-align: center; 
    width: 100%; 
  }
  .warning {
      color: crimson;
  }
  .message {
    color: #4caf50;
    text-align: center;
    font-weight: bold;
    margin-bottom: 20px;
  }
  .button-send {
    padding: 12px;
    font-weight: bold;
    font-size: 1em;
    border: none;
    border-radius: 4px;
    cursor: pointer;
    transition: background-color 0.3s;
  }
  .select-event, hardware-data, #eventSelect {
    padding: 10px;
    border: none;
    border-radius: 4px;
    cursor: pointer;
    transition: background-color 0.3s;
  }
  strong {
    font-weight: bold;
  }
  @media (min-width: 1024px) {
    .reservation {
      max-width: 100vh;
      min-height: 80vh;
      display: flex;
      align-items: center;
    }
  }
</style>
