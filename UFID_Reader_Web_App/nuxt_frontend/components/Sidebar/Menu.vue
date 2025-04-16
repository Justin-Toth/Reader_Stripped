<script setup lang="ts">
const items = ref([
  { name: "Dashboard", path: "/", icon: "lucide:home" },
  { name: "Students", path: "/students", icon: "formkit:people" },
  { name: "Kiosks", path: "/kiosks", icon: "lucide:monitor" },
  { name: "Exams", path: "/exams", icon: "quill:paper" },
  { name: "Timesheet", path: "/timesheet", icon: "lucide:clock" },
])
</script>

<template>
  <div v-for="item in items" :key="item.name" class="flex">
          <NuxtLink
            :to="item.path"
            class="flex items-center gap-3 p-2 rounded-lg text-muted-foreground hover:text-foreground transition-colors"
          >
            <!-- Icon -->
            <Icon :name="item.icon" class="h-6 w-6" />
            <!-- Name -->
            <span class="text-sm font-medium">{{ item.name }}</span>
          </NuxtLink>
  </div>
</template>
