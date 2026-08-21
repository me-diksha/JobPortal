<script setup lang="ts">
import type { CandidateEducation } from "@/types/candidate";
import type { CandidateEducationRequest } from "@/types/candidate";
import { ref } from "vue";
import { DeleteCandidateEducation } from "@/composables/CandidateEducation/UseDeleteCandidateEducation";
import { AddCandidateEducation } from "@/composables/CandidateEducation/UseAddCandidateEducation";
defineProps<{
    educations: CandidateEducation[];
}>();

const emit = defineEmits<{
    edit: [education: CandidateEducation];
    delete: [id: number];
    added: [];
}>();
const isAdding = ref(false);
const isSaving = ref(false);
const newEducation = ref<CandidateEducationRequest>({
    instituteName: "",
    degree: "",
    fieldOfStudy: "",
    startYear: 0,
    endYear: 0,
    percentage: 0
});
const errors = ref({
    instituteName: "",
    degree: "",
    percentage: "",
    startYear: "",
    endYear: ""
});

const startAdd = () => {

    newEducation.value = {
        instituteName: "",
        degree: "",
        fieldOfStudy: "",
        startYear: 0,
        endYear: 0,
        percentage: 0
    };

    isAdding.value = true;

};
const cancelAdd = () => {
    isAdding.value = false;
};

const saveEducation = async () => {

    // Clear previous errors
    errors.value = {
        instituteName: "",
        degree: "",
        percentage: "",
        startYear: "",
        endYear: ""
    };

    let isValid = true;

    if (!newEducation.value.instituteName.trim()) {
        errors.value.instituteName = "Institute Name is required";
        isValid = false;
    }

    if (!newEducation.value.degree.trim()) {
        errors.value.degree = "Degree is required";
        isValid = false;
    }

    if (!newEducation.value.percentage) {
        errors.value.percentage = "Percentage is required";
        isValid = false;
    }

    if (!newEducation.value.startYear) {
        errors.value.startYear = "Start Year is required";
        isValid = false;
    }

    if (!newEducation.value.endYear) {
        errors.value.endYear = "End Year is required";
        isValid = false;
    }

    // Don't call API if validation failed
    if (!isValid) {
        return;
    }
    try {

        isSaving.value = true;

        await AddCandidateEducation(newEducation.value);

        isAdding.value = false;

        // Tell parent that education was added
        emit("added");

    } catch (error) {

        console.error("Error adding education:", error);

    } finally {

        isSaving.value = false;

    }
};
const deleteEducation = async (id: number) => {

    try {

        await DeleteCandidateEducation(id);

        // Tell parent to remove it from UI
        emit("delete", id);

    } catch (error) {

        console.error("Error deleting education:", error);

    }
};
</script>

<template>

    <div class="education-card">

        <!-- HEADER -->
        <div class="card-header">

            <h2>Education</h2>

            <button v-if="!isAdding" class="add-btn" @click="startAdd">
                + Add Education
            </button>

        </div>


        <!-- ADD EDUCATION FORM -->
        <div v-if="isAdding" class="add-form">

            <div class="form-row">

                <div class="form-group">
                    <label>Institute Name</label>

                    <input v-model="newEducation.instituteName" type="text" placeholder="Enter institute name" />
                    <span v-if="errors.instituteName" class="error-message">
                        {{ errors.instituteName }}
                    </span>
                </div>


                <div class="form-group">
                    <label>Degree</label>

                    <input v-model="newEducation.degree" type="text" placeholder="Enter degree" />
                    <span v-if="errors.degree" class="error-message">
                        {{ errors.degree }}
                    </span>
                </div>

            </div>


            <div class="form-row">

                <div class="form-group">
                    <label>Field of Study</label>

                    <input v-model="newEducation.fieldOfStudy" type="text" placeholder="Enter field of study" />
                </div>


                <div class="form-group">
                    <label>Percentage</label>

                    <input v-model.number="newEducation.percentage" type="number" placeholder="Enter percentage" />
                    <span v-if="errors.percentage" class="error-message">
                        {{ errors.percentage }}
                    </span>
                </div>

            </div>


            <div class="form-row">

                <div class="form-group">
                    <label>Start Year</label>

                    <input v-model.number="newEducation.startYear" type="number" placeholder="Start year" />
                    <span v-if="errors.startYear" class="error-message">
                        {{ errors.startYear }}
                    </span>
                </div>


                <div class="form-group">
                    <label>End Year</label>

                    <input v-model.number="newEducation.endYear" type="number" placeholder="End year" />
                    <span v-if="errors.endYear" class="error-message">
                        {{ errors.endYear }}
                    </span>
                </div>

            </div>


            <div class="form-actions">

                <button type="button" class="cancel-btn" @click="cancelAdd">
                    Cancel
                </button>

                <button type="button" class="save-btn" :disabled="isSaving" @click="saveEducation">
                    {{ isSaving ? "Saving..." : "Save Education" }}
                </button>

            </div>

        </div>


        <!-- EMPTY STATE -->
        <div v-if="!isAdding && educations.length === 0" class="empty-state">

            <div class="empty-icon">
                💡
            </div>

            <h3>
                No Education added yet
            </h3>

            <p>
                Add your Education to help recruiters understand your expertise.
            </p>

        </div>


        <!-- EDUCATION LIST -->
        <div v-for="education in educations" :key="education.id" class="education-block">

            <div class="education-content">

                <div class="education-main">

                    <h3>
                        {{ education.degree }}
                    </h3>

                    <p class="institute">
                        {{ education.instituteName }}
                    </p>

                    <p v-if="education.fieldOfstudy" class="field">
                        {{ education.fieldOfstudy }}
                    </p>

                </div>


                <div class="education-info">

                    <div>

                        <span>Duration</span>

                        <strong>
                            {{ education.startYear }} -
                            {{ education.endYear }}
                        </strong>

                    </div>


                    <div>

                        <span>Percentage</span>

                        <strong>
                            {{ education.percentage }}%
                        </strong>

                    </div>

                </div>


                <div class="actions">

                    <button type="button" @click="emit('edit', education)">
                        Edit
                    </button>


                    <button type="button" @click="deleteEducation(education.id)">
                        Delete
                    </button>

                </div>

            </div>

        </div>

    </div>

</template>


<style scoped>
.education-card {
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

.education-block {
    padding: 18px 0;
    border-top: 1px solid #e5e7eb;
}

.education-content {
    display: flex;
    align-items: center;
    justify-content: space-between;
    gap: 30px;
}

.education-main {
    flex: 1;
}

.education-main h3 {
    margin: 0 0 6px;
    font-size: 17px;
}

.institute {
    margin: 0;
    color: #555;
    font-size: 14px;
}

.field {
    margin: 5px 0 0;
    color: #777;
    font-size: 13px;
}

.education-info {
    display: flex;
    gap: 35px;
}

.education-info div {
    display: flex;
    flex-direction: column;
    gap: 5px;
}

.education-info span {
    font-size: 12px;
    color: #888;
}

.education-info strong {
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

.form-group input {
    padding: 9px 11px;
    border: 1px solid #d1d5db;
    border-radius: 6px;
    outline: none;
    font-size: 14px;
}

.form-group input:focus {
    border-color: rgb(24, 46, 107);
}

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

.actions button:first-child {
    color: rgb(24, 46, 107);
}

.actions button:last-child {
    color: #dc2626;
}
.error-message {
    color: #dc2626;
    font-size: 12px;
    margin-top: 2px;
}
</style>