import api from "@/api/axios";

export const GetAllExperienceLevel = async () => {
    return await api.get(
        "/common/all/experienceLevel"
    );
};