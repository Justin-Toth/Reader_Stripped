/* eslint-disable @typescript-eslint/no-explicit-any */
import type { ColumnDef } from '@tanstack/vue-table'
import type { Kiosk } from '@/composables/Kiosks/useKioskApi'


// Columns for the kiosks
export const baseColumns: ColumnDef<Kiosk>[] = [
    {
        accessorKey: "serial_num",
        header: () => h('div', { class: 'text-left' }, 'Serial Number'),
        cell: ({ row } : any) => {
            const serial_num = row.getValue('serial_num')
            return h('div', { class: 'text-left font-medium' }, serial_num)
        }
    },
    {
        accessorKey: "room_num",
        header: () => h('div', { class: 'text-left' }, 'Room Number'),
        cell: ({ row } : any) => {
            const room_num = row.getValue('room_num')
            return h('div', { class: 'text-left font-medium' }, room_num)
        }
    },
    {
        id: 'actions',
        enableHiding: false,
        cell: () => h('div', 'Actions') // Placeholder for actions column
    }
]