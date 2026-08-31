<script setup lang="ts">

defineProps<{
    firstName: string;
    lastName: string |null;
    subtitle?: string | null;
    location?: string | null;
    isEditing: boolean;
}>();

const emit = defineEmits<{
    edit: [];
}>();

</script>

<template>

    <div class="profile-header">

        <!-- Profile Image -->

        <img
            src="@/assets/user.png"
            alt="Profile"
            class="profile-img"
        />


        <!-- Profile Information -->

        <div class="profile-info">

            <h1>
                {{ firstName }}
                {{ lastName }}
            </h1>

            <p class="subtitle">
                {{ subtitle || "Add your professional information" }}
            </p>

            <p
                v-if="location"
                class="location"
            >
                📍 {{ location }}
            </p>

        </div>


        <!-- Edit Button -->

        <button
            class="edit-profile-btn"
            @click="emit('edit')"
        >

            {{ isEditing ? "Save Changes" : "Edit Profile" }}

        </button>

    </div>

</template>

<style scoped>

.profile-header {

    background: white;

    padding: 25px;

    border-radius: 15px;

    display: flex;

    align-items: center;

    gap: 20px;

    box-shadow: 0 2px 10px rgba(0, 0, 0, 0.04);

}


.profile-img {

    width: 100px;

    height: 100px;

    border-radius: 50%;

    object-fit: cover;

    border: 3px solid #e8edff;

}


.profile-info {

    flex: 1;

}


.profile-info h1 {

    margin: 0 0 8px 0;

    color: #1f2937;

    font-size: 28px;

}


.subtitle {

    margin: 0 0 8px 0;

    color: #4b5563;

    font-size: 16px;

}


.location {

    margin: 0;

    color: #6b7280;

    font-size: 14px;

}


.edit-profile-btn {

    padding: 12px 22px;

    background: rgb(24, 46, 107);

    color: white;

    border: none;

    border-radius: 8px;

    cursor: pointer;

    font-size: 14px;

    transition: 0.3s;

}


.edit-profile-btn:hover {

    background: #334f9c;

}


@media (max-width: 700px) {

    .profile-header {

        flex-direction: column;

        text-align: center;

    }

    .edit-profile-btn {

        margin-top: 10px;

    }

}

</style>