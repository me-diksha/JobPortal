import { createRouter, createWebHistory } from 'vue-router'
import Login from "../views/auth/Login.vue";
import CandidateDashboard from "../views/candidate/CandidateDashboard.vue";
import RecruiterDashboard from '@/views/recruiter/RecruiterDashboard.vue';
import Register from '@/views/auth/Register.vue';
import CandidateProfile from "@/views/candidate/CandidateProfile.vue";
import { useAuthStore } from '@/stores/authStore.ts';
import { roles } from '@/common/PermissionRoles.ts';

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
        role: roles.Candidate
      }
    },
    {
      path: "/recruiterDashboard",
      component: RecruiterDashboard,
      meta: {
        requiresAuth: true,
        role: roles.Recruiter
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
        role: roles.Candidate
      }
    }
  ],
})
router.beforeEach((to, from, next) => {

    const authStore = useAuthStore();

    // Protected route
    if (to.meta.requiresAuth) {

        // No token -> login
        if (!authStore.isAuthenticated) {
            return next("/login");
        }

        // Check role
        if (to.meta.role && authStore.actorType !== to.meta.role) {
            return next("/login");
        }
    }

    next();
});
export default router

