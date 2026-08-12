<script setup lang="ts">

import { ref } from "vue";
import Sidebar from "@/components/common/SideBar.vue";
import logo from "@/assets/JobPortal_logo.png";

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


const profile = ref({

    name: "John Doe",

    headline: "Full Stack Developer",

    email: "john@gmail.com",

    location: "Delhi",

    bio: "Passionate developer with experience in .NET and Vue.",

    expectedSalary: "800000",


    skills: [
        ".NET",
        "Vue",
        "PostgreSQL"
    ],


    education: [
        {
            degree: "B.Tech Computer Engineering",
            institute: "ABC University",
            year: "2025"
        }
    ],


    experience: [
        {
            company: "Sonar Softech",
            role: "Software Developer Intern",
            duration: "6 Months"
        }
    ]

});



const editProfile = () => {

    isEdit.value = !isEdit.value;

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


                <ProfileHeader :profile="profile" />


                <AboutCard :profile="profile" />


                <SkillsCard :skills="profile.skills" />


                <EducationCard :education="profile.education" />


                <ExperienceCard :experience="profile.experience" />


            </div>


        </section>
    </div>

</template>
<style scoped>
.dashboard{

display:flex;
height:100vh;

}


.main{

flex:1;
background:#f5f7fb;

height:100vh;

overflow-y:auto;

}


.topbar{

height:70px;
background:white;

display:flex;
align-items:center;

padding:0 30px;

}



.profile-page{

padding:30px;

}
</style>