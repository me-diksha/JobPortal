<script setup lang="ts">
import { useRouter } from "vue-router";

interface MenuItem {
  name: string;
  icon: string;
  path: string;
}

interface BottomMenuItem {
  name: string;
  icon: string;
  action?: string;
}

const props = defineProps<{
  companyName: string;
  slogan: string;
  logo: string;
  menuItems: MenuItem[];
  bottomMenu: BottomMenuItem[];
}>();

const router = useRouter();

const navigate = (path: string) => {
  router.push(path);
};

const handleBottomClick = (item: BottomMenuItem) => {
  
  
};
</script>
<template>
  <aside class="sidebar">

    <!-- Brand -->
    <div class="brand">

      <img
        :src="logo"
        class="logo-img"
      />

      <div class="brand-text">
        <h2 class="company">{{ companyName }}</h2>
        <p class="slogan">{{ slogan }}</p>
      </div>

    </div>


    <!-- Main Menu -->

    <div
      v-for="item in menuItems"
      :key="item.name"
      class="menu-item"
      @click="navigate(item.path)"
    >

      <span>{{ item.icon }}</span>

      <span>{{ item.name }}</span>

    </div>

    <!-- Bottom -->

    <div class="bottom-menu">

      <div
        v-for="item in bottomMenu"
        :key="item.name"
        @click="handleBottomClick(item)"
      >

        <span>{{ item.icon }}</span>

        <span>{{ item.name }}</span>

      </div>

    </div>

  </aside>
</template>
<style scoped>

.sidebar {
    width: 250px;
    min-height: 100vh;
    background: rgb(24, 46, 107);
    color: white;
    padding: 25px;
    display: flex;
    flex-direction: column;
    position: sticky;
    top: 0;
    overflow-y: auto;
}

.brand {
    display: flex;
    align-items: center;
    gap: 12px;
    margin-bottom: 20px;
}

.brand-text {
    display: flex;
    flex-direction: column;
}

.company {
    margin: 0;
    font-size: 24px;
    font-weight: bold;
}

.slogan {
    margin: 2px 0 0;
    font-size: 12px;
    color: rgba(255,255,255,.7);
    letter-spacing: 1px;
}

.logo-img {
    width: 50px;
    height: 50px;
    border-radius: 50%;
    object-fit: cover;
}

.user-info {
    text-align: center;
    margin-bottom: 30px;
}

.profile-img {
    width: 90px;
    height: 90px;
    border-radius: 50%;
    object-fit: cover;
    margin-bottom: 10px;
}

.menu {
    flex: 1;
}

.menu-item {
    display: flex;
    align-items: center;
    gap: 12px;
    padding: 15px;
    margin: 10px 0;
    cursor: pointer;
    border-radius: 8px;
    transition: .3s;
}

.menu-item:hover {
    background: white;
    color: rgb(24,46,107);
}

.bottom-menu {
    margin-top: auto;
    border-top: 1px solid rgba(255,255,255,.15);
    padding-top: 20px;
    padding-bottom: 10px;
}

.bottom-menu div {
    display: flex;
    align-items: center;
    gap: 12px;
    padding: 12px;
    border-radius: 8px;
    cursor: pointer;
    transition: .3s;
}

.bottom-menu div:hover {
    background: #334155;
}

</style>