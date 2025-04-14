/* eslint-disable @typescript-eslint/no-explicit-any */
import type { ColumnDef } from '@tanstack/vue-table'
import type { Student } from '@/composables/Students/useStudentApi'
import DropdownAction from './dataTableDropdown.vue'

// These are our column definitions for the table
export const baseStudentColumns: ColumnDef<Student>[] = [
    {
        accessorKey: 'full_name',
        header: () => h('div', { class: 'text-left' }, 'Full Name'),
        cell: ({ row } : any) => {
            const full_name = row.getValue('full_name')
            return h('div', { class: 'text-left font-medium' }, full_name)
        }
    },
    {
        accessorKey: 'email',
        header: () => h('div', { class: 'text-left' }, 'Email'),
        cell: ({ row } : any) => {
            const email = row.getValue('email')
            return h('div', { class: 'text-left font-medium' }, email)
        }
    },
    {
        id: 'actions',
        enableHiding: false,
        cell: ({ row }) => {
            const student = row.original
    
            return h('div', { class: 'relative' }, h(DropdownAction, {
            student,
            }))
        },
    },
]