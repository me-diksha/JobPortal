<script setup lang="ts">

import { ref, onMounted } from "vue";

import Sidebar from "@/components/common/SideBar.vue";

import logo from "@/assets/JobPortal_logo.png";

import ProfileHeader from "@/components/common/ProfileHeader.vue";

import RecruiterInfoCard from "@/components/Recruiter/RecruiterInfoCard.vue";

import type { RecruiterProfile as RecruiterProfileType }
    from "@/types/recruiter";

import { GetRecruiterProfile } from "@/composables/Recruiter/RecruiterProfile/UseRecruiterGetProfile";
import { useCompanyStore } from "@/stores/companyStore";

const companyStore = useCompanyStore(); //tostore company id so can be used by company.vue
const isEdit = ref(false);

const loading = ref(false);

const error = ref("");

const recruiterProfile =
    ref<RecruiterProfileType | null>(null);


const recruiterMenu = [

    { name: "Dashboard", icon: "🏠", path: "/recruiterDashboard" },
    { name: "Company Profile", icon: "🏢", path: "/company" },
    { name: "Post Job", icon: "📢", path: "/recruiter/jobs/create" },
    { name: "Manage Jobs", icon: "💼", path: "/recruiter/jobs" },
    { name: "Candidates", icon: "👥", path: "/candidates" },
    { name: "Interviews", icon: "📅", path: "/interview" },
    { name: "Shortlisted", icon: "⭐", path: "shortlisted" }
];


const bottomMenu = [

    {
        name: "Preferences",
        icon: "⚙"
    },

    {
        name: "Dark Mode",
        icon: "🌙"
    },

    {
        name: "Help Center",
        icon: "💬"
    }

];


const loadProfile = async () => {

    try {

        loading.value = true;

        error.value = "";

        const response =
            await GetRecruiterProfile();

        recruiterProfile.value =
            response.data;
        if (response.data.companyId) {
            companyStore.setCompanyId(response.data.companyId);
        }
        console.log(
            "Recruiter Profile:",
            response.data
        );

    } catch (err) {

        console.error(
            "Error fetching recruiter profile:",
            err
        );

        error.value =
            "Unable to load recruiter profile.";

    } finally {

        loading.value = false;

    }

};


onMounted(() => {

    loadProfile();

});


const editProfile = () => {

    isEdit.value = !isEdit.value;

};


const updateProfile = (
    updatedProfile: RecruiterProfileType
) => {

    recruiterProfile.value =
        updatedProfile;

};


</script>


<template>

    <div class="dashboard">


        <!-- Sidebar -->

        <Sidebar companyName="Jobsy" slogan="Find Your Sea" :logo="logo" :menuItems="recruiterMenu"
            :bottomMenu="bottomMenu" />


        <!-- Main -->

        <section class="main">


            <div class="profile-page">


                <!-- Loading -->

                <div v-if="loading" class="message">

                    Loading profile...

                </div>


                <!-- Error -->

                <div v-else-if="error" class="error-box">

                    {{ error }}

                </div>


                <!-- Profile -->

                <template v-else-if="recruiterProfile">


                    <ProfileHeader :first-name="recruiterProfile.firstName" :last-name="recruiterProfile.lastName"
                        :subtitle="recruiterProfile.designation" @edit="editProfile" />


                    <RecruiterInfoCard :profile="recruiterProfile" @update="updateProfile" />


                </template>


                <!-- No profile -->

                <div v-else class="message">

                    Recruiter profile not found.

                </div>


            </div>

        </section>

    </div>

</template>


<style scoped>
.dashboard {

    display: flex;

    height: 100vh;

}


.main {

    flex: 1;

    background: #f5f7fb;

    height: 100vh;

    overflow-y: auto;

}


.profile-page {

    padding: 30px;

}


.message {

    background: white;

    padding: 30px;

    border-radius: 12px;

    text-align: center;

    color: #6b7280;

}


.error-box {

    background: #fef2f2;

    border: 1px solid #fecaca;

    color: #dc2626;

    padding: 15px;

    border-radius: 8px;

}
</style>