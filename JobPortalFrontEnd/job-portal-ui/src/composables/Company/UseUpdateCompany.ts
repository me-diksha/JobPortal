import api from "@/api/axios";
import type { CompanyDetails } from "@/types/company";

export const UpdateCompany = async (
    payload: CompanyDetails
) => {
    return await api.put(
        "/company/updateCompany",
        payload
    );
};