/* eslint-disable @typescript-eslint/no-explicit-any */
import type { ColumnDef } from '@tanstack/vue-table'
import DropdownAction from './dataTableDropdown.vue'

export interface Course {
    name: string
    code: string
    sectionNumber: string
    room: string
}


// These are our column definitions for the table
export const baseClassColumns: ColumnDef<Course>[] = [
    {
        accessorKey: "code",
        header: () => h('div', { class: 'text-left' }, 'Course Code'),
        cell: ({ row } : any) => {
            const course_code = row.getValue('code')
            return h('div', { class: 'text-left font-medium' }, course_code)
        }
    },
    {
        accessorKey: "name",
        header: () => h('div', { class: 'text-left' }, 'Course Name'),
        cell: ({ row } : any) => {
            const course_name = row.getValue('name')
            return h('div', { class: 'text-left font-medium' }, course_name)
        }
    },
    {
        accessorKey: "section",
        header: () => h('div', { class: 'text-left' }, 'Section'),
        cell: ({ row } : any) => {
            const section = row.getValue('section')
            return h('div', { class: 'text-left font-medium' }, section)
        }
    },
    {
        accessorKey: "room",
        header: () => h('div', { class: 'text-left' }, 'Room'),
        cell: ({ row } : any) => {
            const room = row.getValue('room')

            return h('div', { class: 'text-left font-medium' }, room)
        }
    },
   {
        id: 'actions',
        enableHiding: false,
        cell: ({ row }) => {
            const course = row.original
    
            return h('div', { class: 'relative' }, h(DropdownAction, {
            course,
            }))
        },
    },
]