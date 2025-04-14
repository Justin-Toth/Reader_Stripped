<!-- eslint-disable @typescript-eslint/no-explicit-any -->
<script setup lang="ts">
import { toast } from '@/components/ui/toast'
import type { Exam } from '@/composables/Exams/useExamApi'
import { useExamApi } from '@/composables/Exams/useExamApi'
import { baseColumns } from '~/components/Pages/Exams/columns'
import DropdownAction from '~/components/Pages/Exams/dataTableDropdown.vue'

const { fetchExams, updateExam, clearExam } = useExamApi()
const { data: exams, refresh } = useAsyncData<Exam[]>('exams', fetchExams)


async function handleUpdate(updatedExam: Exam) {
    try {
        await updateExam(updatedExam)
        await refresh()
        toast({
            title: 'Exam Updated',
            description: 'The exam has been successfully updated.',
            duration: 3000,
        })
    } catch (error) {
        console.error("Error updating exam:", error)
        toast({
          title: 'Error',
          description: 'There was an error updating the exam. Please try again.',
          duration: 3000,
        })
    }
}


async function handleClear(exam: Exam) {
    try {
        await clearExam(exam)
        await refresh()
        toast({
            title: 'Exam Cleared',
            description: 'The exam has been successfully cleared.',
            duration: 3000,
        })
    } catch (error) {
        console.error("Error clearing exam:", error)
        toast({
            title: 'Error',
            description: 'There was an error clearing the exam. Please try again.',
            duration: 3000,
        })
    }
}


const columns = baseColumns.map(col => {
    if (col.id !== 'actions')
        return col

    return {
        ...col,
        cell: ({ row } : any) => 
            h(DropdownAction, {
                exam: row.original,
                onUpdate: async (updatedExam: Exam) => handleUpdate(updatedExam),
                onClear: async () => await handleClear(row.original),
                onExpand: () => row.toggleExpanded(),
            })
    }    
})
</script>

<template>
  <div>
    <main class="grid flex-1 items-start gap-4 sm:px-6 sm:py-0 md:gap-4">
      <div class="ml-auto flex items-center gap-2">
      <Button size="sm" variant="outline" class="h-7 gap-1">
        <Icon name="lucide:file" class="h-3.5 w-3.5" />
          <span class="sr-only sm:not-sr-only sm:whitespace-nowrap">
              Export
          </span>
      </Button>
    </div>

      <Card>
        <CardHeader>
          <CardTitle>Exams</CardTitle>
          <CardDescription>List of all exams.</CardDescription>
        </CardHeader>

        <CardContent>
          <ClientOnly >
            <PagesExamsDataTable :columns="columns" :data="exams || []" />
          </ClientOnly>
        </CardContent>
      </Card>
    </main>
  </div>
</template>