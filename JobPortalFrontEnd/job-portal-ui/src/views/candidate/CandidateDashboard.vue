<script setup lang="ts">
import { ref } from "vue";
import { useRouter } from "vue-router";
import Sidebar from "@/components/common/SideBar.vue";
import logo from "@/assets/JobPortal_logo.png";
const router = useRouter();


const goToProfile = () => {

    router.push("/candidateprofile");

};
const showProfileMenu = ref(false);

const toggleProfile = () => {
    showProfileMenu.value = !showProfileMenu.value;
};
const handlelogout = () => {

}
const candidateMenu = [

{
 name:"Home",
 icon:"🏠",
 path:"/candidateDashboard"
},


{
 name:"Browse Jobs",
 icon:"💼",
 path:"/jobs"
},

{
 name:"Applications",
 icon:"📄",
 path:"/applications"
},

{
 name:"Saved Jobs",
 icon:"⭐",
 path:"/savedJobs"
}

];
const bottomMenu = [
  { name: "Preferences", icon: "⚙" },
  { name: "Dark Mode", icon: "🌙" },
  { name: "Help Center", icon: "💬" }
];
</script>


<template>

    <div class="dashboard">
        <Sidebar companyName="Jobsy"
       slogan="Find Your Sea"
       :logo="logo"
       :menuItems="candidateMenu"
      :bottomMenu="bottomMenu"/>

        <!-- Sidebar -->
        <!-- <aside class="sidebar">
            <div class="brand">
                <img src="@/assets/JobPortal_logo.png" class="logo-img" />
                <div class="brand-text">
                    <h2 class="company">Jobsy</h2>
                    <p class="slogan">Find Your Sea</p>
                </div>
            </div>

             <div class="user-info">

        <img 
        src="@/assets/user.png"
        class="profile-img"
        />

        <h3>John Doe</h3>

    </div> 




            <div class="menu-item ">
                🏠 Home
            </div>
            <div class="menu-item">
                ♟ My Jobs
            </div>


            <div class="menu-item">
                📄 Applications
            </div>


            <div class="menu-item">
                👥 Browse Talent
            </div>


            <div class="bottom-menu">

                <div>
                    ⚙ Preferences
                </div>

                <div>
                    🌙 Dark Mode
                </div>

                <div>
                    💬 Help Center
                </div>

            </div>


        </aside> -->



        <!-- Main Area -->

        <section class="main">


            <header class="topbar">


                <div class="search-box">

                    <input placeholder="Search jobs..." />

                    <button>
                        Search
                    </button>

                </div>



                <div class="profile-dropdown">


                    <div class="profile-button" @click="toggleProfile">

                        <img src="@/assets/user.png" alt="User Photo" />

                        <span>
                            John Doe
                        </span>

                        ▼

                    </div>



                    <div v-if="showProfileMenu" class="dropdown-menu">

                        <div @click="goToProfile">
                            My Profile
                        </div>


                        <div v-on:click="handlelogout">
                            Logout
                        </div>


                    </div>


                </div>


            </header>


            <div class="profile-card">


                <h1>
                    Welcome John 👋
                </h1>


                <div class="details">

                    <p>
                        <b>Email:</b> john@gmail.com
                    </p>


                    <p>
                        <b>Location:</b> Delhi
                    </p>


                    <p>
                        <b>Skills:</b> .NET, Vue, PostgreSQL
                    </p>


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



/* Sidebar */

.sidebar {

    width: 250px;
    min-height: 100vh;
    background: rgb(24, 46, 107);
    color: white;
    padding: 25px;
    display: flex;
    flex-direction: column;

    position: sticky;
    top: 0;
    overflow-y: auto;
}


.brand {
    display: flex;
    align-items: center;
    gap: 12px;
    margin-bottom: 20px;
}

.brand-text {
    display: flex;
    flex-direction: column;
}

.company {
    margin: 0;
    font-size: 24px;
    font-weight: bold;
}

.slogan {
    margin: 2px 0 0 0;
    font-size: 12px;
    color: rgba(255, 255, 255, 0.7);
    letter-spacing: 1px;
}




.logo-img {

    width: 50px;
    height: 50px;
    border-radius: 50%;
    object-fit: cover;

}



.menu-item {
    display: flex;
    align-items: center;
    padding: 15px;
    margin: 10px 0;
    cursor: pointer;
    border-radius: 8px;

    transition: 0.3s;

}

.menu-item.active {

    background: #fff1e8;
    color: rgb(24, 46, 107);

}


.menu-item:hover {

    background: white;
    color: rgb(24, 46, 107);

}

.bottom-menu {

    /* margin-top:auto; */

    border-top: 1px solid rgba(255, 255, 255, 0.15);
    align-items: center;

    padding-top: 20px;
    padding-bottom: 30px;

}


.bottom-menu div {

    padding: 12px;
    border-radius: 8px;

    cursor: pointer;

    display: flex;
    align-items: center;
    gap: 10px;
}

.bottom-menu div:hover {

    background: #334155;

}

/* Main */

.main {

    flex: 1;
    background: #f5f7fb;
    height: 100vh;
    display: flex;
    flex-direction: column;

    overflow: hidden;


}



.topbar {

    height: 70px;
    background: white;
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 0 30px;

}




.search-box input {

    width: 650px;
    padding: 12px;
    border: 1px solid #ddd;
    border-radius: 8px;

}


.search-box button {

    padding: 12px 20px;
    margin-left: 10px;
    background: rgb(24, 46, 107);
    color: white;
    border: none;
    border-radius: 8px;

}



/* Profile */

.profile-button {

    display: flex;
    align-items: center;
    gap: 10px;
    cursor: pointer;

}


.profile-button img {

    width: 40px;
    height: 40px;
    border-radius: 50%;

}



.dropdown-menu {

    position: absolute;
    right: 30px;
    top: 65px;
    background: white;
    width: 160px;
    box-shadow: 0 5px 15px #ddd;
    border-radius: 8px;

}


.dropdown-menu div {

    padding: 15px;
    cursor: pointer;

}


.dropdown-menu div:hover {

    background: #f1f5f9;
    color: rgb(24, 46, 107);

}



/* Content */

.profile-card {

    margin: 30px;
    background: white;
    padding: 30px;
    border-radius: 12px;

}
</style>