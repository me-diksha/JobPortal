import api from "@/api/axios"
import type { RegisterRequest } from "@/types/Register";


export const register = async (payload: RegisterRequest) => {
    return await api.post("/auth/register", payload);
};
