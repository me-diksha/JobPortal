import api from "@/api/axios"
import type { JobRequest } from "@/types/Job";

export const UpdateJob = async (
    id: number,
    payload: JobRequest
) => {
    return await api.put(
        `/job/updatejob?id=${id}`,
        payload
    );
};