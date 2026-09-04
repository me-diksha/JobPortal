<script setup lang="ts">

import {
    onMounted,
    reactive,
    ref
} from "vue";

import { CreateJob } from "@/composables/Job/UseCreateJob";
import { GetAllEmploymentType } from "@/composables/Common/UseGetAllEmploymentType";

import type {
    JobRequest
} from "@/types/Job";

import type {
    EmploymentType
} from "@/types/EmploymentType";


const loading = ref(false);

const loadingEmploymentTypes =
    ref(false);

const error = ref("");

const employmentTypes =
    ref<EmploymentType[]>([]);


const form = reactive<JobRequest>({
    title: "",
    description: "",
    location: "",
    refEmploymentType: undefined,
    experienceLevel: "",
    minSalary: undefined,
    maxSalary: undefined,
    deadline: undefined
});


const loadEmploymentTypes = async () => {

    try {

        loadingEmploymentTypes.value = true;

        const response =
            await GetAllEmploymentType();

        employmentTypes.value =
            response.data;

    } catch (err) {

        console.error(
            "Error loading employment types:",
            err
        );

        error.value =
            "Failed to load employment types";

    } finally {

        loadingEmploymentTypes.value = false;

    }
};


const submitJob = async () => {

    try {

        loading.value = true;

        error.value = "";


        const payload: JobRequest = {

            title:
                form.title.trim(),

            description:
                form.description.trim(),

            location:
                form.location?.trim() || undefined,

            refEmploymentType:
                form.refEmploymentType || undefined,

            experienceLevel:
                form.experienceLevel || undefined,

            minSalary:
                form.minSalary,

            maxSalary:
                form.maxSalary,

            deadline:
                form.deadline || undefined
        };


        const response =
            await CreateJob(payload);


        console.log(
            "Job created:",
            response.data
        );


        alert(
            "Job created successfully"
        );


    } catch (err: any) {

        console.error(
            "Error creating job:",
            err
        );

        error.value =
            err.response?.data?.message ||
            "Failed to create job";

    } finally {

        loading.value = false;

    }
};


onMounted(() => {

    loadEmploymentTypes();

});

</script>


<template>

    <div class="create-job">

        <h2>Create Job</h2>


        <p
            v-if="error"
            class="error"
        >
            {{ error }}
        </p>


        <form
            @submit.prevent="submitJob"
        >


            <!-- Job Title -->

            <div>

                <label>
                    Job Title
                </label>

                <input
                    v-model="form.title"
                    type="text"
                    required
                />

            </div>


            <!-- Description -->

            <div>

                <label>
                    Description
                </label>

                <textarea
                    v-model="form.description"
                    rows="6"
                    required
                ></textarea>

            </div>


            <!-- Location -->

            <div>

                <label>
                    Location
                </label>

                <input
                    v-model="form.location"
                    type="text"
                />

            </div>


            <!-- Employment Type -->

            <div>

                <label>
                    Employment Type
                </label>

                <select
                    v-model="form.refEmploymentType"
                    :disabled="loadingEmploymentTypes"
                >

                    <option
                        :value="undefined"
                    >
                        {{
                            loadingEmploymentTypes
                                ? "Loading..."
                                : "Select Employment Type"
                        }}
                    </option>


                    <option
                        v-for="type in employmentTypes"
                        :key="type.id"
                        :value="type.id"
                    >
                        {{ type.description }}
                    </option>

                </select>

            </div>


            <!-- Experience -->

            <div>

                <label>
                    Experience Level
                </label>

                <select
                    v-model="form.experienceLevel"
                >

                    <option value="">
                        Select Experience Level
                    </option>

                    <option value="Fresher">
                        Fresher
                    </option>

                    <option value="0-2 Years">
                        0-2 Years
                    </option>

                    <option value="2-5 Years">
                        2-5 Years
                    </option>

                    <option value="5+ Years">
                        5+ Years
                    </option>

                </select>

            </div>


            <!-- Minimum Salary -->

            <div>

                <label>
                    Minimum Salary
                </label>

                <input
                    v-model.number="form.minSalary"
                    type="number"
                    min="0"
                />

            </div>


            <!-- Maximum Salary -->

            <div>

                <label>
                    Maximum Salary
                </label>

                <input
                    v-model.number="form.maxSalary"
                    type="number"
                    min="0"
                />

            </div>


            <!-- Deadline -->

            <div>

                <label>
                    Application Deadline
                </label>

                <input
                    v-model="form.deadline"
                    type="date"
                />

            </div>


            <!-- Submit -->

            <button
                type="submit"
                :disabled="loading"
            >

                {{
                    loading
                        ? "Creating..."
                        : "Create Job"
                }}

            </button>


        </form>

    </div>

</template>