import api from "@/api/axios";
import type { RecruiterProfileRequest } from "@/types/recruiter";

export const UpdateRecruiterProfile = async (
    payload: RecruiterProfileRequest
) => {
    return await api.put(
        "/recruiter/updateprofile",
        payload
    );
};