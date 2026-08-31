import api from "@/api/axios"

export const GetAllCompanies = async () => {
    return await api.get("/company/getallcompanies");
};