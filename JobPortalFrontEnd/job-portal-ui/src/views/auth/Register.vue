<script setup lang="ts">

import { ref, computed, onMounted } from "vue";
import { useRouter } from "vue-router"
import { useToast } from "vue-toastification"
import { register } from "@/composables/Auth/UseRegister"
import { useAuthStore } from "@/stores/authStore";
import { roles } from "@/common/PermissionRoles";
import { RoleId } from "@/common/PermissionRoles";
import type { RegisterRequest } from "@/types/Register";
import type { Company } from "@/types/company";
import { GetAllCompanies } from "@/composables/Common/UseGetAllCompanies";
const toast = useToast();
const router = useRouter();
const role = ref("candidate");

const companies = ref<Company[]>([]);

const selectedCompanyId = ref<number | null>(null);

const companySearch = ref("");

const showCompanyDropdown = ref(false);

const showCompanyModal = ref(false);

const newCompanyName = ref("");
const form = ref({

    FirstName: "",
    LastName: "",
    email: "",
    password: "",
    confirmPassword: "",
    country: "",


});
const closeCompanyDropdown = () => {
    setTimeout(() => {
        showCompanyDropdown.value = false;
    }, 150);
};
const getCompanies = async () => {

    try {

        const response =
            await GetAllCompanies();

        companies.value = response.data;

    }
    catch (error) {

        console.error(error);

        toast.error("Unable to load companies");

    }
};

onMounted(() => {
    getCompanies();
})

const filteredCompanies = computed(() => {

    const search =
        companySearch.value.trim().toLowerCase();

    if (!search) {
        return companies.value;
    }

    return companies.value.filter(company =>
        company.name.toLowerCase().includes(search)
    );

});
const selectCompany = (company: Company) => {

    selectedCompanyId.value = company.id;

    companySearch.value = company.name;

    showCompanyDropdown.value = false;

};
const openCompanyModal = () => {

    newCompanyName.value = "";

    showCompanyDropdown.value = false;

    showCompanyModal.value = true;

};
const closeCompanyModal = () => {

    showCompanyModal.value = false;

};
const useNewCompany = () => {

    if (!newCompanyName.value.trim()) {

        toast.error("Company name is required");

        return;

    }

    // Clear existing selection
    selectedCompanyId.value = null;

    // Store the new company name
    companySearch.value =
        newCompanyName.value.trim();

    showCompanyModal.value = false;

};
const registerUser = async () => {
    const authStore = useAuthStore();
    console.log({
        role: role.value,
        ...form.value
    });

    if (form.value.password !== form.value.confirmPassword) {
        toast.error("Passwords do not match");
        return;
    }
    const roleId = role.value === "candidate" ? RoleId.Candidate : RoleId.Recruiter;
    var response = await register({
        FirstName: form.value.FirstName,
        LastName: form.value.LastName,
        Country:
        role.value === "candidate"
            ? form.value.country
            : null,
        Email: form.value.email,
        Password: form.value.password,
        RoleId: roleId,
        CompanyId:
        role.value === "recruiter"
            ? selectedCompanyId.value
            : null,

        CompanyName:
        role.value === "recruiter" &&
        selectedCompanyId.value === null
            ? companySearch.value.trim()
            : null
    })
    toast.success("Registration successfull");
    authStore.setToken(response.data.token);


    if (authStore.actorType === roles.Candidate) {
        router.push("/candidateDashboard");
    }
    else if (authStore.actorType === roles.Recruiter) {
        router.push("/recruiterDashboard");
    }
    else {
        router.push("/login");
    }

    // later API call
};



const goToLogin = () => {

    router.push("/login");

};


</script>



<template>

    <div class="register-page">


        <!-- Left Branding -->

        <div class="brand-section">


            <img src="@/assets/JobPortal_logo.png" class="logo-img" />


            <h1>
                Jobsy
            </h1>


            <p>
                Find Your Sea
            </p>


        </div>




        <!-- Register Form -->

        <div class="register-container">


            <div class="register-card">


                <h2>
                    Create Account
                </h2>


                <p class="subtitle">
                    Join Jobsy today
                </p>




                <!-- Role Selection -->

                <div class="role-box">


                    <button type="button" @click="role = 'candidate'" :class="{ active: role === 'candidate' }">

                        Candidate

                    </button>



                    <button type="button" @click="role = 'recruiter'" :class="{ active: role === 'recruiter' }">

                        Recruiter

                    </button>


                </div>





                <form @submit.prevent="registerUser">

                    <div class="Namecontainer">
                        <input v-model="form.FirstName" placeholder="FirstName" required />

                        <input v-model="form.LastName" placeholder="LastName" required />
                    </div>
                    <input v-model="form.email" placeholder="Email Address" required />

                    <input type="password" v-model="form.password" placeholder="Password" required />



                    <input type="password" v-model="form.confirmPassword" placeholder="Confirm Password" required />






                    <!-- Recruiter Extra Details -->

                    <div v-if="role === 'recruiter'" class="company-section">
                        

                        <div class="company-select-wrapper">

                            <div class="company-input-wrapper">

                                <span class="search-icon">⌕</span>

                                <input v-model="companySearch" class="company-input"
                                    placeholder="Search or select company" autocomplete="off"
                                    @focus="showCompanyDropdown = true" @click="showCompanyDropdown = true"  @blur="closeCompanyDropdown" />

                                <span class="dropdown-arrow" :class="{ rotated: showCompanyDropdown }">
                                    ▾
                                </span>

                            </div>


                            <!-- Dropdown -->

                            <div v-if="showCompanyDropdown" class="company-dropdown">

                                <!-- Existing companies -->

                                <div v-for="company in filteredCompanies" :key="company.id" class="company-option"
                                    :class="{
                                        selected: selectedCompanyId === company.id
                                    }" @click="selectCompany(company)">

                                    <div class="company-icon">
                                        {{ company.name.charAt(0).toUpperCase() }}
                                    </div>

                                    <div class="company-info">

                                        <span class="company-name">
                                            {{ company.name }}
                                        </span>

                                    </div>

                                    <span v-if="selectedCompanyId === company.id" class="check-icon">
                                        ✓
                                    </span>

                                </div>


                                <!-- No results -->

                                <div v-if="filteredCompanies.length === 0" class="no-company">

                                    <div class="no-company-icon">
                                        🔍
                                    </div>

                                    <span>
                                        No company found
                                    </span>

                                </div>


                                <!-- Add company -->

                                <div class="add-company-option" @click="openCompanyModal">

                                    <div class="add-company-icon">
                                        +
                                    </div>

                                    <div>
                                        <strong>Add new company</strong>

                                        <small>
                                            Can't find your company?
                                        </small>
                                    </div>

                                </div>

                            </div>

                        </div>

                    </div>

                    <div v-if="role === 'candidate'">
                        <input v-model="form.country" placeholder="Country" required />
                    </div>




                    <button type="submit" class="register-btn">

                        Register

                    </button>



                </form>




                <p class="login-link">

                    Already have an account?

                    <span @click="goToLogin">
                        Login
                    </span>


                </p>



            </div>


        </div>


    </div>

    <div v-if="showCompanyModal" class="modal-overlay">

        <div class="company-modal">

            <h3>Add New Company</h3>

            <input v-model="newCompanyName" placeholder="Company Name" />

            <div class="modal-buttons">

                <button type="button" class="cancel-btn" @click="closeCompanyModal">
                    Cancel
                </button>

                <button type="button" class="add-btn" @click="useNewCompany">
                    Use Company
                </button>

            </div>

        </div>

    </div>

</template>





<style scoped>
.register-page {

    height: 100vh;
    display: flex;
    background: #f5f7fb;

}



/* Branding */


.brand-section {

    width: 40%;
    background: rgb(24, 46, 107);

    color: white;

    display: flex;
    flex-direction: column;

    justify-content: center;

    align-items: center;

}



.logo-img {

    width: 100px;
    height: 100px;

    border-radius: 50%;

}


.brand-section h1 {

    font-size: 45px;
    margin: 20px 0 5px;

}



.brand-section p {

    font-size: 18px;

    opacity: .8;

}





/* Form Side */


.register-container {

    width: 60%;

    display: flex;

    justify-content: center;

    align-items: center;

}


.Namecontainer {
    display: flex;
    gap: 10px;
}

.register-card {

    width: 450px;

    background: white;

    padding: 40px;

    border-radius: 15px;

    box-shadow: 0 10px 25px rgba(0, 0, 0, 0.1);

}



.register-card h2 {

    color: rgb(24, 46, 107);

    font-size: 30px;

    margin-bottom: 5px;

}



.subtitle {

    color: #777;

}




/* Role */


.role-box {

    display: flex;

    gap: 15px;

    margin: 20px 0;

}



.role-box button {

    flex: 1;

    padding: 12px;

    border: none;

    border-radius: 8px;

    cursor: pointer;

    background: #e5e7eb;

}



.role-box .active {

    background: rgb(24, 46, 107);

    color: white;

}





/* Inputs */


input {

    width: 100%;

    padding: 13px;

    margin: 8px 0;

    border: 1px solid #ddd;

    border-radius: 8px;

    box-sizing: border-box;

}



.row {

    display: flex;

    gap: 10px;

}



.row input {

    width: 50%;

}





.register-btn {

    width: 100%;

    padding: 14px;

    margin-top: 15px;

    background: rgb(24, 46, 107);

    color: white;

    border: none;

    border-radius: 8px;

    cursor: pointer;

    font-size: 16px;

}



.register-btn:hover {

    background: #102052;

}





.login-link {

    text-align: center;

    margin-top: 20px;

    color: #666;

}



.login-link span {

    color: rgb(24, 46, 107);

    font-weight: bold;

    cursor: pointer;

}



@media(max-width:800px) {


    .brand-section {

        display: none;

    }


    .register-container {

        width: 100%;

    }


    .register-card {

        width: 90%;

    }


}

.company-section {
    position: relative;
    margin-top: 10px;
}

.company-label {
    display: block;
    margin-bottom: 7px;
    font-size: 14px;
    font-weight: 600;
    color: #374151;
}

.company-label span {
    color: #dc2626;
}


/* Input */

.company-select-wrapper {
    position: relative;
}

.company-input-wrapper {
    position: relative;
    display: flex;
    align-items: center;
}

.company-input {
    width: 100%;
    height: 46px;

    padding: 0 42px 0 40px;

    margin: 0;

    border: 1px solid #d9dee8;
    border-radius: 9px;

    font-size: 14px;

    background: white;

    transition: all 0.2s ease;
}

.company-input:focus {
    outline: none;

    border-color: rgb(24, 46, 107);

    box-shadow:
        0 0 0 3px rgba(24, 46, 107, 0.08);
}

.search-icon {
    position: absolute;
    left: 14px;

    font-size: 20px;

    color: #8b93a1;

    z-index: 2;
}

.dropdown-arrow {
    position: absolute;
    right: 14px;

    color: #6b7280;

    font-size: 16px;

    pointer-events: none;

    transition: transform 0.2s ease;
}

.dropdown-arrow.rotated {
    transform: rotate(180deg);
}


/* Dropdown */

.company-dropdown {
    position: absolute;

    top: calc(100% + 6px);

    left: 0;
    right: 0;

    background: white;

    border: 1px solid #e1e5eb;

    border-radius: 10px;

    box-shadow:
        0 10px 30px rgba(0, 0, 0, 0.12);

    overflow: hidden;

    z-index: 1000;

    max-height: 250px;

    overflow-y: auto;
}


/* Company option */

.company-option {
    display: flex;

    align-items: center;

    gap: 12px;

    padding: 11px 14px;

    cursor: pointer;

    transition: background 0.15s ease;
}

.company-option:hover {
    background: #f5f7fb;
}

.company-option.selected {
    background: #f1f4fa;
}


/* Company icon */

.company-icon {
    width: 34px;
    height: 34px;

    min-width: 34px;

    border-radius: 8px;

    display: flex;
    align-items: center;
    justify-content: center;

    background: #eef1f7;

    color: rgb(24, 46, 107);

    font-weight: 700;

    font-size: 14px;
}


/* Company text */

.company-info {
    flex: 1;

    min-width: 0;
}

.company-name {
    display: block;

    font-size: 14px;

    font-weight: 500;

    color: #252a34;

    white-space: nowrap;

    overflow: hidden;

    text-overflow: ellipsis;
}


/* Check */

.check-icon {
    color: rgb(24, 46, 107);

    font-weight: bold;

    font-size: 17px;
}


/* No result */

.no-company {
    display: flex;

    flex-direction: column;

    align-items: center;

    justify-content: center;

    gap: 5px;

    padding: 20px;

    color: #8a919e;

    font-size: 13px;
}

.no-company-icon {
    font-size: 20px;
}


/* Add company */

.add-company-option {
    display: flex;

    align-items: center;

    gap: 11px;

    padding: 12px 14px;

    border-top: 1px solid #edf0f4;

    cursor: pointer;

    color: rgb(24, 46, 107);

    transition: background 0.15s ease;
}

.add-company-option:hover {
    background: #f5f7fb;
}

.add-company-option strong {
    display: block;

    font-size: 14px;
}

.add-company-option small {
    display: block;

    margin-top: 2px;

    color: #8a919e;

    font-size: 11px;
}

.add-company-icon {
    width: 34px;
    height: 34px;

    min-width: 34px;

    border-radius: 8px;

    display: flex;

    align-items: center;
    justify-content: center;

    background: #eef1f7;

    font-size: 20px;

    font-weight: 500;
}
/* =========================
   Add Company Modal
   ========================= */

.modal-overlay {
    position: fixed;
    inset: 0;

    display: flex;
    align-items: center;
    justify-content: center;

    background: rgba(0, 0, 0, 0.45);

    z-index: 2000;
}

.company-modal {
    width: 400px;

    background: white;

    padding: 30px;

    border-radius: 14px;

    box-shadow: 0 20px 50px rgba(0, 0, 0, 0.2);

    animation: modalOpen 0.2s ease;
}

.company-modal h3 {
    margin: 0 0 20px;

    color: rgb(24, 46, 107);

    font-size: 22px;
}

.company-modal input {
    width: 100%;

    padding: 13px;

    margin: 0;

    border: 1px solid #d9dee8;

    border-radius: 8px;

    box-sizing: border-box;
}

.company-modal input:focus {
    outline: none;

    border-color: rgb(24, 46, 107);

    box-shadow: 0 0 0 3px rgba(24, 46, 107, 0.08);
}


/* Modal buttons */

.modal-buttons {
    display: flex;

    justify-content: flex-end;

    gap: 10px;

    margin-top: 20px;
}

.modal-buttons button {
    padding: 10px 18px;

    border-radius: 8px;

    cursor: pointer;

    font-size: 14px;
}

.cancel-btn {
    border: 1px solid #ddd;

    background: white;

    color: #555;
}

.cancel-btn:hover {
    background: #f5f5f5;
}

.add-btn {
    border: none;

    background: rgb(24, 46, 107);

    color: white;
}

.add-btn:hover {
    background: #102052;
}


/* Animation */

@keyframes modalOpen {

    from {
        opacity: 0;
        transform: scale(0.95);
    }

    to {
        opacity: 1;
        transform: scale(1);
    }

}
</style>