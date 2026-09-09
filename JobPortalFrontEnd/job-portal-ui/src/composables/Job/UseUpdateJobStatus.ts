import api from "@/api/axios";

export const UpdateJobStatus = async (
    jobId: number,
    statusId: number
) => {
    return await api.put(
        `/job/updateStatus?id=${jobId}`,
        {
            statusId: statusId
        }
    );
};