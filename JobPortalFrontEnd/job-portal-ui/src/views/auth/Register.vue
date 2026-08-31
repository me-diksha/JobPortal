<script setup lang="ts">

import { ref } from "vue";
import { useRouter } from "vue-router"
import { useToast } from "vue-toastification"
import {register} from "@/composables/Auth/UseRegister"
import { useAuthStore } from "@/stores/authStore";
import { roles } from "@/common/PermissionRoles";
import { RoleId } from "@/common/PermissionRoles";
const toast = useToast();
const router = useRouter();

const role = ref("candidate");


const form = ref({

    FirstName: "",
    LastName: "",
    email: "",
    password: "",
    confirmPassword: "",
    companyName: "",
    country: "",
   

});



const registerUser = async() => {
    const authStore = useAuthStore();
    console.log({
        role: role.value,
        ...form.value
    });

    if (form.value.password !== form.value.confirmPassword) {
        toast.error("Passwords do not match");
        return;
    }
    const roleId = role.value === "candidate" ?  RoleId.Candidate: RoleId.Recruiter;
    var response = await register({
        FirstName :form.value.FirstName,
        LastName :form.value.LastName,
        Country :form.value.country,
        CompanyName:form.value.companyName,
        Email: form.value.email,
        Password :form.value.password,
        RoleId :roleId
    })
    toast.success("Registration successfull");
    authStore.setToken(response.data.token);


        if(authStore.actorType === roles.Candidate)
        {
            router.push("/candidateDashboard");
        }
        else if(authStore.actorType === roles.Recruiter)
        {
            router.push("/recruiterDashboard");
        }
        else
        {
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


                    <button @click="role = 'candidate'" :class="{ active: role === 'candidate' }">

                        Candidate

                    </button>



                    <button @click="role = 'recruiter'" :class="{ active: role === 'recruiter' }">

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

                    <div v-if="role === 'recruiter'">


                        <input v-model="form.companyName" placeholder="Company Name" required />



                    </div>

                    <div v-if="role === 'candidate'">
                        <input v-model="form.country" placeholder="Country" required />
                    </div>




                    <button type="submit" class="register-btn" >

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
</style>