import { defineStore } from "pinia";
import { ref } from "vue";

export const useCompanyStore = defineStore("company", () => {
    const companyId = ref<number | null>(null);

    const setCompanyId = (id: number) => {
        companyId.value = id;
    };

    return {
        companyId,
        setCompanyId
    };
});