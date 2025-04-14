/* eslint-disable @typescript-eslint/no-explicit-any */
import { h } from 'vue'
import type { ColumnDef } from '@tanstack/vue-table'
import DropdownAction from './data-table-dropdown.vue'
import { ArrowUpDown } from 'lucide-vue-next'
import { Button } from '@/components/ui/button'


// This is the interface for the data that will be displayed in the table
export interface Payment {
    id: string
    amount: number
    status: 'pending' | 'processing' | 'success' | 'failed'
    email: string
  }
  
// This is the mock data that will be displayed in the table  
export const payments: Payment[] = [
    {
      id: '728ed52f',
      amount: 100,
      status: 'pending',
      email: 'm@example.com',
    },
    {
      id: '489e1d42',
      amount: 125,
      status: 'processing',
      email: 'example@gmail.com',
    },
    {
        id: '489e1d42',
        amount: 125,
        status: 'processing',
        email: 'example@gmail.com',
    },
    {
        id: '489e1d42',
        amount: 125,
        status: 'processing',
        email: 'example@gmail.com',
    },
    {
        id: '489e1d42',
        amount: 125,
        status: 'processing',
        email: 'example@gmail.com',
    },
    {
        id: '489e1d42',
        amount: 125,
        status: 'processing',
        email: 'example@gmail.com',
    },
    {
        id: '489e1d42',
        amount: 125,
        status: 'processing',
        email: 'example@gmail.com',
    },
    {
        id: '489e1d42',
        amount: 125,
        status: 'processing',
        email: 'example@gmail.com',
    },
    {
        id: '489e1d42',
        amount: 125,
        status: 'processing',
        email: 'example@gmail.com',
    },
    {
        id: '489e1d42',
        amount: 125,
        status: 'processing',
        email: 'example@gmail.com',
    },
    {
        id: '489e1d42',
        amount: 125,
        status: 'processing',
        email: 'example@gmail.com',
    },
    {
        id: '489e1d42',
        amount: 125,
        status: 'processing',
        email: 'example@gmail.com',
    },
    {
        id: '489e1d42',
        amount: 125,
        status: 'processing',
        email: 'example@gmail.com',
    },
    {
        id: '489e1d42',
        amount: 125,
        status: 'processing',
        email: 'example@gmail.com',
    },
    {
        id: '489e1d42',
        amount: 125,
        status: 'processing',
        email: 'example@gmail.com',
    },
    {
        id: '489e1d42',
        amount: 125,
        status: 'processing',
        email: 'example@gmail.com',
    },
  ]

// These are our column definitions for the table
export const columns: ColumnDef<Payment>[] = [
    {
        accessorKey: 'status',
        header: () => h('div', { class: 'text-left' }, 'Status'),
        cell: ({ row } : any) => {
            const status = row.getValue('status')
            return h('div', { class: 'text-left font-medium' }, status)
        }
    },
    {
        accessorKey: 'email',
        header: ({ column }) => {
            return h(Button, {
                variant: 'ghost',
                onClick: () => column.toggleSorting(column.getIsSorted() === 'asc'),
            }, () => ['Email', h(ArrowUpDown, { class: 'ml-2 h-4 w-4' })])
        },
        cell: ({ row }) => h('div', { class: 'lowercase' }, row.getValue('email')),
    },
    {
        accessorKey: 'amount',
        header: () => h('div', { class: 'text-right' }, 'Amount'),
        cell: ({ row }) => {
            const amount = Number.parseFloat(row.getValue('amount'))
            const formatted = new Intl.NumberFormat('en-US', {
                style: 'currency',
                currency: 'USD',
            }).format(amount)

            return h('div', { class: 'text-right font-medium' }, formatted)
        },
    },
    {
        accessorKey: 'id',
        header: () => h('div', { class: 'text-right' }, 'ID'),
        cell: ({ row } : any) => {
            const id = row.getValue('id')
            return h('div', { class: 'text-right font-medium' }, id)
        }
    },
    {
        id: 'actions',
        enableHiding: false,
        cell: ({ row }) => {
          const payment = row.original
    
          return h('div', { class: 'relative' }, h(DropdownAction, {
            payment,
            onExpand: row.toggleExpanded,
          }))
        },
    },

]