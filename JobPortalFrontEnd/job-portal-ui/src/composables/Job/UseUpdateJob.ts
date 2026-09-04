import api from "@/api/axios"
import type { JobRequest } from "@/types/Job";

export const UpdateJob = async (
    payload: JobRequest
) => {
    return await api.put(
        '/job/updatejob?id=${id}',
        payload
    );
};