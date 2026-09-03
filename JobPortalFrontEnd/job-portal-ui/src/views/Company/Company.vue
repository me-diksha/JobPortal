<script setup lang="ts">
import { ref, onMounted } from "vue";
import type { CompanyDetails, CompanyDetailsRequest } from "@/types/company";
import { GetCompany } from "@/composables/Company/UseGetCompany";
import { UpdateCompany } from "@/composables/Company/UseUpdateCompany";
import { useCompanyStore } from "@/stores/companyStore";
import {HTTP_StatusCodes} from "@/components/common/enum/HTTP_StatusCodes";
import { useRouter } from "vue-router";
const companyStore = useCompanyStore();
const isloading = ref(false);
const ischangesSaved = ref(false);
const companyDetails =
    ref<CompanyDetails | null>(null);
const loadCompany = async () => {
    try {
        isloading.value = true;
        const companyId = companyStore.companyId;
        if (companyId == null) return;
        const response = await GetCompany(companyId);
        companyDetails.value = response.data;
        loadEditData();
    } catch (error) {
        console.error(
            "Error in fetching Company Details:",
            error
        );
    }
    finally {
        isloading.value = false;
    }
}
onMounted(() => {
    loadCompany();
});
const router = useRouter();
const editData = ref<CompanyDetails>({
    id: 0,
    name: "",
    description: "",
    industry: "",
    website: "",
    email: "",
    phone: "",
    addressLine1: "",
    addressLine2: "",
    city: "",
    state: "",
    country: "",
    postalCode: "",
    logosUrl: "",
    companySize: 0,
    foundedYear: 0
});
const loadEditData = () => {
    if (!companyDetails.value) return;

    editData.value = {
        id: companyDetails.value.id,
        name: companyDetails.value.name,
        description: companyDetails.value.description,
        industry: companyDetails.value.industry,
        website: companyDetails.value.website,
        email: companyDetails.value.email,
        phone: companyDetails.value.phone,
        addressLine1: companyDetails.value.addressLine1,
        addressLine2: companyDetails.value.addressLine2,
        city: companyDetails.value.city,
        state: companyDetails.value.state,
        country: companyDetails.value.country,
        postalCode: companyDetails.value.postalCode,
        logosUrl: companyDetails.value.logosUrl,
        companySize: companyDetails.value.companySize,
        foundedYear: companyDetails.value.foundedYear
    }
}
const saveCompany = async () => {

    try {
        const payload: CompanyDetails = {

            id: editData.value.id,

            name: editData.value.name,
            description: editData.value.description,
            industry: editData.value.industry,
            website: editData.value.website,
            email: editData.value.email,
            phone: editData.value.phone,
            addressLine1: editData.value.addressLine1,
            addressLine2: editData.value.addressLine2,
            city: editData.value.city,
            state: editData.value.state,
            country: editData.value.country,
            postalCode: editData.value.postalCode,
            logosUrl: editData.value.logosUrl,
            companySize: editData.value.companySize,
            foundedYear: editData.value.foundedYear
        };
        const response = await UpdateCompany(payload);
        if(response.status == HTTP_StatusCodes.OK){
            ischangesSaved.value = true;
        }
    } catch (error) {
        console.log(error);
    }

}
const redirect = ()=>{
    ischangesSaved.value = false; 
    router.push("/recruiterDashboard");
}
</script>
<template>
    <div class="section-header">
        <h2>Company Details</h2>
        <button class="edit-btn">Edit</button>
    </div>
    <form @submit.prevent="saveCompany">
        <div class="info-grid">
            <input v-model="editData.name" placeholder="Company Name" required>
            <input v-model="editData.description" placeholder="Company Description">
            <input v-model="editData.industry" placeholder="Company Industry">
            <input v-model="editData.website" placeholder="Company website" required>
            <input v-model="editData.email" placeholder="email" required>
            <input v-model="editData.phone" placeholder="Company phone">
            <input v-model="editData.addressLine1" placeholder="Company address" required>
            <input v-model="editData.addressLine2" placeholder="Company addressline2">
            <input v-model="editData.city" placeholder="Company City" required>
            <input v-model="editData.state" placeholder="Company State" required>
            <input v-model="editData.country" placeholder="Company Country" required>
            <input v-model="editData.postalCode" placeholder="Company postalcode" required>
            <input v-model="editData.logosUrl" placeholder="Company logo">
            <input v-model="editData.companySize" placeholder="Company size">
            <input v-model="editData.foundedYear" placeholder="founded year">
        </div>
        <button type="submit" class="edit-btn">Save</button>
    </form>
    <div v-if="ischangesSaved" class="modal-overlay">
    <div class="success-modal">
        <div class="success-icon">
            ✓
        </div>

        <h2>Changes Saved!</h2>

        <p>Your company details have been updated successfully.</p>

        <button
            class="modal-btn"
            @click="redirect"
        >
            OK
        </button>
    </div>
</div>
    
</template>
<style scoped>
.section-header {

    display: flex;

    justify-content: space-between;

    align-items: center;

    margin-bottom: 20px;

}

h2 {
    color: rgb(24, 46, 107);
}

.edit-btn {
    background-color: rgb(24, 46, 107);
    color: white;
    padding: 10px;
    border: none;
    margin: 20px;
    border-radius: 5px;
    cursor: pointer;

}

.edit-btn:hover {

    background: #334f9c;

}

.info-grid {

    display: grid;

    grid-template-columns: 1fr 1fr 1fr;

    gap: 20px;

}
.modal-overlay {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background: rgba(0, 0, 0, 0.5);

    display: flex;
    justify-content: center;
    align-items: center;

    z-index: 9999;
}

.success-modal {
    background: white;
    width: 400px;
    padding: 35px;
    border-radius: 12px;

    text-align: center;

    box-shadow: 0 10px 40px rgba(0, 0, 0, 0.2);

    animation: modalFadeIn 0.25s ease;
}

.success-modal h2 {
    margin: 15px 0 10px;
    color: rgb(24, 46, 107);
}

.success-modal p {
    color: #666;
    margin-bottom: 25px;
}

.success-icon {
    width: 70px;
    height: 70px;

    margin: 0 auto;

    border-radius: 50%;

    background: #28a745;
    color: white;

    font-size: 42px;
    font-weight: bold;

    display: flex;
    align-items: center;
    justify-content: center;
}

.modal-btn {
    background-color: rgb(24, 46, 107);
    color: white;

    border: none;
    border-radius: 5px;

    padding: 10px 30px;

    cursor: pointer;
    font-size: 15px;
}

.modal-btn:hover {
    background: #334f9c;
}

@keyframes modalFadeIn {
    from {
        opacity: 0;
        transform: scale(0.9);
    }

    to {
        opacity: 1;
        transform: scale(1);
    }
}
</style>