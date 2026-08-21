<script setup lang="ts">
import type { CandidateExperience } from "@/types/candidate";
import { ref } from "vue";

defineProps<{
  experiences: CandidateExperience[];
}>();

const emit = defineEmits<{
  edit: [experience: CandidateExperience];
  delete: [id: number];
}>();
const isAdding = ref(false);
</script>

<template>
  <div class="experience-card">

    <div class="card-header">
      <h2>Experience</h2>

      <button class="add-btn">
        + Add Experience
      </button>
    </div>
<div
        v-if="!isAdding"
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
    <div v-for="experience in experiences" :key="experience.id" class="experience-block">

      <div class="experience-content">

        <div class="experience-main">

          <h3>{{ experience.designation }}</h3>

          <p class="company">
            {{ experience.companyName }}
          </p>

          <p v-if="experience.description" class="description">
            {{ experience.description }}
          </p>

        </div>

        <div class="experience-info">

          <div>
            <span>Duration</span>
            <strong>
              {{ experience.startDate.getFullYear() }} - {{ experience.currentlyWorking
                ? "till Present"
                : experience.endDate?.getFullYear() }}
            </strong>
          </div>

          <div>
            <span>currentlyWorking</span>
            <strong>
              {{ experience.currentlyWorking }}
            </strong>
          </div>

        </div>

        <div class="actions">

          <button type="button" @click="emit('edit', experience)">
            Edit
          </button>

          <button type="button" @click="emit('delete', experience.id)">
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
  gap: 40px;
}

.experience-info div {
  display: flex;
  flex-direction: column;
  gap: 10px;
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
</style>