import api from "@/api/axios"
import type { JobRequest } from "@/types/Job";

export const CreateJob = async (
    payload: JobRequest
) => {
    return await api.post(
        "/job/createjob",
        payload
    );
};