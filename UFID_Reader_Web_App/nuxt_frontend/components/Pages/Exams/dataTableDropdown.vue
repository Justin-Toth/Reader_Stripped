<!-- eslint-disable @typescript-eslint/no-explicit-any -->
<script setup lang="ts">
import { useExamForm } from '~/composables/Exams/useExamForm';


const { updateExamSchema } = useExamForm()

// Define props for the exam data 
const props = defineProps<{
  exam : {
    course_code: string,
    course_name: string,
    instructors: string,
    sections: string,
    room: string,
    date: string,
    start_time: string,
    end_time: string,  
  }
}>()

// Define emits for the component actions
const emit = defineEmits<{
  (e: 'clear' | 'expand'): void,
  (e: 'update', updatedExam: any): void,
}>()

const updatedExam = reactive({ ...props.exam }) 


// Function to clear the exam data from the forms before opening the dialog
function clearExam() {
  updatedExam.room = ""
  updatedExam.date = ""
  updatedExam.start_time = ""
  updatedExam.end_time = ""
}


async function onSubmit() {
  // Emit the updated exam data to the parent component
  emit('update', updatedExam)
  clearExam()
}

onMounted(() => {
  clearExam()
})
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
        <DropdownMenuItem @click="$emit('clear')">
          Clear
        </DropdownMenuItem>
      </DropdownMenuContent>
    </DropdownMenu>

    <!--Edit Dialog-->
    <Form v-slot="{ handleSubmit }" :validation-schema="updateExamSchema">
      <DialogContent class="sm:max-w-[425px]">

        <!--Header Content for the Dialog-->
        <DialogHeader>
          <DialogTitle>Edit Exam</DialogTitle>
          <DialogDescription>
            Change exam details here. Click save when done.
          </DialogDescription>
        </DialogHeader>

        <!--Form Content for the Dialog-->
        <form id="dialogForm" @submit="handleSubmit($event, onSubmit)">
          <FormField v-slot="{ componentField }" name="room" :initial-value="null">
            <FormItem>
              <FormLabel>Room</FormLabel>
              <FormControl>
                <Input v-bind="componentField" v-model="updatedExam.room" type="text" placeholder="ex: NSC215" />
              </FormControl>
              <FormMessage />
            </FormItem>
          </FormField>

          <FormField v-slot="{ componentField }" name="date">
            <FormItem>
              <FormLabel>Date</FormLabel>
              <FormControl>
                <Input v-bind="componentField" v-model="updatedExam.date" type="text" placeholder="MM/DD/YYYY" />
              </FormControl>
              <FormMessage />
            </FormItem>
          </FormField>

          <FormField v-slot="{ componentField }" name="start_time">
            <FormItem>
              <FormLabel>Start Time</FormLabel>
              <FormControl>
                <Input v-bind="componentField" v-model="updatedExam.start_time" type="text" placeholder="HH:MM AM/PM" />
              </FormControl>
              <FormMessage />
            </FormItem>
          </FormField>

          <FormField v-slot="{ componentField }" name="end_time" >
            <FormItem>
              <FormLabel>End Time</FormLabel>
              <FormControl>
                <Input v-bind="componentField" v-model="updatedExam.end_time" type="text" placeholder="HH:MM AM/PM" />
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