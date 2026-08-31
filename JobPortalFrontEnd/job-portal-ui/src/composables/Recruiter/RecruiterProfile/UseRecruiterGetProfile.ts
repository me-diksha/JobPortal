import api from "@/api/axios";

export const GetRecruiterProfile = async () => {
    return await api.get("/recruiter/getrecruiterprofile");
};