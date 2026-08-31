<script setup lang="ts">
import { ref } from "vue";
import logo from "@/assets/JobPortal_logo.png"
import Sidebar from "@/components/common/SideBar.vue";
import { useAuthStore } from "@/stores/authStore";
import { useRouter } from "vue-router";
const authStore = useAuthStore();
const router = useRouter();
const showProfileMenu = ref(false);

const toggleProfile = () => {
    showProfileMenu.value = !showProfileMenu.value;
};

const handleLogout = () => {
    authStore.logout();
    showProfileMenu.value =false;
    router.push("/login");
};
const recruiterMenu = [
  { name: "Dashboard", icon: "🏠", path: "/recruiterDashboard" },
  { name: "Company Profile", icon: "🏢", path: "/company" },
  { name: "Post Job", icon: "📢", path: "/postJob" },
  { name: "Manage Jobs", icon: "💼", path: "/manageJobs" },
  { name: "Candidates", icon: "👥", path: "/candidates" },
  { name: "Interviews", icon:"📅", path: "/interview"},
  {name: "Shortlisted", icon:"⭐", path: "shortlisted"}
];
const bottomMenu = [
  { name: "Preferences", icon: "⚙" },
  { name: "Dark Mode", icon: "🌙" },
  { name: "Help Center", icon: "💬" }
];

</script>


<template>

    <div class="dashboard">


        <!-- Sidebar -->
        <Sidebar
        companyName="Jobsy"
        slogan="Find Your Sea"
        :logo="logo"
        :menuItems="recruiterMenu"
        :bottomMenu="bottomMenu"
/>
        <!-- <aside class="sidebar">


            <div class="brand">

                <img src="@/assets/JobPortal_logo.png" class="logo-img" />

                <div class="brand-text">
                    <h2 class="company">Jobsy</h2>
                    <p class="slogan">Hire The Best</p>
                </div>

            </div>



            <div class="menu-item active">
                🏠 Dashboard
            </div>


            <div class="menu-item">
                🏢 Company Profile
            </div>


            <div class="menu-item">
                📢 Post Job
            </div>


            <div class="menu-item">
                💼 Manage Jobs
            </div>


            <div class="menu-item">
                👥 Candidates
            </div>


            <div class="menu-item">
                ⭐ Shortlisted
            </div>


            <div class="menu-item">
                📅 Interviews
            </div>


            <div class="bottom-menu">


                <div>
                    ⚙ Settings
                </div>


                <div>
                    💬 Help Center
                </div>


            </div>


        </aside> -->



        <!-- Main -->

        <section class="main">



            <header class="topbar">


                <div class="search-box">

                    <input placeholder="Search candidates..." />

                    <button>
                        Search
                    </button>

                </div>



                <div class="profile-dropdown">


                    <div class="profile-button" @click="toggleProfile">


                        <img src="@/assets/user.png" />

                        <span>
                            ABC Technologies
                        </span>

                        ▼


                    </div>



                    <div v-if="showProfileMenu" class="dropdown-menu">


                        <div>
                            Edit Profile
                        </div>

                        <div @click="handleLogout">
                            Logout
                        </div>


                    </div>


                </div>


            </header>




            <div class="content">


                <h1>
                    Welcome Recruiter 👋
                </h1>



                <div class="cards">


                    <div class="card">

                        <h2>
                            12
                        </h2>

                        <p>
                            Active Jobs
                        </p>

                    </div>


                    <div class="card">

                        <h2>
                            245
                        </h2>

                        <p>
                            Total Applicants
                        </p>

                    </div>


                    <div class="card">

                        <h2>
                            35
                        </h2>

                        <p>
                            Shortlisted
                        </p>

                    </div>


                    <div class="card">

                        <h2>
                            8
                        </h2>

                        <p>
                            Interviews
                        </p>

                    </div>


                </div>




                <div class="recent">


                    <h2>
                        Recent Applications
                    </h2>


                    <table>

                        <thead>
                            <tr>
                                <th>
                                    Candidate
                                </th>

                                <th>
                                    Position
                                </th>

                                <th>
                                    Status
                                </th>

                            </tr>
                        </thead>


                        <tbody>

                            <tr>

                                <td>
                                    John Smith
                                </td>

                                <td>
                                    .NET Developer
                                </td>

                                <td>
                                    Shortlisted
                                </td>

                            </tr>


                            <tr>

                                <td>
                                    Alex Brown
                                </td>

                                <td>
                                    Vue Developer
                                </td>

                                <td>
                                    Pending
                                </td>

                            </tr>

                        </tbody>


                    </table>


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
    overflow-y: auto;

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

.content {
    padding: 30px;
}


.cards {

    display: grid;
    grid-template-columns: repeat(4, 1fr);
    gap: 20px;

}


.card {

    background: white;
    padding: 25px;
    border-radius: 12px;
    box-shadow: 0 5px 15px #ddd;

}


.card h2 {

    font-size: 35px;
    color: rgb(24, 46, 107);

}


.recent {

    margin-top: 30px;
    background: white;
    padding: 25px;
    border-radius: 12px;

}


table {

    width: 100%;
    border-collapse: collapse;

}


th,
td {

    padding: 15px;
    text-align: left;
    border-bottom: 1px solid #eee;

}



@media(max-width:900px) {

    .cards {
        grid-template-columns: repeat(2, 1fr);
    }

}


@media(max-width:600px) {

    .cards {
        grid-template-columns: 1fr;
    }

}
</style>