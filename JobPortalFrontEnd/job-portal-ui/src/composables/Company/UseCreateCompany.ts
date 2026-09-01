import api from "@/api/axios"

export const CreateCompany = async () => {
    return await api.get("/common/all/skills");
};