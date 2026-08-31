<script setup lang="ts">

import { ref, onMounted } from "vue";
import Sidebar from "@/components/common/SideBar.vue";
import logo from "@/assets/JobPortal_logo.png";
import ProfileHeader from "@/components/common/ProfileHeader.vue";
import type { CandidateProfile as CandidateProfileType } from "@/types/candidate";
import AboutCard from "@/components/candidateProfile/AboutCard.vue";
import SkillsCard from "@/components/candidateProfile/SkillsCard.vue";
import type { CandidateSkill } from "@/types/candidate";
import EducationCard from "@/components/candidateProfile/EducationCard.vue";
import type { CandidateEducation as CandidateEducationType } from "@/types/candidate";
import type { CandidateExperience as CandidateExperienceType } from "@/types/candidate";
import ExperienceCard from "@/components/candidateProfile/ExperienceCard.vue";
import { GetCandidateProfileDetails } from "@/composables/CandidateProfile/UseGetCandidateProfile";
import { GetCandidateSkills } from "@/composables/CandidateSkills/UseGetCandidateSkill";
import { GetCandidateEducation } from "@/composables/CandidateEducation/UseGetCandidateEducation";
import { GetCandidateExperience } from "@/composables/CandidateExperience/UseGetCandidateExperience";

const isEdit = ref(false);
const loading = ref(false);
const error = ref("");
const candidateProfile = ref<CandidateProfileType | null>(null);
const candidateSkills = ref<CandidateSkill[]>([]);
const candidateEducation = ref<CandidateEducationType[]>([]);
const candidateExperienence = ref<CandidateExperienceType[]>([]);
const candidateMenu = [

    {
        name: "Home",
        icon: "🏠",
        path: "/candidateDashboard"
    },


    {
        name: "Browse Jobs",
        icon: "💼",
        path: "/jobs"
    },

    {
        name: "Applications",
        icon: "📄",
        path: "/applications"
    },

    {
        name: "Saved Jobs",
        icon: "⭐",
        path: "/savedJobs"
    }

];
const bottomMenu = [
    { name: "Preferences", icon: "⚙" },
    { name: "Dark Mode", icon: "🌙" },
    { name: "Help Center", icon: "💬" }
];

const loadprofiledetails = async () => {
    try {
        loading.value = true;
        error.value = "";

        const response = await GetCandidateProfileDetails();
        candidateProfile.value = response.data;

        const responseSkills = await GetCandidateSkills();
        candidateSkills.value = responseSkills.data ?? [];

        const responseEducation = await GetCandidateEducation();
        candidateEducation.value = responseEducation.data ?? [];

        const responseExperience = await GetCandidateExperience();
        candidateExperienence.value = responseExperience.data ?? [];

        console.log("Candidate Profile:", response.data);
    } catch (err) {
        console.error("Error fetching candidate profile:", err);
        error.value = "Unable to load profile details.";
    } finally {
        loading.value = false;
    }
}
onMounted(() => {
    loadprofiledetails();
})
const handleEducationEdit = (updatedEducation: CandidateEducationType) => {
    const index = candidateEducation.value.findIndex(
        education => education.id === updatedEducation.id
    );

    if (index !== -1) {
        candidateEducation.value[index] = updatedEducation;
    }
};

const handleEducationDelete = (id: number) => {
    candidateEducation.value = candidateEducation.value.filter(
        education => education.id !== id
    );
};
const handleEducationAdd = (newEducation: CandidateEducationType) => {
    candidateEducation.value.push(newEducation);
};
const handleExperienceEdit = (updatedExperience: CandidateExperienceType) => {
    const index = candidateExperienence.value.findIndex(exp => exp.id === updatedExperience.id);
    if (index !== -1) {
        candidateExperienence.value[index] = updatedExperience
    }
};

const handleExperienceDelete = (id: number) => {
    candidateExperienence.value = candidateExperienence.value.filter(
        experience => experience.id !== id
    );
};
const handleExperienceAdd = async (newExperience: CandidateExperienceType) => {
    candidateExperienence.value.push(newExperience);
}
const handleSkillAdded = async () => {
    const updatedSkills = await GetCandidateSkills();
    candidateSkills.value = updatedSkills.data ?? [];

};
const handleSkillDelete = (id: number) => {
    candidateSkills.value = candidateSkills.value.filter(
        skill => skill.id !== id
    );
};

const editProfile = () => {

    isEdit.value = !isEdit.value;

};

const updateProfile = (
    updatedProfile: CandidateProfileType
) => {

    candidateProfile.value = updatedProfile;

};

const saveProfile = () => {

    console.log(candidateProfile.value);

    isEdit.value = false;

    // API PUT call here

};


</script>



<template>

    <div class="dashboard">
        <Sidebar companyName="Jobsy" slogan="Find Your Sea" :logo="logo" :menuItems="candidateMenu"
            :bottomMenu="bottomMenu" />
        <section class="main">


            <!-- <header class="topbar">

                <h2>
                    My Profile
                </h2>


            </header> -->



            <div class="profile-page">


                <ProfileHeader v-if="candidateProfile" :first-name="candidateProfile.firstname"
                    :last-name="candidateProfile.lastName" :subtitle="candidateProfile.headline"
                    :location="`${candidateProfile.city ?? ''}, ${candidateProfile.state ?? ''}, ${candidateProfile.country ?? ''}`"
                    :is-editing="isEdit" @edit="editProfile" />


                <AboutCard v-if="candidateProfile" :profile="candidateProfile" :is-editing="isEdit"
                    @update="updateProfile" />


                <SkillsCard :skills="candidateSkills" :is-editing="isEdit" @added="handleSkillAdded"
                    @delete="handleSkillDelete" />


                <EducationCard :educations="candidateEducation" :is-editing="isEdit" @added="handleEducationAdd"
                    @edit="handleEducationEdit" @delete="handleEducationDelete" />


                <ExperienceCard :experiences="candidateExperienence" :is-editing="isEdit" @edit="handleExperienceEdit"
                    @added="handleExperienceAdd" @delete="handleExperienceDelete" />


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


.topbar {

    height: 70px;
    background: white;

    display: flex;
    align-items: center;

    padding: 0 30px;

}



.profile-page {

    padding: 30px;

}
</style>