import api from "@/api/axios"



export const register = async (data: any) => {
    return await api.post("/auth/register", data);
};
