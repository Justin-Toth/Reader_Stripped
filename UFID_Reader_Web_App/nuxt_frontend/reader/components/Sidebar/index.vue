<script setup lang="ts">
import {
    Settings,
} from 'lucide-vue-next'
</script>


<template>
    <div class="sticky top-0 z-30 flex h-14 items-center gap-4 border-b bg-background px-4 sm:static sm:h-auto sm:border-0 sm:bg-transparent sm:px-6">
        <aside class="fixed inset-y-0 left-0 z-10 hidden w-[70px] flex-col border-r bg-background sm:flex">     //Increased width bc added labels and too large
            <nav class="flex flex-col items-center gap-4 px-2 py-4">

                <!--Company Logo-->
                <CompanyLogo />
                
                <!--Menu Items-->
                <SidebarMenu />
            </nav>

            <!--Settings Icon-->
            <nav class="mt-auto flex flex-col items-center gap-4 px-2 py-4">
                <TooltipProvider>
                    <Tooltip>
                        <TooltipTrigger as-child>
                            <a
                                href="#"
                                class="flex h-9 w-9 items-center justify-center rounded-lg text-muted-foreground transition-colors hover:text-foreground md:h-8 md:w-8">
                                <Settings class="h-5 w-5" />
                                <span class="sr-only">Settings</span>
                            </a>
                        </TooltipTrigger>
                        <TooltipContent side="right">
                            Settings
                        </TooltipContent>
                    </Tooltip>
                </TooltipProvider>
            </nav>
        </aside>
    </div>
</template>