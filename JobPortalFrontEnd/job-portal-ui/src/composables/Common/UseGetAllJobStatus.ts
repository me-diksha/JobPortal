import api from "@/api/axios";

export const GetAllJobStatusList = async () => {
    return await api.get(
        "/common/all/jobStatus"
    );
};