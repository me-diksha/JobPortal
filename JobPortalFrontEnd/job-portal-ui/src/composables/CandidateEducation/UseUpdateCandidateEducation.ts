import api from "@/api/axios"

export const UpdateCandidateEducation = async () => {
    return await api.put("/candidate/getprofile");
};