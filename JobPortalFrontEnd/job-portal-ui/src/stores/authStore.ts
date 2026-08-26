import { defineStore } from "pinia";
import { computed, ref } from "vue";
import { jwtDecode } from "jwt-decode";

interface JwtPayload {
    exp?: number;
    sub?: string;
    email?: string;
    actor_type?: string;
}

export const useAuthStore = defineStore(
    "auth",
    () => {

        const accessToken = ref<string | null>(
            sessionStorage.getItem("accessToken")
        );

        const userId = ref<number | null>(null);
        const actorType = ref<string | null>(null);
        const email = ref<string | null>(null);


        const parseToken = (token: string) => {

            try {

                return jwtDecode<JwtPayload>(token);

            } catch {

                return null;

            }
        };


        const isAuthenticated = computed(() => {

            if (!accessToken.value) {
                return false;
            }

            const payload = parseToken(
                accessToken.value
            );

            if (!payload?.exp) {
                return false;
            }

            const isValid =
                payload.exp >
                Math.floor(Date.now() / 1000);

            if (!isValid) {
                clear();
                return false;
            }

            return true;
        });


        const setToken = (token: string) => {

            accessToken.value = token;

            sessionStorage.setItem(
                "accessToken",
                token
            );

            const payload = parseToken(token);

            if (payload) {

                userId.value =
                    payload.sub
                        ? Number(payload.sub)
                        : null;

                actorType.value =
                    payload.actor_type ?? null;

                email.value =
                    payload.email ?? null;
            }
        };


        const clear = () => {

            accessToken.value = null;
            userId.value = null;
            actorType.value = null;
            email.value = null;

            sessionStorage.removeItem(
                "accessToken"
            );
        }
        const restoreUserFromToken = () => {

    if (!accessToken.value) {
        return;
    }

    const payload = parseToken(accessToken.value);

    if (!payload) {
        return;
    }

    userId.value =
        payload.sub
            ? Number(payload.sub)
            : null;

    actorType.value =
        payload.actor_type ?? null;

    email.value =
        payload.email ?? null;
};

restoreUserFromToken();
        


        return {
            accessToken,
            userId,
            actorType,
            email,
            isAuthenticated,
            setToken,
            clear
            
        };
    }
);