/* eslint-disable @typescript-eslint/no-explicit-any */
import type { ColumnDef } from '@tanstack/vue-table'
import type { Timesheet } from '@/composables/Timesheets/useTimesheetApi'
import DropdownAction from './dataTableDropdown.vue'


// Columns for the Timesheets
export const baseColumns: ColumnDef<Timesheet>[] = [
    {
        accessorKey: "course_code",
        header: () => h('div', { class: 'text-left' }, 'Course Code'),
        cell: ({ row } : any) => {
            const course_code = row.getValue('course_code')
            return h('div', { class: 'text-left font-medium' }, course_code)
        }
    },
    {
        accessorKey: "section_num",
        header: () => h('div', { class: 'text-left' }, 'Section Number'),
        cell: ({ row } : any) => {
            const section_num = row.getValue('section_num')
            return h('div', { class: 'text-left font-medium' }, section_num)
        }
    },
    {
        accessorKey: "ufid",
        header: () => h('div', { class: 'text-left' }, 'UFID'),
        cell: ({ row } : any) => {
            const ufid = row.getValue('ufid')
            return h('div', { class: 'text-left font-medium' }, ufid)
        }
    },
    {
        accessorKey: "full_name",
        header: () => h('div', { class: 'text-left' }, 'Full Name'),
        cell: ({ row } : any) => {
            const full_name = row.getValue('full_name')
            return h('div', { class: 'text-left font-medium' }, full_name)
        }
    },
    {
        accessorKey: "date",
        header: () => h('div', { class: 'text-left' }, 'Date'),
        cell: ({ row } : any) => {
            const date = row.getValue('date')
            return h('div', { class: 'text-left font-medium' }, date)
        }
    },
    {
        accessorKey: "swipe_time",
        header: () => h('div', { class: 'text-left' }, 'Swipe Time'),
        cell: ({ row } : any) => {
            const swipe_time = row.getValue('swipe_time')
            return h('div', { class: 'text-left font-medium' }, swipe_time)
        }
    },
    {
        id: 'actions',
        enableHiding: false,
        cell: ({ row }) => {
            const timesheet = row.original
    
            return h('div', { class: 'relative' }, h(DropdownAction, {
            timesheet,
            }))
        },
    }
]