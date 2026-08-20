import api from "@/api/axios"

export const GetCandidateExperience = async () => {
    return await api.get("/candidate/getexperience");
};