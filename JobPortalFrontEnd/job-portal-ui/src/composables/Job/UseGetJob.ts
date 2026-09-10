import api from "@/api/axios";

export const GetJobById = async (
    id: number
) => {
    return await api.get(
        `/job/getjob?id=${id}`
    );
};