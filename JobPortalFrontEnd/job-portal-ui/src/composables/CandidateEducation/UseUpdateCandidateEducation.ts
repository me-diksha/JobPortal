import api from "@/api/axios";
import type { CandidateEducationRequest } from "@/types/candidate";

export const UpdateCandidateEducation = async (id: number,
    education: CandidateEducationRequest) => {
    return await api.put(`/candidate/updateeducation?id=${id}`,education);
};