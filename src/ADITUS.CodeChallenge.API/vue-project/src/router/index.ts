import { createRouter, createWebHistory } from 'vue-router'
import StatisticsView from '../views/StatisticsViews.vue'
import ReservationView from '../views/ReservationView.vue'
import ReservationsView from '../views/ReservationsView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/statistics',
      name: 'statistics',
      component: StatisticsView,
    },
    {
      path: '/reservation',
      name: 'reservation',
      component: ReservationView,
    },
    {
      path: '/release',
      name: 'release',
      component: ReservationsView,
    },
  ],
})

export default router
