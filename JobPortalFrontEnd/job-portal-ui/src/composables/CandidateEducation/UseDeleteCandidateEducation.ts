import api from "@/api/axios";

export const DeleteCandidateEducation = async (id: number) => {
    return await api.delete(`/candidate/education/${id}`);
};