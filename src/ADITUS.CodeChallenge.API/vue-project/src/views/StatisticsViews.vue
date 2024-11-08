<template>
  <div class="container">
    <h1>Veranstaltungen-Statistiken</h1>
    <BarChart :chartData="chartData" :chartOptions="chartOptions"/>
  </div>
</template>

<script setup lang="ts">
  import { ref, onMounted } from 'vue';
  import BarChart from '../components/BarChart.vue';
  import type { MyEvent, OnsiteEvent, OnlineEvent, HybridEvent } from '../types/chartInterfaces.ts';
  import type { ChartData, ChartOptions } from 'chart.js';

  const chartData = ref<ChartData<'bar'>>({
    labels: ["visitorsCount", "exhibitorsCount", "boothsCount", "attendees", "invites", "visits", "virtualRooms"],
    datasets: []
  });

  const chartOptions: ChartOptions<'bar'> = {
    responsive: true,
    plugins: {
      legend: { display: true, position: 'top' }
    },
    scales: {
      y: { beginAtZero: true }
    }
  };
  //holt die Daten von den StatisticsController für die Statistiken
  onMounted(async () => {
    try {
      const response = await fetch('/statistics');
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      const data = await response.json();
      chartData.value.datasets = [];

      //Jeder Event wird durchgelaufen, um die Statistiken je nach Typ zu organisieren
      data.myEvents.forEach((myevent: MyEvent) => {
        const eventName = myevent.name;
        const onsiteData: any = {
          label: eventName,
          data: [0, 0, 0, 0, 0, 0, 0], // Array initialisieren
          backgroundColor: "rgba(75, 192, 192, 0.6)"
        };

        const onlineData: any = {
          label: eventName,
          data: [0, 0, 0, 0, 0, 0, 0], 
          backgroundColor: "rgba(153, 102, 255, 0.6)"
        };

        const hybridData: any = {
          label: eventName,
          data: [0, 0, 0, 0, 0, 0, 0],
          backgroundColor: "rgba(255, 159, 64, 0.6)"
        };
        //Daten für die verschiedenen Eventtypen werden hier bearbeitet, damit die in die Graphik später gezeigt werden
        data.eventOnsite.forEach((event: OnsiteEvent) => {
          if (event.id === myevent.id && event.type === "OnSite") {
            onsiteData.data = [
              event.visitorsCount || 0,
              event.exhibitorsCount || 0,
              event.boothsCount || 0,
            ];
            chartData.value.datasets.push(onsiteData); 
          }
        });

        data.eventOnline.forEach((event: OnlineEvent) => {
          if (event.id === myevent.id && event.type === "Online") {
            onlineData.data = [
              0, 0, 0,
              event.attendees || 0,
              event.invites || 0,
              event.visits || 0,
              event.virtualRooms || 0
            ];
            chartData.value.datasets.push(onlineData);
          }
        });

        data.eventHybrid.forEach((event: HybridEvent) => {
          if (event.id === myevent.id && event.type === "Hybrid") {
            hybridData.data = [
              event.visitorsCount || 0,
              event.exhibitorsCount || 0,
              event.boothsCount || 0,
              event.attendees || 0,
              event.invites || 0,
              event.visits || 0,
              event.virtualRooms || 0
            ];
            chartData.value.datasets.push(hybridData);
          }
        });
      });
    } catch (error) {
      console.error("Fehler beim Holen des Daten:", error);
    } 
  });
</script>

<style>

  BarChart {
    max-width: 100vh;
    height: 100%;
  }
</style>
