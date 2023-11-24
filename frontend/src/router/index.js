import { createRouter, createWebHistory } from 'vue-router'
import MoviePageView from '../views/MoviePageView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: MoviePageView
    }
  ]
})

export default router
