import api from "@/api/axios";

export const DeleteCandidateSkill = async (id: number) => {
    return await api.delete(`/candidate/deleteskills/${id}`);
};