<script setup lang="ts">

import { ref, watch } from "vue";

import type {
    RecruiterProfile,
    RecruiterProfileRequest
} from "@/types/recruiter";

import { UpdateRecruiterProfile } from "@/composables/Recruiter/RecruiterProfile/UseRecruiterUpdateProfile";

const props = defineProps<{
    profile: RecruiterProfile;
    isEditing: boolean;
}>();


const emit = defineEmits<{
    update: [profile: RecruiterProfile];
}>();


const isEditingInfo = ref(false);


const errors = ref({
    firstName: "",
    lastName: ""
});


const editData = ref<RecruiterProfileRequest>({
    companyId: null,

    firstName: "",
    lastName: "",

    designation: "",
    department: "",
    phone: "",
    linkedinUrl: ""
});


const clearErrors = () => {

    errors.value = {
        firstName: "",
        lastName: ""
    };

};


const loadEditData = () => {

    editData.value = {

        companyId:
            props.profile.companyId,

        firstName:
            props.profile.firstName ?? "",

        lastName:
            props.profile.lastName ?? "",

        designation:
            props.profile.designation ?? "",

        department:
            props.profile.department ?? "",

        phone:
            props.profile.phone ?? "",

        linkedinUrl:
            props.profile.linkedinUrl ?? ""

    };

};


const startEdit = () => {

    loadEditData();

    clearErrors();

    isEditingInfo.value = true;

};


const cancelEdit = () => {

    isEditingInfo.value = false;

    clearErrors();

};


const saveChanges = async () => {

    clearErrors();

    let isValid = true;


    // First name

    if (!editData.value.firstName.trim()) {

        errors.value.firstName =
            "First Name is required";

        isValid = false;

    }


    // Last name

    if (!editData.value.lastName.trim()) {

        errors.value.lastName =
            "Last Name is required";

        isValid = false;

    }


    if (!isValid) {

        return;

    }


    try {

        const payload: RecruiterProfileRequest = {

            companyId:
                editData.value.companyId,

            firstName:
                editData.value.firstName.trim(),

            lastName:
                editData.value.lastName.trim(),

            designation:
                editData.value.designation?.trim() || null,

            department:
                editData.value.department?.trim() || null,

            phone:
                editData.value.phone?.trim() || null,

            linkedinUrl:
                editData.value.linkedinUrl?.trim() || null

        };


        await UpdateRecruiterProfile(payload);


        emit("update", {

            ...props.profile,

            ...payload

        });


        isEditingInfo.value = false;


        console.log(
            "Recruiter profile updated successfully"
        );


    } catch (error) {

        console.error(
            "Error updating recruiter profile:",
            error
        );

    }

};


watch(
    () => props.profile,

    () => {

        if (!isEditingInfo.value) {

            loadEditData();

        }

    },

    {
        deep: true,
        immediate: true
    }
);

</script>


<template>

<div class="section">


    <!-- HEADER -->

    <div class="section-header">

        <h2>
            Recruiter Information
        </h2>

        <button
            v-if="props.isEditing && !isEditingInfo"
            class="edit-btn"
            @click="startEdit"
        >

            Edit

        </button>

    </div>


    <!-- VIEW MODE -->

    <div v-if="!isEditingInfo">

        <div class="info-grid">


            <!-- First Name -->

            <div class="field">

                <label>
                    First Name
                </label>

                <p>
                    {{ profile.firstName || "Not added" }}
                </p>

            </div>


            <!-- Last Name -->

            <div class="field">

                <label>
                    Last Name
                </label>

                <p>
                    {{ profile.lastName || "Not added" }}
                </p>

            </div>


            <!-- Designation -->

            <div class="field">

                <label>
                    Designation
                </label>

                <p>
                    {{ profile.designation || "Not added" }}
                </p>

            </div>


            <!-- Department -->

            <div class="field">

                <label>
                    Department
                </label>

                <p>
                    {{ profile.department || "Not added" }}
                </p>

            </div>


            <!-- Phone -->

            <div class="field">

                <label>
                    Phone
                </label>

                <p>
                    {{ profile.phone || "Not added" }}
                </p>

            </div>


            <!-- LinkedIn -->

            <div class="field">

                <label>
                    LinkedIn
                </label>

                <p v-if="profile.linkedinUrl">

                    <a
                        :href="profile.linkedinUrl"
                        target="_blank"
                        rel="noopener noreferrer"
                    >
                        View LinkedIn Profile
                    </a>

                </p>

                <p v-else>
                    Not added
                </p>

            </div>

        </div>

    </div>


    <!-- EDIT MODE -->

    <div
        v-else
        class="edit-form"
    >


        <!-- First Name -->

        <label>
            First Name
            <span class="required">*</span>
        </label>

        <input
            v-model="editData.firstName"
            type="text"
            placeholder="Enter first name"
            :class="{ 'input-error': errors.firstName }"
        />

        <span
            v-if="errors.firstName"
            class="error-message"
        >
            {{ errors.firstName }}
        </span>


        <!-- Last Name -->

        <label>
            Last Name
            <span class="required">*</span>
        </label>

        <input
            v-model="editData.lastName"
            type="text"
            placeholder="Enter last name"
            :class="{ 'input-error': errors.lastName }"
        />

        <span
            v-if="errors.lastName"
            class="error-message"
        >
            {{ errors.lastName }}
        </span>


        <!-- Designation -->

        <label>
            Designation
        </label>

        <input
            v-model="editData.designation"
            type="text"
            placeholder="e.g. HR Manager"
        />


        <!-- Department -->

        <label>
            Department
        </label>

        <input
            v-model="editData.department"
            type="text"
            placeholder="e.g. Human Resources"
        />


        <!-- Phone -->

        <label>
            Phone
        </label>

        <input
            v-model="editData.phone"
            type="text"
            placeholder="Enter phone number"
        />


        <!-- LinkedIn -->

        <label>
            LinkedIn URL
        </label>

        <input
            v-model="editData.linkedinUrl"
            type="url"
            placeholder="https://linkedin.com/in/..."
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

    border: none;

    background: rgb(24, 46, 107);

    color: white;

    padding: 8px 14px;

    border-radius: 6px;

    cursor: pointer;

}


.edit-btn:hover {

    background: #334f9c;

}


.info-grid {

    display: grid;

    grid-template-columns: 1fr 1fr;

    gap: 20px;

}


.field label {

    display: block;

    font-size: 13px;

    font-weight: 600;

    color: #6b7280;

    margin-bottom: 7px;

}


.field p {

    margin: 0;

    color: #374151;

}


.field a {

    color: rgb(24, 46, 107);

    text-decoration: none;

}


.field a:hover {

    text-decoration: underline;

}


.edit-form input {

    width: 100%;

    box-sizing: border-box;

    padding: 12px;

    border: 1px solid #ddd;

    border-radius: 8px;

    margin-bottom: 15px;

    font-family: inherit;

}


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


.error-message {

    display: block;

    color: #dc2626;

    font-size: 12px;

    margin-top: -10px;

    margin-bottom: 12px;

}


.input-error {

    border: 1px solid #dc2626 !important;

}


.required {

    color: #dc2626;

}


@media (max-width: 700px) {

    .info-grid {

        grid-template-columns: 1fr;

    }

}

</style>