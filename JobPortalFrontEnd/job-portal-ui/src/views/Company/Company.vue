<script setup lang="ts">
import { ref, onMounted } from "vue";
import type { CompanyDetails } from "@/types/company";
import { GetCompany } from "@/composables/Company/UseGetCompany";
import { UpdateCompany } from "@/composables/Company/UseUpdateCompany";
import { useCompanyStore } from "@/stores/companyStore";
const companyStore = useCompanyStore();
const companyDetails =
    ref<CompanyDetails | null>(null);
const loadCompany = async ()=>{
    const response = await GetCompany(companyStore.companyId);
    companyDetails.value = response.data;
}

</script>
<template>
</template>
<style scoped>
</style>