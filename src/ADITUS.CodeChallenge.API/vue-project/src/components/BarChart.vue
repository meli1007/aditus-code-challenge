<template>
  <div class="container-graphic">
    <canvas ref="chartRef"></canvas>
  </div>
</template>

<script lang="ts">
  import { defineComponent, onMounted, onBeforeUnmount, ref, watch } from 'vue';
  import { Chart, registerables } from 'chart.js';
  import type { ChartData, ChartOptions } from 'chart.js';

  Chart.register(...registerables);

  export default defineComponent({
    props: {
      chartData: {
        type: Object as () => ChartData<'bar'>, // Spezifiziert der Typ
        required: true,
      },
      chartOptions: {
        type: Object as () => ChartOptions<'bar'>, 
        required: true,
      },
    },
    setup(props) {
      const chartRef = ref<HTMLCanvasElement | null>(null);
      let chartInstance: Chart | null = null;

      console.log('chartData:', props.chartData);
      console.log('chartOptions:', props.chartOptions);

      const createChart = () => {
        if (chartRef.value) {
          if (chartInstance) {
            chartInstance.destroy(); 
          }
          chartInstance = new Chart(chartRef.value.getContext('2d')!, {
            type: 'bar',
            data: props.chartData,
            options: props.chartOptions,
          });
        }
      };

      onMounted(createChart);

      onBeforeUnmount(() => {
        if (chartInstance) {
          chartInstance.destroy();
        }
      });

      watch(
        () => props.chartData,
        (newData) => {
          if (chartInstance && newData.datasets.length > 0) { // Prüft, ob Daten vorhanden sind
            chartInstance.data = newData; // Aktualisiert die Daten
            chartInstance.update(); // Aktualisiert die Graphik
          }
        },
        { deep: true } // beobachtet Veränderungen an komplexen Objekten
      );
      return { chartRef };
    },
  });
</script>

<style>
  canvas {
    max-width: 100vh;
    height: 400px;
  }
  @media (min-width: 1024px) {
    .container-graphic {
      max-width: 100vh;
      min-height: 80vh;
      display: flex;
      align-items: center;
    }
  }
</style>
