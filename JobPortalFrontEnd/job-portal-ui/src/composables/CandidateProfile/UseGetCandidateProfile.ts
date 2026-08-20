import api from "@/api/axios"

export const GetCandidateProfileDetails = async () => {
    return await api.get("/candidate/getprofile");
};