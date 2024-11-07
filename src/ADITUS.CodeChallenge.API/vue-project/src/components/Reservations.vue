<template>
  <div class="release">
    <h2>Reservierungen: </h2>
    <ul>
      <li v-for="(reservation, index) in reservations" :key="reservation.id">
        <strong class="text-color">Reservierung wird geprüft:</strong>
        <p>
          <strong>
            - Reservierungsnummer
          </strong>
          {{ reservation.id }}<br />
          <strong>
            - Event:
          </strong>
          {{ reservation.eventName }}<br />
          <strong>
            - Hardware-Komponenten die reserviert wurden:
          </strong>
          {{ formatComponents(reservation.hardwareNames, reservation.quantities) }}<br />
          <strong>
            - Datum von der Event:
          </strong>
            {{ formatDate(reservation.startDate) }} a {{ formatDate(reservation.endDate) }}<br />
        </p>
      </li>
    </ul>
  </div>
</template>

<script setup lang="ts">
  import { ref, onMounted } from 'vue';
  import { format } from 'date-fns';
  import type { Reservation } from '../types/reservationInterfaces.ts';

  const reservations = ref<Reservation[]>([]);
  // Datum formatieren
  const formatDate = (date: Date) => {
    return format(date, 'dd-MM-yyyy');
  };

  // holt die Reservierung von EventHardwareController
  const fetchReservations = async () => {
    try {
      const response = await fetch('/reservation/reservations');

      if (!response.ok) {
        throw new Error(`HTTP error! Status: ${response.status}`);
      }

      const data = await response.json();
      reservations.value = data;
      
    } catch (error) {
      console.error('Fehler beim Holen der Reservierungen', error);
    }
  };
  // ruf fetch
  onMounted(() => {
    fetchReservations();
  });
  //formatiert die Hardware-Components mit den Mengen
  const formatComponents = (hardwareNames: string[], quantities: number[]) => {
    if (hardwareNames.length > 0) {
      return hardwareNames.map((name, index) => `${name} (${quantities[index]})`).join(', ');
    } else {
      return 'Keine Komponenten';
    }
  };
</script>

<style>
  .release {
    max-width: 500px;
    margin: 20px auto;
    padding: 20px;
    background-color: #f7f7f7;
    border-radius: 8px;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
    font-family: Arial, sans-serif;
    font-size: medium;
  }
  strong {
    font-weight: bold;
  }
  .text-color {
      color: crimson;
  }
  @media (min-width: 1024px) {
    .release {
      max-width: 100vh;
      display: flex;
      align-items: center;
    }
  }

</style>
