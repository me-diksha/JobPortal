<script setup lang="ts">

import { ref } from "vue";
import Sidebar from "@/components/common/SideBar.vue";
import logo from "@/assets/JobPortal_logo.png";
import ProfileHeader from "@/components/candidateProfile/ProfileHeader.vue";
import type { CandidateProfile as CandidateProfileType } from "@/types/candidate";
import AboutCard from "@/components/candidateProfile/AboutCard.vue";
import SkillsCard from "@/components/candidateProfile/SkillsCard.vue";
import type { CandidateSkill } from "@/types/candidate";
import EducationCard from "@/components/candidateProfile/EducationCard.vue";
import type { CandidateEducation } from "@/types/candidate";
import type { CandidateExperience } from "@/types/candidate";
import ExperienceCard from "@/components/candidateProfile/ExperienceCard.vue";

const isEdit = ref(false);
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


const profile = ref<CandidateProfileType>({

    id: 1,

    firstname: "John",

    lastName: "Doe",

    headline: "Full Stack Developer",

    bio:
        "Software developer experienced in .NET, Vue and PostgreSQL.",

    addressLine1: "123 Main Street",

    addressLine2: "",

    city: "Delhi",

    state: "Delhi",

    country: "India",

    currentSalary: 600000,

    expectedSalary: 800000,

    resumeUrl: null

});

const skills = ref<CandidateSkill[]>([
    {
        id: 1,
        skillId: 1,
        skillName: ".NET",
        experienceYears: 2
    },
    {
        id: 2,
        skillId: 2,
        skillName: "Vue.js",
        experienceYears: 1
    },
    {
        id: 3,
        skillId: 3,
        skillName: "PostgreSQL",
        experienceYears: 2
    }
]);

const educations = ref<CandidateEducation[]>([
    {
        id: 1,
        instituteName: "ABC Senior Secondary School",
        degree: "10th",
        fieldOfstudy: "",
        startYear: 2017,
        endYear: 2018,
        percentage: 86.5
    },
    {
        id: 2,
        instituteName: "ABC Senior Secondary School",
        degree: "12th",
        fieldOfstudy: "Science",
        startYear: 2018,
        endYear: 2020,
        percentage: 89.2
    },
    {
        id: 3,
        instituteName: "XYZ University",
        degree: "B.Tech",
        fieldOfstudy: "Computer Engineering",
        startYear: 2020,
        endYear: 2024,
        percentage: 82.5
    }
]);

const experiences = ref<CandidateExperience[]>([
    {
        id: 1,
        companyName: "ABC Senior Technologies",
        designation: "Intern",
        startDate: new Date("2018-01-01"),
        endDate: new Date("2018-06-01"),
        currentlyWorking: false,
        description: ""
    },
    {
        id: 2,
        companyName: "XYZ Technologies",
        designation: "Software Developer",
        startDate: new Date("2024-07-01"),
        currentlyWorking: true,
        description: "Working on .NET Web API and Vue.js applications."
    }
]);
const handleEducationEdit = (education: CandidateEducation) => {
    console.log("Edit:", education);
};

const handleEducationDelete = (id: number) => {
    educations.value = educations.value.filter(
        education => education.id !== id
    );
};
const handleExperienceEdit = (experience: CandidateExperience) => {
    console.log("Edit:", experience);
};

const handleExperienceDelete = (id: number) => {
    experiences.value = experiences.value.filter(
        experience => experience.id !== id
    );
};
const updateSkills = (updatedSkills: CandidateSkill[]) => {

    skills.value = updatedSkills;

};
const editProfile = () => {

    isEdit.value = !isEdit.value;

};

const updateProfile = (
    updatedProfile: CandidateProfileType
) => {

    profile.value = updatedProfile;

};

const saveProfile = () => {

    console.log(profile.value);

    isEdit.value = false;

    // API PUT call here

};


</script>



<template>

    <div class="dashboard">
        <Sidebar companyName="Jobsy" slogan="Find Your Sea" :logo="logo" :menuItems="candidateMenu"
            :bottomMenu="bottomMenu" />
        <section class="main">


            <header class="topbar">

                <h2>
                    Candidate Profile
                </h2>


            </header>



            <div class="profile-page">


                <ProfileHeader :profile="profile" @edit="editProfile" />


                <AboutCard :profile="profile" @update="updateProfile" />


                <SkillsCard :skills="skills" @update="updateSkills" />


                <EducationCard :educations="educations" @edit="handleEducationEdit" @delete="handleEducationDelete" />


                <ExperienceCard :experiences="experiences" @edit=""handleExperienceEdit @delete="handleExperienceDelete" />


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