import api from "@/api/axios";

export const GetJob = async (
    id: number
) => {
    return await api.get(
        `/job/getjob?id=${id}`
    );
};