import api from "@/api/axios";
import type { CandidateExperienceRequest } from "@/types/candidate";

export const UpdateCandidateExperience = async (id: number,
    experience: CandidateExperienceRequest) => {
    return await api.put(`/candidate/updateexperience?id=${id}`,experience);
};