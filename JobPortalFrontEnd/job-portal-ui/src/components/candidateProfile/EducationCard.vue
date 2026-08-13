<script setup lang="ts">
import type { CandidateEducation } from "@/types/candidate";

defineProps<{
    educations: CandidateEducation[];
}>();

const emit = defineEmits<{
    edit: [education: CandidateEducation];
    delete: [id: number];
}>();
</script>

<template>
    <div class="education-card">

        <div class="card-header">
            <h2>Education</h2>

            <button class="add-btn">
                + Add Education
            </button>
        </div>

        <div v-for="education in educations" :key="education.id" class="education-block">

            <div class="education-content">

                <div class="education-main">

                    <h3>{{ education.degree }}</h3>

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
                            {{ education.startYear }} - {{ education.endYear }}
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

                    <button type="button" @click="emit('delete', education.id)">
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
</style>