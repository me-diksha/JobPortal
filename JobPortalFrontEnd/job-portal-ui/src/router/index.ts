import { createRouter, createWebHistory } from 'vue-router'
import Login from "../views/auth/Login.vue";
import CandidateDashboard from "../views/candidate/CandidateDashboard.vue";
import RecruiterDashboard from '@/views/recruiter/RecruiterDashboard.vue';
import Register from '@/views/auth/Register.vue';
import CandidateProfile from "@/views/candidate/CandidateProfile.vue";
import { getActorType } from '@/utils/auth.ts';
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      redirect: "/login",
    },
    {
      path: "/login",
      component: Login,
    },
    {
      path: "/candidateDashboard",
      component: CandidateDashboard,
      meta: {
        requiresAuth: true,
        role: "candidate"
      }
    },
    {
      path: "/recruiterDashboard",
      component: RecruiterDashboard,
      meta: {
        requiresAuth: true,
        role: "recruiter"
      }
    },
    {
      path: "/register",
      component: Register,
    },
    {
      path: "/candidateprofile",
      component: CandidateProfile,
      meta: {
        requiresAuth: true,
        role: "candidate"
      }
    }
  ],
})
router.beforeEach((to, from, next) => {
  const token = localStorage.getItem("token");

  if (to.meta.requiresAuth && !token) {
    return next("/login");
  }

  if (to.meta.role) {
    const role = getActorType();

    if (role !== to.meta.role) {
      return next("/login");
    }
  }

  next();
});
export default router

