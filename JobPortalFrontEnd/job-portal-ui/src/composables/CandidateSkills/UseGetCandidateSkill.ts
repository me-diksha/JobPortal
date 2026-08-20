import api from "@/api/axios"

export const GetCandidateSkills = async () => {
    return await api.get("/candidate/getskills");
};