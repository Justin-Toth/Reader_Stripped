<!-- eslint-disable @typescript-eslint/no-explicit-any -->
<script setup lang="ts">
import { useKioskForm } from '~/composables/Kiosks/useKioskForm';

const { updateKioskSchema } = useKioskForm()

// Define props for the exam data 
const props = defineProps<{
  kiosk : {
    serial_num: string,
    room_num: string, 
  }
}>()

// Define emits for the component actions
const emit = defineEmits<{
  (e: 'delete' | 'expand'): void,
  (e: 'update', updatedStudent: any): void,
}>()


// Make a copy of exam data to edit
const updatedKiosk = reactive({ ...props.kiosk }) 

function onSubmit() {
  // Emit the updated exam data to the parent component
  emit('update', updatedKiosk)
}
</script>

<template>
  <Dialog>  
    <DropdownMenu>
      <DropdownMenuTrigger as-child>
        <Button variant="ghost" class="w-8 h-8 p-0">
          <span class="sr-only">Open menu</span>
          <Icon name="lucide:ellipsis" class="w-4 h-4" />
        </Button>
      </DropdownMenuTrigger>
      
      <DropdownMenuContent align="end">
        <DropdownMenuLabel>Actions</DropdownMenuLabel>
        <DropdownMenuItem @click="$emit('expand')">
          Expand
        </DropdownMenuItem>
        <DropdownMenuSeparator />
        
        <DialogTrigger as-child>
          <DropdownMenuItem>
            Edit
          </DropdownMenuItem>
        </DialogTrigger>

        <DropdownMenuItem @click="$emit('delete')">
          Delete
        </DropdownMenuItem>
      </DropdownMenuContent>
    </DropdownMenu>

    <!--Edit Dialog-->
    <Form v-slot="{ handleSubmit }" :validation-schema="updateKioskSchema">
      <DialogContent class="sm:max-w-[425px]">

        <!--Header Content for the Dialog-->
        <DialogHeader>
          <DialogTitle>Edit Kiosk</DialogTitle>
          <DialogDescription>
            Change Kiosk details here. Click save when done.
          </DialogDescription>
        </DialogHeader>

        <!--Form Content for the Dialog-->
        <form id="dialogForm" @submit="handleSubmit($event, onSubmit)">
          <FormField v-slot="{ componentField }" name="room_num">
            <FormItem>
              <FormLabel>Room Code</FormLabel>
              <FormControl>
                <Input v-bind="componentField" v-model="updatedKiosk.room_num" type="text" placeholder="ex: NSC215" />
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
</template>