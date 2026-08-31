import api from "@/api/axios";
import type { CandidateEducationRequest } from "@/types/candidate";

export const AddCandidateEducation = async (
    payload: CandidateEducationRequest
) => {
    const response = await api.post<number>(
        "/candidate/addeducation",
        payload
    );

    return response.data;
};