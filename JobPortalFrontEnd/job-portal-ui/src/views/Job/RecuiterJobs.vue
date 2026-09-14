```vue
<script setup lang="ts">

import {
    onMounted,
    ref
} from "vue";
import { useToast } from "vue-toastification";
import {
    useRouter
} from "vue-router";

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
import { DeleteJob } from "@/composables/Job/UseDeleteJob";
import { HttpStatusCode } from "axios";
import { recruiterMenu,bottomMenu } from "@/constants/recruiterMenu";


/* =========================
   ROUTER
========================= */

const router = useRouter();


/* =========================
   JOB DATA
========================= */

const jobs =
    ref<Job[]>([]);

const loading =
    ref(false);

const error =
    ref("");

const toast = useToast();
/* =========================
   STATUS DATA
========================= */

const jobStatuses =
    ref<any[]>([]);

const openStatusJobId =
    ref<number | null>(null);

const statusLoading =
    ref(false);
const ischangeDelete = ref(false);
const selectedJob = ref<Job | null>(null);




/* =========================
   LOAD JOB STATUSES
========================= */

const loadJobStatuses =
    async () => {

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


/* =========================
   LOAD JOBS
========================= */

const loadJobs =
    async () => {

        try {

            loading.value = true;

            error.value = "";


            const response =
                await GetRecruiterJobs();


            jobs.value =
                response.data ?? [];

        }
        catch (err: any) {

            console.error(
                "Error loading jobs:",
                err
            );

            error.value =
                err.response?.data?.message ||
                "Failed to load jobs";

        }
        finally {

            loading.value = false;

        }

    };


/* =========================
   FORMAT SALARY
========================= */

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


/* =========================
   FORMAT DATE
========================= */

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


/* =========================
   EDIT JOB
========================= */

const editJob = (
    job: Job
) => {

    /*
     * Navigate to the SAME CreateJob.vue
     * but pass the job ID.
     */

    router.push({
        path: "/recruiter/jobs/create",
        query: {
            editId: job.id.toString()
        }
    });

};
const DeleteClicked = (job: Job) => {
    selectedJob.value = job;
    ischangeDelete.value = true;
};
const handleDelete = async() =>{
    if (!selectedJob.value) return;
    try{
         var id = selectedJob.value.id;
        const response =
                await DeleteJob(id);
        if(response.status== HttpStatusCode.Ok)   {
            toast.success("Job deleted successfully");
            ischangeDelete.value = false;
            selectedJob.value = null;
           await loadJobs();
        }   
    }catch(err){
        console.error("Can't delete",err);
    }
 

}
/* =========================
   STATUS DROPDOWN
========================= */

const toggleStatusDropdown = (
    jobId: number
) => {

    if (
        openStatusJobId.value ===
        jobId
    ) {

        openStatusJobId.value = null;

    }
    else {

        openStatusJobId.value =
            jobId;

    }

};


/* =========================
   CHANGE STATUS
========================= */

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


        await loadJobs();


        openStatusJobId.value =
            null;

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

        statusLoading.value =
            false;

    }

};


/* =========================
   STATUS CLASS
========================= */

const getStatusClass = (
    status?: string
) => {

    if (!status) {

        return "unknown";

    }


    return status
        .toLowerCase()
        .trim()
        .replace(/\s+/g, "-");

};


/* =========================
   CREATE JOB
========================= */

const createJob = () => {

    router.push(
        "/recruiter/jobs/create"
    );

};


/* =========================
   MOUNT
========================= */

onMounted(async () => {

    await loadJobs();

    await loadJobStatuses();

});

</script>


<template>

    <div class="dashboard">


        <!-- SIDEBAR -->

        <Sidebar
            companyName="Jobsy"
            slogan="Find Your Sea"
            :logo="logo"
            :menuItems="recruiterMenu"
            :bottomMenu="bottomMenu"
        />


        <!-- MAIN -->

        <section class="main">

            <div class="page-content">


                <!-- HEADER -->

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


                    <button
                        class="create-btn"
                        @click="createJob"
                    >
                        + Create Job
                    </button>

                </div>



                <!-- ERROR -->

                <div
                    v-if="error"
                    class="error-message"
                >
                    {{ error }}
                </div>



                <!-- LOADING -->

                <div
                    v-if="loading"
                    class="state-message"
                >
                    Loading jobs...
                </div>



                <!-- EMPTY -->

                <div
                    v-else-if="jobs.length === 0"
                    class="empty-card"
                >

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

                    <button
                        class="create-btn"
                        @click="createJob"
                    >
                        Create Your First Job
                    </button>

                </div>



                <!-- JOB GRID -->

                <div
                    v-else
                    class="jobs-grid"
                >


                    <!-- JOB CARD -->

                    <div
                        v-for="job in jobs"
                        :key="job.id"
                        class="job-card"
                    >


                        <!-- =========================
                             NORMAL VIEW
                        ========================== -->


                            <!-- CARD HEADER -->

                            <div class="job-card-header">

                                <div>

                                    <h2>
                                        {{ job.title }}
                                    </h2>

                                    <p
                                        v-if="
                                            job.companyName
                                        "
                                        class="company"
                                    >
                                        🏢
                                        {{
                                            job.companyName
                                        }}
                                    </p>

                                </div>



                                <!-- STATUS -->

                                <div
                                    class="status-wrapper"
                                >

                                    <button
                                        class="status-badge"
                                        :class="
                                            getStatusClass(
                                                job.status
                                            )
                                        "
                                        @click="
                                            toggleStatusDropdown(
                                                job.id
                                            )
                                        "
                                    >

                                        {{
                                            job.status ||
                                            "Draft"
                                        }}

                                        <span
                                            class="status-arrow"
                                        >
                                            ▾
                                        </span>

                                    </button>



                                    <!-- STATUS OPTIONS -->

                                    <div
                                        v-if="
                                            openStatusJobId ===
                                            job.id
                                        "
                                        class="status-dropdown"
                                    >

                                        <button
                                            v-for="
                                                status in jobStatuses
                                            "
                                            :key="status.id"
                                            class="status-option"
                                            :class="[
                                                getStatusClass(
                                                    status.description
                                                ),
                                                {
                                                    active:
                                                        status.description ===
                                                        job.status
                                                }
                                            ]"
                                            :disabled="
                                                statusLoading
                                            "
                                            @click="
                                                changeJobStatus(
                                                    job,
                                                    status.id
                                                )
                                            "
                                        >

                                            {{
                                                status.description
                                            }}

                                        </button>

                                    </div>

                                </div>

                            </div>



                            <!-- DESCRIPTION -->

                            <div
                                class="job-description"
                                v-html="
                                    job.description
                                "
                            ></div>



                            <!-- DETAILS -->

                            <div class="job-details">


                                <!-- LOCATION -->

                                <div
                                    v-if="
                                        job.location
                                    "
                                    class="detail"
                                >

                                    <span
                                        class="detail-label"
                                    >
                                        Location
                                    </span>

                                    <span>
                                        📍
                                        {{
                                            job.location
                                        }}
                                    </span>

                                </div>



                                <!-- EMPLOYMENT -->

                                <div
                                    v-if="
                                        job.employmentType
                                    "
                                    class="detail"
                                >

                                    <span
                                        class="detail-label"
                                    >
                                        Employment
                                    </span>

                                    <span>
                                        💼
                                        {{
                                            job.employmentType
                                        }}
                                    </span>

                                </div>



                                <!-- EXPERIENCE -->

                                <div
                                    v-if="
                                        job.experienceLevel
                                    "
                                    class="detail"
                                >

                                    <span
                                        class="detail-label"
                                    >
                                        Experience
                                    </span>

                                    <span>
                                        🎓
                                        {{
                                            job.experienceLevel
                                        }}
                                    </span>

                                </div>



                                <!-- SALARY -->

                                <div
                                    v-if="
                                        job.minSalary !== undefined ||
                                        job.maxSalary !== undefined
                                    "
                                    class="detail"
                                >

                                    <span
                                        class="detail-label"
                                    >
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



                                <!-- DEADLINE -->

                                <div
                                    v-if="
                                        job.deadline
                                    "
                                    class="detail"
                                >

                                    <span
                                        class="detail-label"
                                    >
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



                            <!-- FOOTER -->

                            <div
                                class="job-card-footer"
                            >

                                <div>

                                    <button
                                        class="action-btn"
                                        @click="
                                            editJob(job)
                                        "
                                    >
                                        Edit
                                    </button>

                                    <button
                                        class="delete-btn" @click ="DeleteClicked(job)"
                                    >
                                        Delete
                                    </button>

                                </div>

                            </div>

                        

                      

                    </div>

                </div>

            </div>

        </section>
        <div v-if="ischangeDelete" class="modal-overlay">
            <div class="success-modal">
                <div class="success-icon">
                    ⚠️
                </div>

                <h2>Are You Sure You Want to Delete this Job ?</h2>


                <button class="modal-btn" @click="handleDelete">
                    Yes
                </button>
                <button class ="modal-btn" @click="ischangeDelete=false">
                    Cancel
                </button>
            </div>
        </div>

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



/* =========================
   JOB GRID
========================= */

.jobs-grid {

    display: grid;

    grid-template-columns:
        repeat(
            auto-fit,
            minmax(340px, 1fr)
        );

    gap: 20px;

}


/* =========================
   JOB CARD
========================= */

.job-card {

    background: white;

    border-radius: 12px;

    padding: 22px;

    box-shadow:
        0 2px 10px
        rgba(
            0,
            0,
            0,
            0.05
        );

    transition:
        transform 0.2s,
        box-shadow 0.2s;

}


.job-card:hover {

    transform:
        translateY(-2px);

    box-shadow:
        0 6px 18px
        rgba(
            0,
            0,
            0,
            0.08
        );

}



/* =========================
   CARD HEADER
========================= */

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



/* =========================
   STATUS
========================= */

.status-wrapper {

    position: relative;

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


.status-arrow {

    font-size: 11px;

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



/* =========================
   STATUS DROPDOWN
========================= */

.status-dropdown {

    position: absolute;

    top: calc(100% + 6px);

    right: 0;

    min-width: 140px;

    background: white;

    border: 1px solid #e5e7eb;

    border-radius: 8px;

    box-shadow:
        0 6px 20px
        rgba(
            0,
            0,
            0,
            0.12
        );

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



/* =========================
   DESCRIPTION
========================= */

.job-description {

    margin: 18px 0;

    color: #6b7280;

    font-size: 14px;

    line-height: 1.6;

    display: -webkit-box;

    -webkit-line-clamp: 3;

    -webkit-box-orient: vertical;

    overflow: hidden;

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



/* =========================
   DETAILS
========================= */

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



/* =========================
   FOOTER
========================= */

.job-card-footer {

    display: flex;

    align-items: center;

    justify-content: space-between;

    margin-top: 15px;

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



/* =========================
   EDIT HEADER
========================= */

.edit-header {

    display: flex;

    align-items: flex-start;

    justify-content: space-between;

    margin-bottom: 20px;

}


.edit-header h2 {

    margin: 0;

    font-size: 20px;

    color: #1f2937;

}


.close-btn {

    border: none;

    background: #f3f4f6;

    color: #6b7280;

    width: 30px;

    height: 30px;

    border-radius: 50%;

    cursor: pointer;

}


.close-btn:hover {

    background: #e5e7eb;

}



/* =========================
   EDIT FORM
========================= */

.form-group {

    display: flex;

    flex-direction: column;

    gap: 6px;

    margin-bottom: 15px;

}


.form-group label {

    font-size: 13px;

    font-weight: 600;

    color: #374151;

}


.form-group input,
.form-group textarea,
.form-group select {

    width: 100%;

    box-sizing: border-box;

    border: 1px solid #d1d5db;

    border-radius: 7px;

    padding: 10px 12px;

    font-size: 14px;

    color: #1f2937;

    background: white;

    outline: none;

    font-family: inherit;

}


.form-group input:focus,
.form-group textarea:focus,
.form-group select:focus {

    border-color: #4f46e5;

    box-shadow:
        0 0 0 2px
        rgba(
            79,
            70,
            229,
            0.1
        );

}


.form-group textarea {

    resize: vertical;

    min-height: 150px;

    line-height: 1.5;

}


.form-group input:disabled {

    background: #f3f4f6;

    color: #6b7280;

    cursor: not-allowed;

}


.edit-row {

    display: grid;

    grid-template-columns:
        1fr 1fr;

    gap: 15px;

}


.edit-actions {

    display: flex;

    justify-content: flex-end;

    gap: 10px;

    margin-top: 20px;

    padding-top: 15px;

    border-top: 1px solid #eef0f4;

}


.cancel-btn,
.save-btn {

    border: none;

    border-radius: 7px;

    padding: 9px 16px;

    font-size: 13px;

    font-weight: 600;

    cursor: pointer;

}


.cancel-btn {

    background: #f3f4f6;

    color: #374151;

}


.cancel-btn:hover {

    background: #e5e7eb;

}


.save-btn {

    background: #4f46e5;

    color: white;

}


.save-btn:hover {

    background: #4338ca;

}


.save-btn:disabled {

    opacity: 0.6;

    cursor: not-allowed;

}



/* =========================
   EDIT ERROR
========================= */

.edit-error {

    background: #fee2e2;

    color: #b91c1c;

    padding: 10px 12px;

    border-radius: 7px;

    margin-bottom: 15px;

    font-size: 13px;

}



/* =========================
   EMPTY / STATES
========================= */

.empty-card {

    background: white;

    border-radius: 12px;

    padding: 60px 30px;

    text-align: center;

    box-shadow:
        0 2px 10px
        rgba(
            0,
            0,
            0,
            0.05
        );

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
/*modal */
.modal-overlay {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background: rgba(0, 0, 0, 0.5);

    display: flex;
    justify-content: center;
    align-items: center;

    z-index: 9999;
}

.success-modal {
    background: white;
    width: 400px;
    padding: 35px;
    border-radius: 12px;

    text-align: center;

    box-shadow: 0 10px 40px rgba(0, 0, 0, 0.2);

    animation: modalFadeIn 0.25s ease;
}

.success-modal h2 {
    margin: 15px 0 10px;
    color: rgb(24, 46, 107);
}

.success-modal p {
    color: #666;
    margin-bottom: 25px;
}

.success-icon {
    width: 70px;
    height: 70px;

    margin: 0 auto;

    border-radius: 50%;

    color: white;

    font-size: 42px;
    font-weight: bold;

    display: flex;
    align-items: center;
    justify-content: center;
}

.modal-btn {
    background-color: rgb(24, 46, 107);
    color: white;
    margin:10px;
    border: none;
    border-radius: 5px;

    padding: 10px 30px;

    cursor: pointer;
    font-size: 15px;
}

.modal-btn:hover {
    background: #334f9c;
}
@keyframes modalFadeIn {
    from {
        opacity: 0;
        transform: scale(0.9);
    }

    to {
        opacity: 1;
        transform: scale(1);
    }
}

/* =========================
   RESPONSIVE
========================= */

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


    .edit-row {

        grid-template-columns: 1fr;

    }

}

</style>
```
