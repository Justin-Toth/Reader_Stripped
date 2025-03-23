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