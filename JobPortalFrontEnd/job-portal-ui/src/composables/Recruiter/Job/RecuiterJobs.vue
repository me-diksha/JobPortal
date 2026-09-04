<script setup lang="ts">
import { onMounted, ref } from "vue";
import { GetRecruiterJobs } from "@/composables/Job/UseGetRecruiterJob";
import type { Job } from "@/types/Job";

const jobs = ref<Job[]>([]);
const loading = ref(false);
const error = ref("");

const loadJobs = async () => {

    try {

        loading.value = true;

        const response =
            await GetRecruiterJobs();

        jobs.value =
            response.data;

    } catch (err: any) {

        console.error(
            "Error loading jobs:",
            err
        );

        error.value =
            "Failed to load jobs";

    } finally {

        loading.value = false;

    }
};

onMounted(() => {
    loadJobs();
});
</script>

<template>

    <div>

        <h2>My Jobs</h2>

        <p v-if="loading">
            Loading jobs...
        </p>

        <p v-if="error">
            {{ error }}
        </p>

     <div
    v-for="job in jobs"
    :key="job.id"
    class="job-card"
>

    <h3>
        {{ job.title }}
    </h3>

    <p v-if="job.companyName">
        Company: {{ job.companyName }}
    </p>

    <p v-if="job.location">
        Location: {{ job.location }}
    </p>

    <p v-if="job.employmentType">
        Employment Type: {{ job.employmentType }}
    </p>

    <p v-if="job.experienceLevel">
        Experience: {{ job.experienceLevel }}
    </p>

    <p
        v-if="
            job.minSalary !== undefined ||
            job.maxSalary !== undefined
        "
    >
        Salary:
        {{ job.minSalary ?? "-" }}
        -
        {{ job.maxSalary ?? "-" }}
    </p>

    <p>
        Status:
        {{ job.status }}
    </p>

    <p v-if="job.deadline">
        Deadline:
        {{ job.deadline }}
    </p>

</div>  

    </div>

</template>