// composables/Recruiter/UseRecruiterContext.ts

import { GetRecruiterProfile } from "@/composables/Recruiter/RecruiterProfile/UseRecruiterGetProfile";
import { useCompanyStore } from "@/stores/companyStore";

export const loadRecruiterContext = async () => {

    const companyStore = useCompanyStore();

    if (companyStore.companyId) {
        return;
    }

    const response = await GetRecruiterProfile();

    if (response.data.companyId) {
        companyStore.setCompanyId(response.data.companyId);
    }

};