<script setup lang="ts">
    const items = ref([
        { name: "Dashboard", path: "/", icon: "lucide:home" },
        { name: "Students", path: "/students", icon: "formkit:people" },
        { name: "Kiosks", path: "/kiosks", icon: "lucide:monitor" },
        { name: "Exams", path: "/exams", icon: "quill:paper" },

    ])
</script>

<template>
    <div v-for="item in items" :key="item.name">
        <TooltipProvider>
            <Tooltip>
                <!--Image-->
                <TooltipTrigger as-child>
                    <NuxtLink
                        :to="item.path"
                        class="flex h-9 w-9 items-center justify-center rounded-lg text-muted-foreground transition-colors hover:text-foreground md:h-8 md:w-8">
                        <Icon :name="item.icon" class="h-5 w-5" />
                        <span class="sr-only">{{ item.name }}</span>
                    </NuxtLink>
                </TooltipTrigger>

                <!--Tooltip-->
                <TooltipContent side="right">
                    {{ item.name }}
                </TooltipContent>
            </Tooltip>
        </TooltipProvider>
    </div>
</template>