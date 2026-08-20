<script setup lang="ts">
import { ref } from "vue";
import login_bg from '@/assets/login_bg.jpg'
import { useRouter } from "vue-router";
import { login } from "@/composables/Auth/UseLogin";
import { HTTP_StatusCodes } from "@/components/common/enum/HTTP_StatusCodes";
import { useToast } from "vue-toastification";
import { useAuthStore } from "@/stores/authStore";
import { roles } from "@/common/PermissionRoles";
const email = ref("");
const password = ref("");
const toast = useToast();
const handleLogin = async () => {

    try {
        const authStore = useAuthStore();
        const response = await login({

            email: email.value,
            password: password.value

        });
        if (response.status == HTTP_StatusCodes.OK) {

            toast.success("Login successfull");
           
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
        }


    }
    catch (error: unknown) {
        toast.error("Invalid User");
        console.log(error);
        return;

    }

}

const router = useRouter();


const goToRegister = () => {
    router.push("/register");
};

</script>

<template>
    <div class="login-container" :style="{ backgroundImage: `url(${login_bg})` }">
        <div class="login-card">

            <h1>Job Portal</h1>

            <h2>Welcome Back</h2>
            <form @submit.prevent="handleLogin">
                <input v-model="email" type="email" placeholder="Email" required />

                <input v-model="password" type="password" placeholder="Password" required />

                <button type="submit">
                    Login
                </button>
            </form>
            <p class="register-text">
                Don't have an account?
                <span @click="goToRegister" class="register">
                    Register
                </span>
            </p>

        </div>
    </div>
</template>

<style scoped>
:global(body) {
    margin: 0;
    padding: 0;
    overflow: hidden;
}

.login-container {
    display: flex;
    justify-content: center;
    align-items: center;

    height: 100vh;
    width: 100vw;

    background-size: cover;
    background-position: center;
    background-repeat: no-repeat;
}

.login-card {
    width: 380px;
    background: white;
    padding: 35px;
    border-radius: 12px;
    box-shadow: 0 4px 15px rgba(0, 0, 0, .1);
}

h1 {
    text-align: center;
    margin-bottom: 5px;
}

h2 {
    text-align: center;
    color: gray;
    margin-bottom: 30px;
}

input {

    width: 100%;
    padding: 12px;
    margin-bottom: 18px;
    border: 1px solid #ccc;
    border-radius: 6px;
    font-size: 16px;
    box-sizing: border-box;

}

button {

    width: 100%;
    padding: 12px;
    background: rgb(24, 46, 107);
    color: white;
    border: none;
    border-radius: 6px;
    cursor: pointer;
    font-size: 16px;

}

button:hover {

    background: #1565c0;

}

p {

    margin-top: 20px;
    text-align: center;

}

a {

    text-decoration: none;

}

.register {
    color: rgb(24, 46, 107);
}

.register:hover {
    color: #1565c0;
}
</style>