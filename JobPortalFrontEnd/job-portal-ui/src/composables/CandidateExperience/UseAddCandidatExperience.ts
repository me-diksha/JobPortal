import api from "@/api/axios";
import type { CandidateExperienceRequest } from "@/types/candidate";

export const AddCandidateExperience = async (payload :CandidateExperienceRequest) => {
    return await api.post("/candidate/addexperience",payload);
};