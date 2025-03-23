<!-- eslint-disable @typescript-eslint/no-explicit-any -->
<script setup lang="ts">
import { toast } from '~/components/ui/toast'
import type { Kiosk } from '@/composables/Kiosks/useKioskApi'
import { useKioskApi } from '@/composables/Kiosks/useKioskApi'
import { useKioskForm } from '@/composables/Kiosks/useKioskForm'
import { baseColumns } from '~/components/Pages/Kiosks/columns'
import DropdownAction from '~/components/Pages/Kiosks/dataTableDropdown.vue'

const { fetchKiosks, addKiosk, updateKiosk, deleteKiosk } = useKioskApi()
const { createKioskSchema } = useKioskForm()
const { data: kiosks, refresh } = useAsyncData<Kiosk[]>('kiosks', fetchKiosks)


// Function to handle form submission for adding a new kiosk
async function onSubmit(newKiosk: Kiosk) {
  try {
    await addKiosk(newKiosk)
    await refresh()
    toast({
      title: 'Kiosk Added',
      description: 'The kiosk has been successfully added.',
      duration: 3000,
    })

  } catch (error) {
    console.error("Error adding kiosk:", error)

    toast({
      title: 'Error',
      description: 'Failed to add the Kiosk. Please try again.',
      duration: 3000,
    })
  }
}


// Function to handle update action emitted from the dropdown
async function handleUpdate(updatedKiosk: Kiosk) {
    try {
        await updateKiosk(updatedKiosk)
        await refresh()
        toast({
            title: 'Kiosk Updated',
            description: 'The kiosk details have been updated successfully.',
            duration: 3000,
        })

    } catch (error) {
        console.error("Error updating kiosk:", error)
        toast({
            title: 'Error',
            description: 'There was an error updating the kiosk. Please try again.',
            duration: 3000,
        })
    }
}

// Function to handle delete action emitted from the dropdown
async function handleDelete(kiosk: Kiosk) {
    try {
        await deleteKiosk(kiosk.serial_num)
        await refresh()
        toast({
            title: 'Kiosk Deleted',
            description: 'The kiosk has been successfully deleted.',
            duration: 3000,
        })

    } catch (error) {
        console.error("Error deleting kiosk:", error)
        toast({
            title: 'Error',
            description: 'There was an error deleting the kiosk. Please try again.',
            duration: 3000,
        })
    }
}

// Override the actions column to include our custom dropdown component actions
const columns = baseColumns.map(col => {
    if (col.id !== 'actions') 
        return col

    return {
        ...col,
        cell: ({ row }: any) =>
            h(DropdownAction, {
                kiosk: row.original,
                onUpdate: async (updatedKiosk: Kiosk) => handleUpdate(updatedKiosk),
                onDelete: async () => await handleDelete(row.original),
                onExpand: () => row.toggleExpanded(),
            })
    }
})
</script>

<template>
  <div>
    <Dialog>
      <main class="grid flex-1 items-start gap-4 sm:px-6 sm:py-0 md:gap-4">
        <div class="ml-auto flex items-center gap-2">
        <Button size="sm" variant="outline" class="h-7 gap-1">
          <Icon name="lucide:file" class="h-3.5 w-3.5" />
            <span class="sr-only sm:not-sr-only sm:whitespace-nowrap">
                Export
            </span>
        </Button>

        <DialogTrigger as-child>
          <Button size="sm" class="h-7 gap-1">
            <Icon name="lucide:circle-plus" class="h-3.5 w-3.5" />
            <span class="sr-only sm:not-sr-only sm:whitespace-nowrap">
                Add Kiosk
            </span>
          </Button>
        </DialogTrigger>
      </div>

        <!--Card Containing our DataTable-->
        <Card>
          <CardHeader>
            <CardTitle>Kiosks</CardTitle>
            <CardDescription>List of all kiosks.</CardDescription>
          </CardHeader>

          <CardContent>
            <ClientOnly>
                <PagesKiosksDataTable :columns="columns" :data="kiosks || []" />
            </ClientOnly>
          </CardContent>
        </Card>
      </main>

      <!--Dialog to add a Kiosk-->
      <Form v-slot="{ handleSubmit }" :validation-schema="createKioskSchema">
        <DialogContent class="sm:max-w-[425px]">

          <!--Header Content for the Dialog-->
          <DialogHeader>
          <DialogTitle>Add Kiosk</DialogTitle>
          <DialogDescription>
              Add Kiosk details here. Click submit when done.
          </DialogDescription>
          </DialogHeader>

          <!--Form Content for the Dialog-->
          <form id="dialogForm" @submit="handleSubmit($event, (values) => onSubmit(values as Kiosk))">
            <FormField v-slot="{ componentField }" name="serial_num">
              <FormItem>
                <FormLabel>Serial Number</FormLabel>
                <FormControl>
                  <Input v-bind="componentField" type="text" placeholder="ex: 10000000d340eb60" />
                </FormControl>
                <FormMessage />
              </FormItem>
            </FormField>

            <FormField v-slot="{ componentField }" name="room_num">
              <FormItem>
                <FormLabel>Room Code</FormLabel>
                <FormControl>
                  <Input v-bind="componentField" type="text" placeholder="ex: NSC215" />
                </FormControl>
                <FormMessage />
              </FormItem>
            </FormField>
          </form>

          <!--Footer Content for the Dialog-->
          <DialogFooter>
            <DialogClose as-child>
              <Button type="submit" form="dialogForm"> Save changes </Button>
            </DialogClose>
            <DialogClose as-child>
              <Button variant="outline">Close</Button>
            </DialogClose>
          </DialogFooter>

        </DialogContent>    
      </Form>
    </Dialog>
  </div>
</template>