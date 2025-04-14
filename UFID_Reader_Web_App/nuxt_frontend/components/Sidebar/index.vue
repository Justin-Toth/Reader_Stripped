<script setup lang="ts">
import { Settings } from 'lucide-vue-next'
</script>

<template>
  <div
    class="sticky top-0 z-30 flex h-14 items-center gap-4 border-b bg-background px-4 sm:static sm:h-auto sm:border-0 sm:bg-transparent sm:px-6"
  >
    <!-- Sidebar -->
    <aside
      class="fixed inset-y-0 left-0 z-10 hidden min-w-fit flex-col border-r bg-background sm:flex"
    >
      <nav class="flex flex-col items-start gap-4 px-4 py-4">
        <!-- Menu Items -->
        <SidebarMenu />
      </nav>

      <!-- Settings Icon -->
      <nav class="mt-auto flex flex-col items-start gap-4 px-4 py-4">
    
        <NuxtLink
            to="/#"
            class="flex items-center gap-3 p-2 rounded-lg text-muted-foreground hover:text-foreground transition-colors"
          >
                <Settings class="h-5 w-5" />
                <span class="text-sm">Settings</span>
        </NuxtLink>
    
      </nav>
    </aside>
  </div>
</template>
