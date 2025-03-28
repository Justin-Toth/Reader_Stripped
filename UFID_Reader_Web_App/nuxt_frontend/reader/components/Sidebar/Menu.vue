<script setup lang="ts">
    const items = ref([
        { name: "Dashboard", path: "/", icon: "lucide:home" },
        { name: "Students", path: "/students", icon: "formkit:people" },
        { name: "Kiosks", path: "/kiosks", icon: "lucide:monitor" },
        { name: "Exams", path: "/exams", icon: "quill:paper" },

    ])
</script>

<!-- Create dynamic column and include respective item icon and name for easy navigation -->
<template>
    <div v-for="item in items" :key="item.name" class="flex flex-col items-center">
        <TooltipProvider>
            <Tooltip>
                <TooltipTrigger as-child>
                    <NuxtLink
                        :to="item.path"
                        class="flex flex-col items-center space-y-1 p-2 rounded-lg text-muted-foreground hover:text-foreground transition-colors"
                    >
                        <!-- Icon -->
                        <Icon :name="item.icon" class="h-6 w-6" />
                        <!-- Name -->
                        <span class="text-xs font-medium">{{ item.name }}</span>
                    </NuxtLink>
                </TooltipTrigger>
                <TooltipContent side="right">
                    {{ item.name }}
                </TooltipContent>
            </Tooltip>
        </TooltipProvider>
    </div>
</template>