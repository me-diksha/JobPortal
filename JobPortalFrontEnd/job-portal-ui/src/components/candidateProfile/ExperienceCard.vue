<script setup lang="ts">
import type { CandidateExperience } from "@/types/candidate";
import type { CandidateExperienceRequest } from "@/types/candidate";
import { ref } from "vue";

// Replace these imports with your actual API composables
import { AddCandidateExperience } from "@/composables/CandidateExperience/UseAddCandidatExperience";
import { DeleteCandidateExperience } from "@/composables/CandidateExperience/UseDeleteCandidateExperience";
import { UpdateCandidateExperience } from "@/composables/CandidateExperience/UseUpdateCandidateExperience";
defineProps<{
    experiences: CandidateExperience[];
}>();

const emit = defineEmits<{
    edit: [experience: CandidateExperience];
    delete: [id: number];
    added: [];
}>();

// --------------------
// ADD / EDIT
// --------------------

const isAdding = ref(false);
const isEditing = ref(false);
const isSaving = ref(false);

const editingExperienceId = ref<number | null>(null);

const newExperience = ref<CandidateExperienceRequest>({
    companyName: "",
    designation: "",
    startDate: "",
    endDate: undefined,
    currentlyWorking: false,
    description: ""
});

// --------------------
// VALIDATION
// --------------------

const errors = ref({
    companyName: "",
    designation: "",
    startDate: "",
    endDate: ""
});

// --------------------
// START ADD
// --------------------

const startAdd = () => {

    newExperience.value = {
        companyName: "",
        designation: "",
        startDate: "",
        endDate: undefined,
        currentlyWorking: false,
        description: ""
    };

    errors.value = {
        companyName: "",
        designation: "",
        startDate: "",
        endDate: ""
    };

    isEditing.value = false;
    editingExperienceId.value = null;

    isAdding.value = true;
};

// --------------------
// CANCEL
// --------------------

const cancelAdd = () => {

    isAdding.value = false;
    isEditing.value = false;
    editingExperienceId.value = null;

};

// --------------------
// EDIT
// --------------------

const editExperience = (experience: CandidateExperience) => {

    editingExperienceId.value = experience.id;

    newExperience.value = {
        companyName: experience.companyName,
        designation: experience.designation,
        startDate: experience.startDate,
        endDate: experience.endDate || undefined,
        currentlyWorking: experience.currentlyWorking,
        description: experience.description ?? ""
    };

    errors.value = {
        companyName: "",
        designation: "",
        startDate: "",
        endDate: ""
    };

    isEditing.value = true;
    isAdding.value = true;
};

// --------------------
// SAVE ADD / EDIT
// --------------------

const saveExperience = async () => {

    // Clear previous errors
    errors.value = {
        companyName: "",
        designation: "",
        startDate: "",
        endDate: ""
    };

    let isValid = true;

    // Company Name
    if (!newExperience.value.companyName.trim()) {
        errors.value.companyName = "Company Name is required";
        isValid = false;
    }

    // Designation
    if (!newExperience.value.designation.trim()) {
        errors.value.designation = "Designation is required";
        isValid = false;
    }

    // Start Date
    if (!newExperience.value.startDate) {
        errors.value.startDate = "Start Date is required";
        isValid = false;
    }

    // End Date
    // End date is required only if currently working is false
    if (
        !newExperience.value.currentlyWorking &&
        !newExperience.value.endDate
    ) {
        errors.value.endDate = "End Date is required";
        isValid = false;
    }

    // Don't call API if validation failed
    if (!isValid) {
        return;
    }

    try {

        isSaving.value = true;

        const payload: CandidateExperienceRequest = {
            ...newExperience.value,

            companyName:
                newExperience.value.companyName.trim(),

            designation:
                newExperience.value.designation.trim(),

            description:
                newExperience.value.description?.trim()
                    ? newExperience.value.description.trim()
                    : undefined,

            endDate:
                newExperience.value.currentlyWorking
                    ? undefined
                    : newExperience.value.endDate
        };

        // UPDATE
        if (
            isEditing.value &&
            editingExperienceId.value !== null
        ) {

            await UpdateCandidateExperience(
                editingExperienceId.value,
                payload
            );

        }

        // ADD
        else {

            await AddCandidateExperience(
                payload
            );
        }

        // Close form
        isAdding.value = false;
        isEditing.value = false;
        editingExperienceId.value = null;

        // Tell parent to refresh experience list
        emit("added");

    } catch (error) {

        console.error(
            "Error saving experience:",
            error
        );

    } finally {

        isSaving.value = false;

    }
};

// --------------------
// DELETE
// --------------------

const deleteExperience = async (id: number) => {

    try {

        await DeleteCandidateExperience(id);

        // Tell parent to remove it from UI
        emit("delete", id);

    } catch (error) {

        console.error(
            "Error deleting experience:",
            error
        );

    }
};
</script>


<template>

    <div class="experience-card">

        <!-- HEADER -->
        <div class="card-header">

            <h2>Experience</h2>

            <button
                v-if="!isAdding"
                class="add-btn"
                @click="startAdd"
            >
                + Add Experience
            </button>

        </div>


        <!-- ADD / EDIT FORM -->
        <div
            v-if="isAdding"
            class="add-form"
        >

            <!-- Company + Designation -->
            <div class="form-row">

                <div class="form-group">

                    <label>Company Name</label>

                    <input
                        v-model="newExperience.companyName"
                        type="text"
                        placeholder="Enter company name"
                    />

                    <span
                        v-if="errors.companyName"
                        class="error-message"
                    >
                        {{ errors.companyName }}
                    </span>

                </div>


                <div class="form-group">

                    <label>Designation</label>

                    <input
                        v-model="newExperience.designation"
                        type="text"
                        placeholder="Enter designation"
                    />

                    <span
                        v-if="errors.designation"
                        class="error-message"
                    >
                        {{ errors.designation }}
                    </span>

                </div>

            </div>


            <!-- Start + End Date -->
            <div class="form-row">

                <div class="form-group">

                    <label>Start Date</label>

                    <input
                        v-model="newExperience.startDate"
                        type="date"
                    />

                    <span
                        v-if="errors.startDate"
                        class="error-message"
                    >
                        {{ errors.startDate }}
                    </span>

                </div>


                <div class="form-group">

                    <label>End Date</label>

                    <input
                        v-model="newExperience.endDate"
                        type="date"
                        :disabled="newExperience.currentlyWorking"
                    />

                    <span
                        v-if="errors.endDate"
                        class="error-message"
                    >
                        {{ errors.endDate }}
                    </span>

                </div>

            </div>


            <!-- Currently Working -->
            <div class="form-group checkbox-group">

                <label>

                    <input
                        v-model="newExperience.currentlyWorking"
                        type="checkbox"
                    />

                    Currently Working Here

                </label>

            </div>


            <!-- Description -->
            <div class="form-group">

                <label>Description</label>

                <textarea
                    v-model="newExperience.description"
                    placeholder="Enter job description"
                    rows="4"
                ></textarea>

            </div>


            <!-- Buttons -->
            <div class="form-actions">

                <button
                    type="button"
                    class="cancel-btn"
                    @click="cancelAdd"
                >
                    Cancel
                </button>


                <button
                    type="button"
                    class="save-btn"
                    :disabled="isSaving"
                    @click="saveExperience"
                >
                    {{
                        isSaving
                            ? (isEditing ? "Updating..." : "Saving...")
                            : (isEditing
                                ? "Update Experience"
                                : "Save Experience")
                    }}
                </button>

            </div>

        </div>


        <!-- EMPTY STATE -->
        <div
            v-if="!isAdding && experiences.length === 0"
            class="empty-state"
        >

            <div class="empty-icon">
                💡
            </div>

            <h3>
                No Experience added yet
            </h3>

            <p>
                Add your Experience to help recruiters understand your expertise.
            </p>

        </div>


        <!-- EXPERIENCE LIST -->
        <div
            v-for="experience in experiences"
            :key="experience.id"
            class="experience-block"
        >

            <div class="experience-content">

                <!-- Main information -->
                <div class="experience-main">

                    <h3>
                        {{ experience.designation }}
                    </h3>

                    <p class="company">
                        {{ experience.companyName }}
                    </p>

                    <p
                        v-if="experience.description"
                        class="description"
                    >
                        {{ experience.description }}
                    </p>

                </div>


                <!-- Information -->
                <div class="experience-info">

                    <div>

                        <span>Duration</span>

                        <strong>

                            {{
                                new Date(
                                    experience.startDate
                                ).toLocaleDateString()
                            }}

                            -

                            {{
                                experience.currentlyWorking
                                    ? "Present"
                                    : experience.endDate
                                        ? new Date(
                                            experience.endDate
                                        ).toLocaleDateString()
                                        : ""
                            }}

                        </strong>

                    </div>


                    <div>

                        <span>Status</span>

                        <strong>
                            {{
                                experience.currentlyWorking
                                    ? "Currently Working"
                                    : "Previous"
                            }}
                        </strong>

                    </div>

                </div>


                <!-- Actions -->
                <div class="actions">

                    <button
                        type="button"
                        @click="editExperience(experience)"
                    >
                        Edit
                    </button>


                    <button
                        type="button"
                        @click="deleteExperience(experience.id)"
                    >
                        Delete
                    </button>

                </div>

            </div>

        </div>

    </div>

</template>


<style scoped>

.experience-card {
    background: white;
    border-radius: 12px;
    padding: 24px;
    margin-bottom: 20px;
    border: 1px solid #e5e7eb;
    margin-top: 20px;
}

.card-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-bottom: 20px;
}

.card-header h2 {
    margin: 0;
    color: rgb(24, 46, 107);
}

.add-btn {
    border: none;
    background: rgb(24, 46, 107);
    color: white;
    padding: 8px 14px;
    border-radius: 6px;
    cursor: pointer;
}

.add-btn:hover {
    background: #334f9c;
}

.experience-block {
    padding: 18px 0;
    border-top: 1px solid #e5e7eb;
}

.experience-content {
    display: flex;
    align-items: center;
    justify-content: space-between;
    gap: 30px;
}

.experience-main {
    flex: 1;
}

.experience-main h3 {
    margin: 0 0 6px;
    font-size: 17px;
}

.company {
    margin: 0;
    color: #555;
    font-size: 14px;
}

.description {
    margin: 5px 0 0;
    color: #777;
    font-size: 13px;
}

.experience-info {
    display: flex;
    gap: 35px;
}

.experience-info div {
    display: flex;
    flex-direction: column;
    gap: 5px;
}

.experience-info span {
    font-size: 12px;
    color: #888;
}

.experience-info strong {
    font-size: 14px;
}

.actions {
    display: flex;
    gap: 8px;
}

.actions button {
    border: none;
    background: transparent;
    cursor: pointer;
    font-size: 14px;
}

.actions button:first-child {
    color: rgb(24, 46, 107);
}

.actions button:last-child {
    color: #dc2626;
}


/* EMPTY STATE */

.empty-state {
    text-align: center;
    padding: 35px;
    color: #6b7280;
}

.empty-icon {
    font-size: 35px;
}

.empty-state h3 {
    color: #374151;
}

.empty-state p {
    font-size: 14px;
}


/* FORM */

.add-form {
    border-top: 1px solid #e5e7eb;
    padding-top: 20px;
    margin-bottom: 20px;
}

.form-row {
    display: flex;
    gap: 20px;
    margin-bottom: 15px;
}

.form-group {
    flex: 1;
    display: flex;
    flex-direction: column;
    gap: 6px;
}

.form-group label {
    font-size: 13px;
    font-weight: 600;
    color: #374151;
}

.form-group input,
.form-group textarea {
    padding: 9px 11px;
    border: 1px solid #d1d5db;
    border-radius: 6px;
    outline: none;
    font-size: 14px;
    font-family: inherit;
}

.form-group input:focus,
.form-group textarea:focus {
    border-color: rgb(24, 46, 107);
}

.checkbox-group {
    margin-bottom: 15px;
}

.checkbox-group label {
    display: flex;
    flex-direction: row;
    align-items: center;
    gap: 8px;
}

.checkbox-group input {
    width: auto;
}


/* BUTTONS */

.form-actions {
    display: flex;
    justify-content: flex-end;
    gap: 10px;
    margin-top: 20px;
}

.save-btn {
    border: none;
    background: rgb(24, 46, 107);
    color: white;
    padding: 9px 16px;
    border-radius: 6px;
    cursor: pointer;
}

.save-btn:hover {
    background: #334f9c;
}

.save-btn:disabled {
    opacity: 0.6;
    cursor: not-allowed;
}

.cancel-btn {
    border: 1px solid #d1d5db;
    background: white;
    padding: 9px 16px;
    border-radius: 6px;
    cursor: pointer;
}

.cancel-btn:hover {
    background: #f3f4f6;
}


/* ERROR */

.error-message {
    color: #dc2626;
    font-size: 12px;
    margin-top: 2px;
}

</style>