import api from "@/api/axios";

export const GetAllEmploymentType = async () => {
    return await api.get("/common/all/employmentType");
};