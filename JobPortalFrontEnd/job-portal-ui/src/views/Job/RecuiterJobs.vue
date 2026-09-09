<script setup lang="ts">

import {
    onMounted,
    ref
} from "vue";

import Sidebar from "@/components/common/SideBar.vue";

import logo from "@/assets/JobPortal_logo.png";

import {
    GetRecruiterJobs
} from "@/composables/Job/UseGetRecruiterJob";
import {
    GetAllJobStatusList
} from "@/composables/Common/UseGetAllJobStatus";

import {
    UpdateJobStatus
} from "@/composables/Job/UseUpdateJobStatus";
import type {
    Job
} from "@/types/Job";


const jobs =
    ref<Job[]>([]);

const loading =
    ref(false);

const error =
    ref("");
const jobStatuses = ref<any[]>([]);

const openStatusJobId = ref<number | null>(null);

const statusLoading = ref(false);

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

const loadJobStatuses = async () => {
    try {
        const response =
            await GetAllJobStatusList();

        jobStatuses.value =
            response.data ?? [];
    }
    catch (err) {
        console.error(
            "Error loading job statuses:",
            err
        );
    }
};
const loadJobs = async () => {

    try {

        loading.value = true;

        error.value = "";

        const response =
            await GetRecruiterJobs();

        jobs.value =
            response.data ?? [];

    } catch (err: any) {

        console.error(
            "Error loading jobs:",
            err
        );

        error.value =
            err.response?.data?.message ||
            "Failed to load jobs";

    } finally {

        loading.value = false;

    }

};


const formatSalary = (
    value?: number
) => {

    if (
        value === undefined ||
        value === null
    ) {
        return "-";
    }

    return new Intl.NumberFormat(
        "en-IN"
    ).format(value);

};


const formatDate = (
    date?: string
) => {

    if (!date) {
        return "-";
    }

    return new Date(date)
        .toLocaleDateString(
            "en-IN",
            {
                day: "2-digit",
                month: "short",
                year: "numeric"
            }
        );

};

const toggleStatusDropdown = (
    jobId: number
) => {
    if (openStatusJobId.value === jobId) {
        openStatusJobId.value = null;
    }
    else {
        openStatusJobId.value = jobId;
    }
};

const changeJobStatus = async (
    job: Job,
    statusId: number
) => {
    try {
        statusLoading.value = true;

        await UpdateJobStatus(
            job.id,
            statusId
        );

        // Reload jobs so the latest status comes from DB
        await loadJobs();

        openStatusJobId.value = null;
    }
    catch (err: any) {
        console.error(
            "Error updating job status:",
            err
        );

        error.value =
            err.response?.data?.message ||
            "Failed to update job status";
    }
    finally {
        statusLoading.value = false;
    }
};
const getStatusClass = (status?: string) => {
    if (!status) return "unknown";

    return status
        .toLowerCase()
        .trim()
        .replace(/\s+/g, "-");
};
onMounted(() => {

    loadJobs();
    loadJobStatuses();

});

</script>


<template>

    <div class="dashboard">

        <Sidebar companyName="Jobsy" slogan="Find Your Sea" :logo="logo" :menuItems="recruiterMenu"
            :bottomMenu="bottomMenu" />


        <section class="main">

            <div class="page-content">


                <!-- Header -->

                <div class="page-header">

                    <div>

                        <h1>
                            My Jobs
                        </h1>

                        <p>
                            Manage the jobs posted
                            by your company.
                        </p>

                    </div>


                    <button class="create-btn" @click="
                        $router.push('/createJob')
                        ">
                        + Create Job
                    </button>

                </div>


                <!-- Loading -->

                <div v-if="loading" class="state-message">

                    Loading jobs...

                </div>


                <!-- Error -->

                <div v-else-if="error" class="error-message">

                    {{ error }}

                </div>


                <!-- Empty -->

                <div v-else-if="jobs.length === 0" class="empty-card">

                    <div class="empty-icon">
                        💼
                    </div>

                    <h3>
                        No Jobs Yet
                    </h3>

                    <p>
                        You haven't posted any
                        jobs yet.
                    </p>

                    <button class="create-btn" @click="
                        $router.push('/createJob')
                        ">
                        Create Your First Job
                    </button>

                </div>


                <!-- Jobs -->

                <div v-else class="jobs-grid">

                    <div v-for="job in jobs" :key="job.id" class="job-card">


                        <!-- Card Header -->

                        <div class="job-card-header">

                            <div>

                                <h2>
                                    {{ job.title }}
                                </h2>

                                <p v-if="
                                    job.companyName
                                " class="company">
                                    🏢
                                    {{
                                        job.companyName
                                    }}
                                </p>

                            </div>


                            <div class="status-wrapper">

                                <button class="status-badge" :class="getStatusClass(job.status)"
                                    @click="toggleStatusDropdown(job.id)">
                                    {{ job.status || "Draft" }}

                                    <span class="status-arrow">
                                        ▾
                                    </span>
                                </button>

                                <div v-if="openStatusJobId === job.id" class="status-dropdown">

                                    <button v-for="status in jobStatuses" :key="status.id" class="status-option" :class="[
                                        getStatusClass(status.description),
                                        {
                                            active: status.description === job.status
                                        }
                                    ]" @click=" changeJobStatus(job,status.id)">
                                        {{ status.description }}
                                    </button>

                                </div>

                            </div>

                        </div>


                        <!-- Description -->
                        <div class="job-description" v-html="job.description"></div>


                        <!-- Details -->

                        <div class="job-details">

                            <div v-if="
                                job.location
                            " class="detail">

                                <span class="detail-label">
                                    Location
                                </span>

                                <span>
                                    📍
                                    {{
                                        job.location
                                    }}
                                </span>

                            </div>


                            <div v-if="
                                job.employmentType
                            " class="detail">

                                <span class="detail-label">
                                    Employment
                                </span>

                                <span>
                                    💼
                                    {{
                                        job.employmentType
                                    }}
                                </span>

                            </div>


                            <div v-if="
                                job.experienceLevel
                            " class="detail">

                                <span class="detail-label">
                                    Experience
                                </span>

                                <span>
                                    🎓
                                    {{
                                        job.experienceLevel
                                    }}
                                </span>

                            </div>


                            <div v-if="
                                job.minSalary !== undefined ||
                                job.maxSalary !== undefined
                            " class="detail">

                                <span class="detail-label">
                                    Salary
                                </span>

                                <span>
                                    ₹{{
                                        formatSalary(
                                            job.minSalary
                                        )
                                    }}
                                    -
                                    ₹{{
                                        formatSalary(
                                            job.maxSalary
                                        )
                                    }}
                                </span>

                            </div>


                            <div v-if="
                                job.deadline
                            " class="detail">

                                <span class="detail-label">
                                    Deadline
                                </span>

                                <span>
                                    📅
                                    {{
                                        formatDate(
                                            job.deadline
                                        )
                                    }}
                                </span>

                            </div>

                        </div>


                        <!-- Footer -->

                        <div class="job-card-footer">


                            <div>

                                <button class="action-btn">
                                    Edit
                                </button>

                                <button class="delete-btn">
                                    Delete
                                </button>

                            </div>

                        </div>

                    </div>

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


.page-content {

    padding: 30px;

    max-width: 1200px;

    margin: 0 auto;

}


.page-header {

    display: flex;

    align-items: center;

    justify-content: space-between;

    margin-bottom: 25px;

}


.page-header h1 {

    margin: 0;

    font-size: 28px;

    font-weight: 700;

    color: #1f2937;

}


.page-header p {

    margin-top: 6px;

    color: #6b7280;

    font-size: 14px;

}


.create-btn {

    border: none;

    border-radius: 8px;

    padding: 11px 18px;

    background: #4f46e5;

    color: white;

    font-size: 14px;

    font-weight: 600;

    cursor: pointer;

}


.create-btn:hover {

    background: #4338ca;

}



.jobs-grid {

    display: grid;

    grid-template-columns:
        repeat(auto-fit,
            minmax(340px, 1fr));

    gap: 20px;

}


.job-card {

    background: white;

    border-radius: 12px;

    padding: 22px;

    box-shadow:
        0 2px 10px rgba(0,
            0,
            0,
            0.05);

    transition:
        transform 0.2s,
        box-shadow 0.2s;

}


.job-card:hover {

    transform:
        translateY(-2px);

    box-shadow:
        0 6px 18px rgba(0,
            0,
            0,
            0.08);

}


.job-card-header {

    display: flex;

    justify-content: space-between;

    gap: 15px;

}


.job-card h2 {

    margin: 0;

    font-size: 19px;

    color: #1f2937;

}


.company {

    margin: 7px 0 0;

    font-size: 13px;

    color: #6b7280;

}


.status-badge {
    border: none;
    cursor: pointer;
    display: flex;
    align-items: center;
    gap: 5px;
    height: fit-content;
    padding: 6px 12px;
    border-radius: 20px;
    font-size: 12px;
    font-weight: 600;
}

.status-badge.draft {
    background: #fef3c7;
    color: #92400e;
}

.status-badge.open {
    background: #dcfce7;
    color: #166534;
}

.status-badge.closed {
    background: #fee2e2;
    color: #991b1b;
}

.status-badge.actively-recruiting {
    background: #dbeafe;
    color: #1e40af;
}

.status-badge.archived {
    background: #ede9fe;
    color: #6d28d9;
}

.status-badge.on-hold {
    background: #f3f4f6;
    color: #374151;
}
.status-option.draft {
    color: #92400e;
}

.status-option.open {
    
    color: #166534;
}

.status-option.closed {
    
    color: #991b1b;
}

.status-option.actively-recruiting {
   
    color: #1e40af;
}

.status-option.on-hold {
   
    color: #374151;
}

.status-option.archived {
    
    color: #6d28d9;
}
.status-option:hover {
    opacity: 0.85;
}
.description {

    margin: 18px 0;

    color: #6b7280;

    font-size: 14px;

    line-height: 1.6;

    display: -webkit-box;

    -webkit-line-clamp: 3;

    -webkit-box-orient: vertical;

    overflow: hidden;

}


.job-details {

    display: flex;

    flex-direction: column;

    gap: 11px;

    padding: 15px 0;

    border-top: 1px solid #eef0f4;

    border-bottom: 1px solid #eef0f4;

}


.detail {

    display: flex;

    justify-content: space-between;

    gap: 10px;

    font-size: 13px;

    color: #374151;

}


.detail-label {

    color: #9ca3af;

    font-weight: 500;

}


.job-card-footer {

    display: flex;

    align-items: center;

    justify-content: space-between;

    margin-top: 15px;

    font-size: 12px;

    color: #9ca3af;

}


.action-btn,
.delete-btn {

    border: none;

    padding: 7px 12px;

    border-radius: 6px;

    font-size: 12px;

    cursor: pointer;

    margin-left: 6px;

}


.action-btn {

    background: #eef2ff;

    color: #4338ca;

}


.delete-btn {

    background: #fee2e2;

    color: #b91c1c;

}


.empty-card {

    background: white;

    border-radius: 12px;

    padding: 60px 30px;

    text-align: center;

    box-shadow:
        0 2px 10px rgba(0,
            0,
            0,
            0.05);

}


.empty-icon {

    font-size: 42px;

}


.empty-card h3 {

    margin:
        15px 0 5px;

    color: #1f2937;

}


.empty-card p {

    color: #6b7280;

    margin-bottom: 20px;

}


.state-message {

    text-align: center;

    padding: 50px;

    color: #6b7280;

}


.error-message {

    background: #fee2e2;

    color: #b91c1c;

    padding: 12px 15px;

    border-radius: 8px;

    margin-bottom: 20px;

}

.job-description :deep(ul) {
    padding-left: 25px;
}

.job-description :deep(ol) {
    padding-left: 25px;
}

.job-description :deep(strong) {
    font-weight: 700;
}

.job-description :deep(u) {
    text-decoration: underline;
}

.status-wrapper {
    position: relative;
}

.status-badge {
    border: none;
    cursor: pointer;
    display: flex;
    align-items: center;
    gap: 5px;
}

.status-arrow {
    font-size: 11px;
}

.status-dropdown {
    position: absolute;
    top: calc(100% + 6px);
    right: 0;
    min-width: 140px;
    background: white;
    border: 1px solid #e5e7eb;
    border-radius: 8px;
    box-shadow: 0 6px 20px rgba(0, 0, 0, 0.12);
    padding: 5px;
    z-index: 100;
}

.status-option {
    width: 100%;
    border: none;
    background: transparent;
    text-align: left;
    padding: 9px 12px;
    border-radius: 6px;
    cursor: pointer;
    font-size: 13px;
}

.status-option:hover {
    background: #f3f4f6;
}

.status-option.active {
    font-weight: 600;
}

@media (max-width: 700px) {

    .page-content {

        padding: 20px;

    }

    .page-header {

        align-items: flex-start;

        flex-direction: column;

        gap: 15px;

    }

    .jobs-grid {

        grid-template-columns: 1fr;

    }

}
</style>