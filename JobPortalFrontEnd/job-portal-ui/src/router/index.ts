import { createRouter, createWebHistory } from 'vue-router'
import Login from "../views/auth/Login.vue";
import CandidateDashboard from "../views/candidate/CandidateDashboard.vue";
import RecruiterDashboard from '@/views/recruiter/RecruiterDashboard.vue';
import Register from '@/views/auth/Register.vue';
import CandidateProfile from "@/views/candidate/CandidateProfile.vue";
import { useAuthStore } from '@/stores/authStore.ts';
import { roles } from '@/common/PermissionRoles.ts';
import RecruiterProfile from '@/views/recruiter/RecruiterProfile.vue';

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
    }, {
      path: "/recruiterprofile",
      component: RecruiterProfile,
      meta: {
        requiresAuth: true,
        role: roles.Recruiter
      }
    }
  ],
})
router.beforeEach((to) => {
  const authStore = useAuthStore();

  // Route doesn't require authentication
  if (!to.meta.requiresAuth) {
    return true;
  }

  // User is not authenticated
  if (!authStore.isAuthenticated) {
    return "/login";
  }

  // User doesn't have the required role
  if (
    to.meta.role &&
    authStore.actorType !== to.meta.role
  ) {
    return "/login";
  }

  // Allow navigation
  return true;
});
export default router

