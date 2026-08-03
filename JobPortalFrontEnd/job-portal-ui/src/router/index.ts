import { createRouter, createWebHistory } from 'vue-router'
import Login from "../views/auth/Login.vue";
import CandidateDashboard from "../views/candidate/CandidateDashboard.vue";
import RecruiterDashboard from '@/views/recruiter/RecruiterDashboard.vue';
import Register from '@/views/auth/Register.vue';
import CandidateProfile from "@/views/candidate/CandidateProfile.vue";
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
    },
    {
      path: "/recruiterDashboard",
      component: RecruiterDashboard,
    },
    {
      path: "/register",
      component: Register,
    },
    {
      path:"/candidateprofile",
      component:CandidateProfile,
    }
  ],
})

export default router
