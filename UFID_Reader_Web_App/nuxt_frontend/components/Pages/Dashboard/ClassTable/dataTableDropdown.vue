<!-- eslint-disable @typescript-eslint/no-explicit-any -->
<script setup lang="ts">
// Function to copy text to clipboard
function copy(text: string) {
  navigator.clipboard.writeText(text)
}
</script>

<template>
    <DropdownMenu>
      <DropdownMenuTrigger as-child>
        <Button variant="ghost" class="w-8 h-8 p-0">
          <span class="sr-only">Open menu</span>
          <Icon name="lucide:ellipsis" class="w-4 h-4" />
        </Button>
      </DropdownMenuTrigger>
      <DropdownMenuContent align="end">
        <DropdownMenuLabel>Actions</DropdownMenuLabel>
        <DropdownMenuItem @click="copy(student.ufid + ' ' + student.iso)">
          Copy IDs
        </DropdownMenuItem>
        <DropdownMenuItem @click="copy(student.email)">
          Copy Email
        </DropdownMenuItem>        
      </DropdownMenuContent>
    </DropdownMenu>
</template>