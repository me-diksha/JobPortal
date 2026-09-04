import api from "@/api/axios";

export const GetAllJobs = async () => {
    return await api.get(
        "/job/getalljobs"
    );
};