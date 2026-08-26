import api from "@/api/axios";
import type { CreateCandidateProfileRequest } from "@/types/candidate";

export const UpdateCandidateProfile = async (id: number,
    profile: CreateCandidateProfileRequest) => {
    return await api.put(`/candidate/update/candidateprofile?id=${id}`,profile);
};