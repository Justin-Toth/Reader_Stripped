<script setup lang="ts" generic="TData, TValue">
import type { 
    ColumnDef, 
    ColumnFiltersState, 
    VisibilityState, 
    ExpandedState,
} from '@tanstack/vue-table'

import {
  FlexRender,
  getCoreRowModel,
  getPaginationRowModel,
  getFilteredRowModel,
  getExpandedRowModel,
  useVueTable,
} from '@tanstack/vue-table'

import { valueUpdater } from '~/lib/utils';

const props = defineProps<{
  columns: ColumnDef<TData, TValue>[]
  data: TData[]
}>()

const columnFilters = ref<ColumnFiltersState>([])
const columnVisibility = ref<VisibilityState>({})
const expanded = ref<ExpandedState>({})

const table = useVueTable({
  get data() { return props.data },
  get columns() { return props.columns },

  getCoreRowModel: getCoreRowModel(),
  getPaginationRowModel: getPaginationRowModel(),
  getFilteredRowModel: getFilteredRowModel(),
  getExpandedRowModel: getExpandedRowModel(),

  onColumnFiltersChange: updaterOrValue => valueUpdater(updaterOrValue, columnFilters),
  onColumnVisibilityChange: updaterOrValue => valueUpdater(updaterOrValue, columnVisibility),
  onExpandedChange: updaterOrValue => valueUpdater(updaterOrValue, expanded),

  state: {
    get columnFilters() { return columnFilters.value },
    get columnVisibility() { return columnVisibility.value },
    get expanded() { return expanded.value },
  },
})
</script>

<template>
    <div>    
        <div class="flex items-center py-4">
            <!-- Filter input for ufid -->
            <Input 
                class="max-w-sm" placeholder="Filter Course Code..."
                :model-value="table.getColumn('course_code')?.getFilterValue() as string"
                @update:model-value=" table.getColumn('course_code')?.setFilterValue($event)" />

            <!-- Dropdown for column visibility -->
            <DropdownMenu>
                <DropdownMenuTrigger as-child>
                    <Button variant="outline" class="ml-auto">
                        Columns
                        <Icon name="lucide:chevron-down" class="w-4 h-4 ml-2" />
                    </Button>
                </DropdownMenuTrigger>
                <DropdownMenuContent align="end">
                    <DropdownMenuCheckboxItem
                        v-for="column in table.getAllColumns().filter((column) => column.getCanHide())" 

                        :key="column.id" class="capitalize"
                        :model-value="column.getIsVisible()" 
                        @update:model-value="(value) => {
                            column.toggleVisibility(!!value)
                        }"
                    >
                        {{ column.id }}
                    </DropdownMenuCheckboxItem>
                </DropdownMenuContent>
            </DropdownMenu>
        </div>
        
        <div class="border rounded-md">
            <Table>
                <TableHeader>
                    <TableRow v-for="headerGroup in table.getHeaderGroups()" :key="headerGroup.id">
                        <TableHead v-for="header in headerGroup.headers" :key="header.id">
                            <FlexRender
                            v-if="!header.isPlaceholder" :render="header.column.columnDef.header"
                            :props="header.getContext()"
                            />
                        </TableHead>
                    </TableRow>
                </TableHeader>
                <TableBody>
                    <template v-if="table.getRowModel().rows?.length">
                      <template v-for="row in table.getRowModel().rows" :key="row.id">
                        <TableRow :data-state="row.getIsSelected() ? 'selected' : undefined">
                            <TableCell v-for="cell in row.getVisibleCells()" :key="cell.id">
                                <FlexRender :render="cell.column.columnDef.cell" :props="cell.getContext()" />
                            </TableCell>
                        </TableRow>
                        <TableRow v-if="row.getIsExpanded()">
                          <TableCell :colspan="row.getAllCells().length">
                            {{ JSON.stringify(row.original) }}
                          </TableCell>
                        </TableRow>
                      </template>
                    </template>
                    <template v-else>
                        <TableRow>
                            <TableCell :colspan="columns.length" class="h-24 text-center">
                            No results.
                            </TableCell>
                        </TableRow>
                    </template>
                </TableBody>
            </Table>
        </div>
        <div class="flex items-center justify-end py-4 space-x-2">
                <Button
                    variant="outline"
                size="sm"
                :disabled="!table.getCanPreviousPage()"
                @click="table.previousPage()"
            >
                Previous
            </Button>
            <Button
                variant="outline"
                size="sm"
                :disabled="!table.getCanNextPage()"
                @click="table.nextPage()"
            >
                Next
            </Button>
        </div>
    </div>
</template>