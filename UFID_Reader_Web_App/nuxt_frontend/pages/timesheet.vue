<script setup lang="ts">
import type { Timesheet } from '@/composables/Timesheets/useTimesheetApi'
import { useTimesheetApi } from '@/composables/Timesheets/useTimesheetApi'
import { baseColumns } from '~/components/Pages/Timesheets/columns'


const { fetchGlobalTimesheet} = useTimesheetApi()
const { data: timesheet } = useAsyncData<Timesheet[]>('timesheet', fetchGlobalTimesheet)

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

        <!--Card Containing our DataTable-->
        <Card>
          <CardHeader>
            <CardTitle>Timesheet</CardTitle>
            <CardDescription>The Global Timesheet</CardDescription>
          </CardHeader>

          <CardContent>
            <ClientOnly>
                <PagesTimesheetsDataTable :columns="baseColumns" :data="timesheet ?? []" />
            </ClientOnly>
          </CardContent>
        </Card>
      </main>
  </div>
</template>