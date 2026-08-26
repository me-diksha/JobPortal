import api from "@/api/axios"

export const GetAllSkills = async () => {
    return await api.get("/common/all/skills");
};