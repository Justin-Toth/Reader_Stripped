<script setup lang="ts">
import type { Payment } from './columns'
import { onMounted, ref } from 'vue'
import { columns, payments } from './columns'
import DataTable from './data-table.vue'

const data = ref<Payment[]>([])

async function getData(): Promise<Payment[]> {
  // Fetch data from your API here.
  return payments;
}

onMounted(async () => {
  data.value = await getData()
})
</script>

<template>
  <div class="container py-10 mx-auto">
    <DataTable :columns="columns" :data="data" />
  </div>
</template>

<!--

Hello! This is a testing page that was used while following the ShadCn datatable tutorial.

It is not used in the final version of the UFID Reader Web App.
This Folder can be deleted whenever, but it is kept here for reference.

This also shows how to use folders for pages in Nuxt 3.
- Index.vue is the default page for this folder.
- You can create subfolders for more pages, and they will be accessible via the URL.
- For example, if you create a folder called "subfolder" and add an index.vue file in it, it will be accessible via /testing/subfolder.

-->