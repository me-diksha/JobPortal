<script setup lang="ts">

import { ref } from "vue";


const isEdit = ref(false);



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


    <div class="profile-page">



        <div class="profile-header">


            <img src="@/assets/user.png" class="profile-img" />



            <div>

                <h1>
                    {{ profile.name }}
                </h1>


                <p>
                    {{ profile.headline }}
                </p>


            </div>


            <button @click="editProfile">

                {{ isEdit ? "Cancel" : "Edit Profile" }}

            </button>



        </div>





        <!-- About -->

        <div class="section">


            <h2>
                About
            </h2>


            <div v-if="!isEdit">


                <p>
                    {{ profile.bio }}
                </p>


                <p>
                    📍 {{ profile.location }}
                </p>


                <p>
                    Expected Salary:
                    {{ profile.expectedSalary }}
                </p>


            </div>




            <div v-else>


                <textarea v-model="profile.bio" />


                <input v-model="profile.location" />


                <input v-model="profile.expectedSalary" />


            </div>


        </div>





        <!-- Skills -->


        <div class="section">


            <h2>
                Skills
            </h2>


            <div class="skills">


                <span v-for="skill in profile.skills" :key="skill">

                    {{ skill }}

                </span>


            </div>


        </div>






        <!-- Education -->


        <div class="section">


            <h2>
                Education
            </h2>



            <div v-for="edu in profile.education" :key="edu.degree" class="card">


                <h3>
                    {{ edu.degree }}
                </h3>


                <p>
                    {{ edu.institute }}
                </p>


                <p>
                    {{ edu.year }}
                </p>


            </div>


        </div>






        <!-- Experience -->


        <div class="section" id="experience">


            <h2>
                Experience
            </h2>


            <div v-for="exp in profile.experience" :key="exp.company" class="card">


                <h3>
                    {{ exp.role }}
                </h3>


                <p>
                    {{ exp.company }}
                </p>


                <p>
                    {{ exp.duration }}
                </p>


            </div>



        </div>



        <button v-if="isEdit" class="save-btn" @click="saveProfile">

            Save Changes

        </button>



    </div>


</template>
<style scoped>
.profile-page {

    padding: 30px;
    background: #f5f7fb;
    height:100vh;
    overflow-y: auto;
    


}



.profile-header {

    background: white;

    padding: 25px;

    border-radius: 15px;

    display: flex;

    align-items: center;

    gap: 20px;

}



.profile-img {

    width: 100px;

    height: 100px;

    border-radius: 50%;

}



.profile-header button {

    margin-left: auto;

    padding: 12px 25px;

    background: rgb(24, 46, 107);

    color: white;

    border: none;

    border-radius: 8px;

    cursor: pointer;

}



.section {

    background: white;

    margin-top: 25px;

    padding: 25px;

    border-radius: 15px;

}



.section h2 {

    color: rgb(24, 46, 107);

}



.skills {

    display: flex;

    gap: 10px;

}



.skills span {

    background: #e8edff;

    padding: 8px 15px;

    border-radius: 20px;

}



.card {

    padding: 15px;

    border: 1px solid #eee;

    border-radius: 10px;

    margin-top: 15px;

}

#experience{
    margin-bottom: 30px;
}

textarea,
input {

    width: 100%;

    padding: 12px;

    margin: 8px 0;

    border: 1px solid #ddd;

    border-radius: 8px;

}



.save-btn {

    margin-top: 20px;

    padding: 15px 30px;

    background: green;

    color: white;

    border: none;

    border-radius: 8px;

}
</style>