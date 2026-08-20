import api from "@/api/axios"

export const GetCandidateEducation = async () => {
    return await api.get("/candidate/geteducation");
};