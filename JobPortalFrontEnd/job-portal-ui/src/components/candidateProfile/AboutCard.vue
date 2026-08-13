<script setup lang="ts">

import { ref, watch } from "vue";

import type { CandidateProfile } from "@/types/candidate";


const props = defineProps<{
    profile: CandidateProfile
}>();


const emit = defineEmits<{
    update: [profile: CandidateProfile]
}>();


const isEditing = ref(false);


const editData = ref({
    headline: "",
    bio: "",

    addressLine1: "",
    addressLine2: "",

    city: "",
    state: "",
    country: "",

    currentSalary: 0,
    expectedSalary: 0,

    resumeUrl: ""
});


/*
    Copy profile data into editData
*/

const loadEditData = () => {

    editData.value = {

        headline: props.profile.headline ?? "",

        bio: props.profile.bio ?? "",

        addressLine1:
            props.profile.addressLine1 ?? "",

        addressLine2:
            props.profile.addressLine2 ?? "",

        city:
            props.profile.city ?? "",

        state:
            props.profile.state ?? "",

        country:
            props.profile.country,

        currentSalary:
            props.profile.currentSalary,

        expectedSalary:
            props.profile.expectedSalary,

        resumeUrl:
            props.profile.resumeUrl ?? ""

    };

};


/*
    Start editing
*/

const startEdit = () => {

    loadEditData();

    isEditing.value = true;

};


/*
    Cancel editing
*/

const cancelEdit = () => {

    isEditing.value = false;

};


/*
    Save changes
*/

const saveChanges = () => {

    emit("update", {

        ...props.profile,

        headline: editData.value.headline,

        bio: editData.value.bio,

        addressLine1:
            editData.value.addressLine1,

        addressLine2:
            editData.value.addressLine2,

        city:
            editData.value.city,

        state:
            editData.value.state,

        country:
            editData.value.country,

        currentSalary:
            editData.value.currentSalary,

        expectedSalary:
            editData.value.expectedSalary,

        resumeUrl:
            editData.value.resumeUrl

    });


    isEditing.value = false;

};


/*
    If profile comes from API later,
    update editData automatically.
*/

watch(
    () => props.profile,
    () => {

        if (!isEditing.value) {

            loadEditData();

        }

    },
    { deep: true }
);

</script>


<template>

<div class="section">


    <!-- =========================
         HEADER
    ========================== -->

    <div class="section-header">

        <h2>About</h2>


        <button
            v-if="!isEditing"
            class="add-btn"
            @click="startEdit"
        >

            Edit

        </button>

    </div>



    <!-- =========================
         VIEW MODE
    ========================== -->

    <div v-if="!isEditing">


        <!-- BIO -->

        <div class="bio-box">

            <label>Bio</label>

            <p v-if="profile.bio">

                {{ profile.bio }}

            </p>

            <p
                v-else
                class="empty"
            >

                No bio added yet.

            </p>

        </div>



        <!-- INFORMATION -->

        <div class="info-grid">


            <!-- Address -->

            <div class="field">

                <label>Address</label>

                <p>

                    {{ profile.addressLine1 || "Not added" }}

                    <span v-if="profile.addressLine2">

                        , {{ profile.addressLine2 }}

                    </span>

                </p>

            </div>


            <!-- City -->

            <div class="field">

                <label>City</label>

                <p>

                    {{ profile.city || "Not added" }}

                </p>

            </div>


            <!-- State -->

            <div class="field">

                <label>State</label>

                <p>

                    {{ profile.state || "Not added" }}

                </p>

            </div>


            <!-- Country -->

            <div class="field">

                <label>Country</label>

                <p>

                    {{ profile.country }}

                </p>

            </div>


            <!-- Current Salary -->

            <div class="field">

                <label>Current Salary</label>

                <p>

                    ₹ {{ profile.currentSalary }}

                </p>

            </div>


            <!-- Expected Salary -->

            <div class="field">

                <label>Expected Salary</label>

                <p>

                    ₹ {{ profile.expectedSalary }}

                </p>

            </div>

        </div>

    </div>



    <!-- =========================
         EDIT MODE
    ========================== -->

    <div
        v-else
        class="edit-form"
    >


        <!-- Headline -->

        <label>Headline</label>

        <input
            v-model="editData.headline"
            placeholder="e.g. Full Stack Developer"
        />



        <!-- Bio -->

        <label>Bio</label>

        <textarea
            v-model="editData.bio"
            rows="5"
            placeholder="Tell recruiters about yourself"
        ></textarea>



        <!-- Address -->

        <label>Address Line 1</label>

        <input
            v-model="editData.addressLine1"
        />


        <label>Address Line 2</label>

        <input
            v-model="editData.addressLine2"
        />



        <!-- City / State -->

        <div class="form-grid">

            <div>

                <label>City</label>

                <input
                    v-model="editData.city"
                />

            </div>


            <div>

                <label>State</label>

                <input
                    v-model="editData.state"
                />

            </div>

        </div>



        <!-- Country -->

        <label>Country</label>

        <input
            v-model="editData.country"
        />



        <!-- Salary -->

        <div class="form-grid">

            <div>

                <label>Current Salary</label>

                <input
                    type="number"
                    v-model.number="editData.currentSalary"
                />

            </div>


            <div>

                <label>Expected Salary</label>

                <input
                    type="number"
                    v-model.number="editData.expectedSalary"
                />

            </div>

        </div>



        <!-- Resume -->

        <label>Resume URL</label>

        <input
            v-model="editData.resumeUrl"
            placeholder="Resume URL"
        />



        <!-- Buttons -->

        <div class="actions">

            <button
                class="cancel-btn"
                @click="cancelEdit"
            >

                Cancel

            </button>


            <button
                class="save-btn"
                @click="saveChanges"
            >

                Save Changes

            </button>

        </div>

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


.edit-btn {

    background: white;

    color: rgb(24, 46, 107);

    border: 1px solid rgb(24, 46, 107);

    padding: 8px 15px;

    border-radius: 7px;

    cursor: pointer;

}


.edit-btn:hover {

    background: rgb(24, 46, 107);

    color: white;

}


/* =========================
   BIO
========================= */

.bio-box {

    padding-bottom: 20px;

    border-bottom: 1px solid #eee;

}


.bio-box label,
.field label,
.edit-form label {

    display: block;

    font-size: 13px;

    font-weight: 600;

    color: #6b7280;

    margin-bottom: 7px;

}


.bio-box p {

    margin: 0;

    line-height: 1.7;

    color: #374151;

}


.empty {

    color: #9ca3af !important;

    font-style: italic;

}


/* =========================
   INFORMATION
========================= */

.info-grid {

    display: grid;

    grid-template-columns: 1fr 1fr;

    gap: 20px;

    margin-top: 20px;

}


.field p {

    margin: 0;

    color: #374151;

}


/* =========================
   EDIT FORM
========================= */

.edit-form input,
.edit-form textarea {

    width: 100%;

    box-sizing: border-box;

    padding: 12px;

    border: 1px solid #ddd;

    border-radius: 8px;

    margin-bottom: 15px;

    font-family: inherit;

}


.edit-form textarea {

    resize: vertical;

}


.form-grid {

    display: grid;

    grid-template-columns: 1fr 1fr;

    gap: 15px;

}


/* =========================
   BUTTONS
========================= */

.actions {

    display: flex;

    justify-content: flex-end;

    gap: 10px;

    margin-top: 10px;

}


.cancel-btn {

    padding: 11px 20px;

    background: white;

    border: 1px solid #ddd;

    border-radius: 8px;

    cursor: pointer;

}


.save-btn {

    padding: 11px 20px;

    background: rgb(24, 46, 107);

    color: white;

    border: none;

    border-radius: 8px;

    cursor: pointer;

}


.save-btn:hover {

    background: #334f9c;

}


@media (max-width: 700px) {

    .info-grid,
    .form-grid {

        grid-template-columns: 1fr;

    }

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