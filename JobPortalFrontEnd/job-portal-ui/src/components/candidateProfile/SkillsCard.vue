<script setup lang="ts">

import { ref } from "vue";
import type { CandidateSkill } from "@/types/candidate";


const props = defineProps<{
    skills: CandidateSkill[]
}>();


const emit = defineEmits<{
    update: [skills: CandidateSkill[]]
}>();


const isAdding = ref(false);

const editingId = ref<number | null>(null);


const newSkill = ref<CandidateSkill>({
    id: 0,
    skillId: 0,
    skillName: "",
    experienceYears: 0
});


const editSkill = ref<CandidateSkill>({
    id: 0,
    skillId: 0,
    skillName: "",
    experienceYears: 0
});


/* =========================
   ADD SKILL
========================= */

const startAdd = () => {

    newSkill.value = {
        id: 0,
        skillId: 0,
        skillName: "",
        experienceYears: 0
    };

    isAdding.value = true;

};


/* =========================
   CANCEL ADD
========================= */

const cancelAdd = () => {

    isAdding.value = false;

};


/* =========================
   SAVE NEW SKILL
========================= */

const saveNewSkill = () => {

    if (!newSkill.value.skillName.trim()) {

        return;

    }


    const skill: CandidateSkill = {

        ...newSkill.value,

        id: Date.now()

    };


    emit("update", [
        ...props.skills,
        skill
    ]);


    isAdding.value = false;

};


/* =========================
   EDIT SKILL
========================= */

const startEdit = (skill: CandidateSkill) => {

    editSkill.value = {
        ...skill
    };

    editingId.value = skill.id;

};


/* =========================
   CANCEL EDIT
========================= */

const cancelEdit = () => {

    editingId.value = null;

};


/* =========================
   SAVE EDIT
========================= */

const saveEdit = () => {

    const updatedSkills = props.skills.map(skill => {

        if (skill.id === editSkill.value.id) {

            return {
                ...editSkill.value
            };

        }

        return skill;

    });


    emit("update", updatedSkills);

    editingId.value = null;

};


/* =========================
   DELETE
========================= */

const deleteSkill = (id: number) => {

    const updatedSkills = props.skills.filter(
        skill => skill.id !== id
    );

    emit("update", updatedSkills);

};

</script>


<template>

<div class="section">


    <!-- =========================
         HEADER
    ========================== -->

    <div class="section-header">

        <div>

            <h2>Skills</h2>

            <p class="section-description">

                Highlight your technical and professional skills.

            </p>

        </div>


        <button
            v-if="!isAdding"
            class="add-btn"
            @click="startAdd"
        >

            + Add Skill

        </button>

    </div>



    <!-- =========================
         ADD SKILL
    ========================== -->

    <div
        v-if="isAdding"
        class="skill-form"
    >

        <h3>
            Add Skill
        </h3>


        <div class="form-grid">


            <div>

                <label>Skill Name</label>

                <input
                    v-model="newSkill.skillName"
                    placeholder="e.g. C#, Vue.js"
                />

            </div>


            <div>

                <label>Experience</label>

                <input
                    type="number"
                    min="0"
                    v-model.number="newSkill.experienceYears"
                    placeholder="Years"
                />

            </div>

        </div>


        <div class="actions">

            <button
                class="cancel-btn"
                @click="cancelAdd"
            >

                Cancel

            </button>


            <button
                class="save-btn"
                @click="saveNewSkill"
            >

                Add Skill

            </button>

        </div>

    </div>



    <!-- =========================
         SKILL LIST
    ========================== -->

    <div
        v-if="props.skills.length"
        class="skills-list"
    >


        <!-- =========================
             SKILL CARD
        ========================== -->

        <div
            v-for="skill in props.skills"
            :key="skill.id"
            class="skill-card"
        >


            <!-- VIEW MODE -->

            <template v-if="editingId !== skill.id">



                <div class="skill-info">

                    <h3>

                        {{ skill.skillName }}

                    </h3>


                    <p>

                        {{ skill.experienceYears }}

                        {{ skill.experienceYears === 1
                            ? "year"
                            : "years"
                        }}

                        experience

                    </p>

                </div>


                <div class="skill-actions">

                    <button
                        class="icon-btn"
                        @click="startEdit(skill)"
                        title="Edit"
                    >

                        ✏️

                    </button>


                    <button
                        class="icon-btn delete"
                        @click="deleteSkill(skill.id)"
                        title="Delete"
                    >

                        🗑️

                    </button>

                </div>

            </template>



            <!-- EDIT MODE -->

            <template v-else>

                <div class="edit-skill-form">

                    <div>

                        <label>Skill Name</label>

                        <input
                            v-model="editSkill.skillName"
                        />

                    </div>


                    <div>

                        <label>Experience Years</label>

                        <input
                            type="number"
                            min="0"
                            v-model.number="editSkill.experienceYears"
                        />

                    </div>


                    <div class="actions">

                        <button
                            class="cancel-btn"
                            @click="cancelEdit"
                        >

                            Cancel

                        </button>


                        <button
                            class="save-btn"
                            @click="saveEdit"
                        >

                            Save

                        </button>

                    </div>

                </div>

            </template>

        </div>

    </div>



    <!-- =========================
         EMPTY STATE
    ========================== -->

    <div
        v-else-if="!isAdding"
        class="empty-state"
    >

        <div class="empty-icon">
            💡
        </div>

        <h3>
            No skills added yet
        </h3>

        <p>
            Add your skills to help recruiters understand your expertise.
        </p>

    </div>


</div>

</template>


<style scoped>

.section {

    background: white;

    margin-top: 25px;

    padding: 25px;

    border-radius: 15px;

    box-shadow: 0 2px 10px rgba(0, 0, 0, 0.04);

}


/* =========================
   HEADER
========================= */

.section-header {

    display: flex;

    justify-content: space-between;

    align-items: center;

    margin-bottom: 20px;

}


.section-header h2 {

    margin: 0;

    color: rgb(24, 46, 107);

}


.section-description {

    margin: 5px 0 0;

    color: #6b7280;

    font-size: 13px;

}


.add-btn {

    padding: 10px 16px;

    background: rgb(24, 46, 107);

    color: white;

    border: none;

    border-radius: 8px;

    cursor: pointer;

}


.add-btn:hover {

    background: #334f9c;

}


/* =========================
   SKILL LIST
========================= */

.skills-list {

    display: grid;

    grid-template-columns: 1fr 1fr;

    gap: 15px;

}


.skill-card {

    border: 1px solid #eee;

    border-radius: 12px;

    padding: 18px;

    display: flex;

    align-items: center;

    gap: 15px;

    transition: 0.2s;

}


.skill-card:hover {

    box-shadow: 0 4px 12px rgba(0, 0, 0, 0.06);

}


.skill-icon {

    width: 45px;

    height: 45px;

    border-radius: 10px;

    background: #e8edff;

    display: flex;

    align-items: center;

    justify-content: center;

    font-size: 22px;

}


.skill-info {

    flex: 1;

}


.skill-info h3 {

    margin: 0 0 5px;

    color: #1f2937;

}


.skill-info p {

    margin: 0;

    color: #6b7280;

    font-size: 13px;

}


/* =========================
   ACTIONS
========================= */

.skill-actions {

    display: flex;

    gap: 5px;

}


.icon-btn {

    width: 35px;

    height: 35px;

    border: none;

    background: #f5f7fb;

    border-radius: 7px;

    cursor: pointer;

}


.icon-btn:hover {

    background: #e8edff;

}


.icon-btn.delete:hover {

    background: #fee2e2;

}


/* =========================
   FORM
========================= */

.skill-form {

    background: #f8f9fc;

    border: 1px solid #eee;

    padding: 20px;

    border-radius: 12px;

    margin-bottom: 20px;

}


.skill-form h3 {

    margin-top: 0;

    color: rgb(24, 46, 107);

}


.form-grid {

    display: grid;

    grid-template-columns: 1fr 1fr;

    gap: 15px;

}


.skill-form label,
.edit-skill-form label {

    display: block;

    font-size: 13px;

    font-weight: 600;

    color: #6b7280;

    margin-bottom: 7px;

}


.skill-form input,
.edit-skill-form input {

    width: 100%;

    box-sizing: border-box;

    padding: 11px;

    border: 1px solid #ddd;

    border-radius: 8px;

    margin-bottom: 10px;

}


/* =========================
   EDIT FORM
========================= */

.edit-skill-form {

    width: 100%;

}


.actions {

    display: flex;

    justify-content: flex-end;

    gap: 10px;

    margin-top: 10px;

}


.cancel-btn {

    padding: 10px 18px;

    background: white;

    border: 1px solid #ddd;

    border-radius: 7px;

    cursor: pointer;

}


.save-btn {

    padding: 10px 18px;

    background: rgb(24, 46, 107);

    color: white;

    border: none;

    border-radius: 7px;

    cursor: pointer;

}


/* =========================
   EMPTY
========================= */

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


/* =========================
   RESPONSIVE
========================= */

@media (max-width: 800px) {

    .skills-list {

        grid-template-columns: 1fr;

    }

}


@media (max-width: 600px) {

    .form-grid {

        grid-template-columns: 1fr;

    }

    .section-header {

        align-items: flex-start;

        gap: 15px;

    }

}

</style>