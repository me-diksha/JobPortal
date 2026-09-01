import api from "@/api/axios"

export const GetCompany = async (id: any) => {
    return await api.get("/company/getcompany", {
        params: {
            id: id
        }
    });
};