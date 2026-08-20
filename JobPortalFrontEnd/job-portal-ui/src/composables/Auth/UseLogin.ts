import api from "@/api/axios"

export const login = async (data: any) => {
    return await api.post("/auth/login", data);
};