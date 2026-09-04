import api from "@/api/axios"

export const DeleteJob = async (
    id: number
) => {
    return await api.delete(
        `/job/deletejob?id=${id}`
    );
};