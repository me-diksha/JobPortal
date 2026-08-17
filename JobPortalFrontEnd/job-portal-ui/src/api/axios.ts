import axios from "axios";
import { useAuthStore } from "@/stores/authStore";

const api = axios.create({
    baseURL: "https://localhost:7237/api",
    headers: {
        "Content-Type": "application/json"
    }
});

api.interceptors.request.use(
    (config) => {

        const authStore = useAuthStore();

        const isAuthRequest =
            config.url?.includes("/auth/login") ||
            config.url?.includes("/auth/register");

        if (
            !isAuthRequest &&
            authStore.accessToken
        ) {

            config.headers.Authorization =
                `Bearer ${authStore.accessToken}`;
        }

        return config;
    },

    (error) => {
        return Promise.reject(error);
    }
);

export default api;