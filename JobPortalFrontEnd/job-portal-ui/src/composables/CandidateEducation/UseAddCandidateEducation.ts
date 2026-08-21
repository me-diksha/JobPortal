import api from "@/api/axios";
import type { CandidateEducationRequest } from "@/types/candidate";

export const AddCandidateEducation = async (payload :CandidateEducationRequest) => {
    return await api.post("/candidate/addeducation",payload);
};