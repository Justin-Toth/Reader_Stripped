/* eslint-disable @typescript-eslint/no-explicit-any */
import type { ColumnDef } from '@tanstack/vue-table'
import type { Student } from '@/composables/Students/useStudentApi'

// These are our column definitions for the table
export const baseColumns: ColumnDef<Student>[] = [
    {
        accessorKey: 'ufid',
        header: () => h('div', { class: 'text-left' }, 'UFID'),
        cell: ({ row } : any) => {
            const ufid = row.getValue('ufid')
            return h('div', { class: 'text-left font-medium' }, ufid)
        }
    },
    {
        accessorKey: 'iso',
        header: () => h('div', { class: 'text-left' }, 'ISO'),
        cell: ({ row } : any) => {
            const iso = row.getValue('iso')
            return h('div', { class: 'text-left font-medium' }, iso)
        }
    },
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
        accessorKey: 'course1',
        header: () => h('div', { class: 'text-left' }, 'Course 1'),
        cell: ({ row } : any) => {
            const course1 = row.getValue('course1')
            if (course1 == null) {
                return h('div', { class: 'text-left font-medium' }, "None")
            }
            else {
                return h('div', { class: 'text-left font-medium' }, course1)
            }
        }
    },
    {
        accessorKey: 'course2',
        header: () => h('div', { class: 'text-left' }, 'Course 2'),
        cell: ({ row } : any) => {
            const course2 = row.getValue('course2')
            if (course2 == null) {
                return h('div', { class: 'text-left font-medium' }, "None")
            }
            else {
                return h('div', { class: 'text-left font-medium' }, course2)
            }
        }
    },
    {
        accessorKey: 'course3',
        header: () => h('div', { class: 'text-left' }, 'Course 3'),
        cell: ({ row } : any) => {
            const course3 = row.getValue('course3')
            if (course3 == null) {
                return h('div', { class: 'text-left font-medium' }, "None")
            }
            else {
                return h('div', { class: 'text-left font-medium' }, course3)
            }
        }
    },
    {
        accessorKey: 'course4',
        header: () => h('div', { class: 'text-left' }, 'Course 4'),
        cell: ({ row } : any) => {
            const course4 = row.getValue('course4')
            if (course4 == null) {
                return h('div', { class: 'text-left font-medium' }, "None")
            }
            else {
                return h('div', { class: 'text-left font-medium' }, course4)
            }
        }
    },
    {
        accessorKey: 'course5',
        header: () => h('div', { class: 'text-left' }, 'Course 5'),
        cell: ({ row } : any) => {
            const course5 = row.getValue('course5')
            if (course5 == null) {
                return h('div', { class: 'text-left font-medium' }, "None")
            }
            else {
                return h('div', { class: 'text-left font-medium' }, course5)
            }
        }
    },
    {
        id: 'actions',
        enableHiding: false,
        cell: () => h('div', 'Actions') // Placeholder for actions column
    },
]