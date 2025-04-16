<script setup lang="ts">
    const items = ref([
        { name: "Dashboard", path: "/", icon: "lucide:home" },
        { name: "Students", path: "/students", icon: "formkit:people" },
        { name: "Kiosks", path: "/kiosks", icon: "lucide:monitor" },
        { name: "Exams", path: "/exams", icon: "quill:paper" },
        { name: "Timesheet", path: "/timesheet", icon: "lucide:clock" },
        { name: "Settings", path: "/settings", icon: "lucide:settings" },

    ])
</script>

<template>
        <Sheet>
            <!--Menu Button-->
            <SheetTrigger as-child>
                <Button size="icon" variant="outline" class="sm:hidden">
                    <Icon name="lucide:panel-left" class="h-5 w-5" />
                    <span class="sr-only">Toggle Menu</span>
                </Button>
            </SheetTrigger>

            <!--Menu Items-->
            <SheetContent side="left" class="sm:max-w-xs">
                <nav class="grid gap-6 text-lg font-medium">

                    <!--Menu Items-->
                    <div v-for="item in items" :key="item.name">
                        <NuxtLink
                            :to="item.path"
                            class="flex items-center gap-4 px-2.5 text-muted-foreground hover:text-foreground">
                            <Icon :name="item.icon" class="h-5 w-5" />
                            {{ item.name }}
                        </NuxtLink>
                    </div>
                </nav>
            </SheetContent>
        </Sheet>
</template>