import api from "@/api/axios"

export const GetRecruiterJobs = async () => {
    return await api.get(
        "/job/getrecruiterjobs"
    );
};