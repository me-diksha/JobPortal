<script  setup lang="ts">
import { GetAllJobs } from "@/composables/Job/UseGetAllJobs";
import {ref,onMounted} from "vue";

const loadJobs = async()=>{
    const reponse = await GetAllJobs();
}
onMounted(async () =>{
    await loadJobs();
})
</script>
<template></template>
<script scoped></script>
