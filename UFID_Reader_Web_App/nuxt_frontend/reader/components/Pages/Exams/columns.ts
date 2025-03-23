/* eslint-disable @typescript-eslint/no-explicit-any */
import type { ColumnDef } from '@tanstack/vue-table'
import type { Exam } from '@/composables/Exams/useExamApi'


// These are our column definitions for the table
export const baseColumns: ColumnDef<Exam>[] = [
    {
        accessorKey: "course_code",
        header: () => h('div', { class: 'text-left' }, 'Course Code'),
        cell: ({ row } : any) => {
            const course_code = row.getValue('course_code')
            return h('div', { class: 'text-left font-medium' }, course_code)
        }
    },
    {
        accessorKey: "course_name",
        header: () => h('div', { class: 'text-left' }, 'Course Name'),
        cell: ({ row } : any) => {
            const course_name = row.getValue('course_name')
            return h('div', { class: 'text-left font-medium' }, course_name)
        }
    },
    {
        accessorKey: "instructors",
        header: () => h('div', { class: 'text-left' }, 'Instructor(s)'),
        cell: ({ row } : any) => {
            const instructors = row.getValue('instructors')
            return h('div', { class: 'text-left font-medium' }, instructors)
        }
    },
    {
        accessorKey: "sections",
        header: () => h('div', { class: 'text-left' }, 'Sections'),
        cell: ({ row } : any) => {
            const sections = row.getValue('sections')
            return h('div', { class: 'text-left font-medium' }, sections)
        }
    },
    {
        accessorKey: "room",
        header: () => h('div', { class: 'text-left' }, 'Room'),
        cell: ({ row } : any) => {
            const room = row.getValue('room')
            if(room == null)
                return h('div', { class: 'text-left font-medium' }, "-")

            return h('div', { class: 'text-left font-medium' }, room)
        }
    },
    {
        accessorKey: "date",
        header: () => h('div', { class: 'text-left' }, 'Date'),
        cell: ({ row } : any) => {
            const date = row.getValue('date')
            if(date == null)
                return h('div', { class: 'text-left font-medium' }, "-")

            return h('div', { class: 'text-left font-medium' }, date)
        }
    },
    {
        accessorKey: "start_time",
        header: () => h('div', { class: 'text-left' }, 'Start Time'),
        cell: ({ row } : any) => {
            const start_time = row.getValue('start_time')
            if(start_time == null)
                return h('div', { class: 'text-left font-medium' }, "-")

            return h('div', { class: 'text-left font-medium' }, start_time)
        }
    },
    {
        accessorKey: "end_time",
        header: () => h('div', { class: 'text-left' }, 'End Time'),
        cell: ({ row } : any) => {
            const end_time = row.getValue('end_time')
            if(end_time == null)
                return h('div', { class: 'text-left font-medium' }, "-")

            return h('div', { class: 'text-left font-medium' }, end_time)
        }
    },
    {
        id: 'actions',
        enableHiding: false,
        cell: () => h('div', 'Actions') // Placeholder for actions column
    }
]