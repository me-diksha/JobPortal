<script setup lang="ts">
import { ref, onMounted } from "vue";
import type { CompanyDetails,CompanyDetailsRequest } from "@/types/company";
import { GetCompany } from "@/composables/Company/UseGetCompany";
import { UpdateCompany } from "@/composables/Company/UseUpdateCompany";
import { useCompanyStore } from "@/stores/companyStore";
const companyStore = useCompanyStore();
const isloading = ref(false);
const companyDetails =
    ref<CompanyDetails | null>(null);
const loadCompany = async ()=>{
    try{
        isloading.value =true;
        const companyId = companyStore.companyId;
        if(companyId == null)return;
        const response = await GetCompany(companyId);
        companyDetails.value = response.data;
       loadEditData();
    }catch (error ){
        console.error(
            "Error in fetching Company Details:",
            error
        );
    }
    finally{
        isloading.value =false;
    }
}
onMounted (()=>{
    loadCompany();
});
const editData = ref<CompanyDetails>({
    id:0,
    name: "",
    description:"",
    industry:"",
    website:"",
    email:"",
    phone:"",
    addressLine1:"",
    addressLine2:"",
    city:"",
    state:"",
    country:"",
    postalCode:"",
    logosUrl:"",
    companySize:0,
    foundedYear:0
});
const loadEditData =  ()=>{
     if (!companyDetails.value) return;

   editData.value ={
   id :companyDetails.value.id,
   name: companyDetails.value.name,
   description:companyDetails.value.description,
    industry:companyDetails.value.industry,
    website:companyDetails.value.website,
    email:companyDetails.value.email,
    phone:companyDetails.value.phone,
    addressLine1:companyDetails.value.addressLine1,
    addressLine2:companyDetails.value.addressLine2,
    city:companyDetails.value.city,
    state:companyDetails.value.state,
    country:companyDetails.value.country,
    postalCode:companyDetails.value.postalCode,
    logosUrl:companyDetails.value.logosUrl,
    companySize:companyDetails.value.companySize,
    foundedYear:companyDetails.value.foundedYear
 }
}
const saveCompany  = async ()=>{


}
</script>
<template>
    <div class = "section-header">
    <h2>Company Details</h2>
    <button class ="edit-btn">Edit</button>
</div>
<form>
    <div class ="info-grid">
    <input v-model="editData.name" placeholder="Company Name" required>
    <input v-model="editData.description" placeholder="Company Description" >
    <input v-model="editData.industry" placeholder="Company Industry" required>
    <input v-model="editData.website" placeholder="Company website" required>
    <input v-model="editData.email" placeholder="email" required>
    <input v-model="editData.phone" placeholder="Company phone" required>
    <input v-model="editData.addressLine1" placeholder="Company address" required>
    <input v-model="editData.addressLine2" placeholder="Company addressline2" >
    <input v-model="editData.city" placeholder="Company City" required>
    <input v-model="editData.state" placeholder="Company State" required>
    <input v-model="editData.country" placeholder="Company Country" required>
    <input v-model="editData.postalCode" placeholder="Company postalcode" required>
    <input v-model="editData.logosUrl" placeholder="Company logo" >
    <input v-model="editData.companySize" placeholder="Company size" >
    <input v-model="editData.foundedYear" placeholder="founded year" required>
</div>
</form>
</template>
<style scoped>
.section-header {

    display: flex;

    justify-content: space-between;

    align-items: center;

    margin-bottom: 20px;

}
h2{
    color:rgb(24, 46, 107);
}
.edit-btn{
    background-color: rgb(24, 46, 107);
    color: white;
    padding: 10px;
    border: none;
    margin:20px;
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

</style>