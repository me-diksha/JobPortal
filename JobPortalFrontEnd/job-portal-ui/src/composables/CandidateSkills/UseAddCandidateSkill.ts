import api from "@/api/axios";
import type { CandidateSkillRequest } from "@/types/candidate";

export const AddCandidateSkills = async (payload :CandidateSkillRequest) => {
    return await api.post("/candidate/addskills",payload);
};