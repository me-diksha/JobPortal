import api from "@/api/axios";

export const DeleteCandidateExperience = async (id: number) => {
    return await api.delete(`/candidate/experience/${id}`);
};