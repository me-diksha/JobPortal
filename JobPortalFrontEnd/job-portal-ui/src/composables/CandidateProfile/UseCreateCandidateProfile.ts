import api from "@/api/axios"
import type { CreateCandidateProfileRequest } from "@/types/candidate";

export const CreateCandidateProfileDetails = async (data: CreateCandidateProfileRequest) => {
    return await api.post("/candidate/createprofile",data);
};