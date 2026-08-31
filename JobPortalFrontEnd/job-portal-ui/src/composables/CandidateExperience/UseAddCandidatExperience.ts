import api from "@/api/axios";
import type { CandidateExperienceRequest } from "@/types/candidate";

export const AddCandidateExperience = async (payload :CandidateExperienceRequest) => {
    const response = await api.post("/candidate/addexperience",payload);
    return response.data;
};