<!-- eslint-disable @typescript-eslint/no-explicit-any -->
<script setup lang="ts">
import { useStudentForm } from '~/composables/Students/useStudentForm';

const { updateStudentCoursesSchema } = useStudentForm()

// Define props for the student data
const props = defineProps<{
  student: {
    ufid: string,
    iso: string,
    full_name: string,
    email: string,

    course1: string | undefined,
    course2: string | undefined,
    course3: string | undefined,
    course4: string | undefined,
    course5: string | undefined,
  }
}>()

// Define emits for the component actions
const emit = defineEmits<{
  (e: 'delete' | 'expand'): void,
  (e: 'update', updatedStudent: any): void,
}>()


// Function to copy text to clipboard
function copy(text: string) {
  navigator.clipboard.writeText(text)
}

// Make a copy of student data to edit
const updatedStudent = reactive({ ...props.student })

// Update the reactive object when edit is triggered
function refreshUpdateStudent() {
  updatedStudent.ufid = props.student.ufid
  updatedStudent.iso = props.student.iso
  updatedStudent.full_name = props.student.full_name
  updatedStudent.email = props.student.email
  
  updatedStudent.course1 = props.student.course1
  updatedStudent.course2 = props.student.course2
  updatedStudent.course3 = props.student.course3
  updatedStudent.course4 = props.student.course4
  updatedStudent.course5 = props.student.course5
}


// Function to submit the updated student data
function onSubmit() {
    // Changing empty strings to undefined for optional fields (theres gotta be a better way of doing this... ~ Justin)
    if (updatedStudent.course1 === "") updatedStudent.course1 = undefined
    if (updatedStudent.course2 === "") updatedStudent.course2 = undefined
    if (updatedStudent.course3 === "") updatedStudent.course3 = undefined
    if (updatedStudent.course4 === "") updatedStudent.course4 = undefined
    if (updatedStudent.course5 === "") updatedStudent.course5 = undefined

    emit('update', updatedStudent)  
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
        <DropdownMenuItem @click="copy(student.ufid + ' ' + student.iso)">
          Copy IDs
        </DropdownMenuItem>
        <DropdownMenuItem @click="copy(student.email)">
          Copy Email
        </DropdownMenuItem>
        <DropdownMenuItem @click="$emit('expand')">
          Expand
        </DropdownMenuItem>
        <DropdownMenuSeparator />
        
        <DialogTrigger as-child>
          <DropdownMenuItem @click="refreshUpdateStudent">
            Edit
          </DropdownMenuItem>
        </DialogTrigger>
      
        <DropdownMenuItem @click="$emit('delete')">
          Delete
        </DropdownMenuItem>
      </DropdownMenuContent>
    </DropdownMenu>


    <!--Edit Dialog-->
    <Form v-slot="{ handleSubmit }" :validation-schema="updateStudentCoursesSchema">
      <DialogContent class="sm:max-w-[425px]">
        <DialogHeader>
          <DialogTitle>
            Editing {{ updatedStudent.full_name }}
          </DialogTitle>
          <DialogDescription>
            Change student details here. Click save when done.
          </DialogDescription>
        </DialogHeader>

        <form id="dialogForm" @submit="handleSubmit($event, onSubmit)">
          <FormField v-slot="{ componentField }" name="course1">
            <FormItem>
              <FormLabel>Course 1</FormLabel>
              <FormControl>
                <Input v-bind="componentField" v-model="updatedStudent.course1" type="text" placeholder="No Course" />
              </FormControl>
              <FormMessage />
            </FormItem>
          </FormField>

          <FormField v-slot="{ componentField }" name="course2">
            <FormItem>
              <FormLabel>Course 2</FormLabel>
              <FormControl>
                <Input v-bind="componentField" v-model="updatedStudent.course2" type="text" placeholder="No Course" />
              </FormControl>
              <FormMessage />
            </FormItem>
          </FormField>

          <FormField v-slot="{ componentField }" name="course3">
            <FormItem>
              <FormLabel>Course 3</FormLabel>
              <FormControl>
                <Input v-bind="componentField" v-model="updatedStudent.course3" type="text" placeholder="No Course" />
              </FormControl>
              <FormMessage />
            </FormItem>
          </FormField>

          <FormField v-slot="{ componentField }" name="course4">
              <FormItem>
              <FormLabel>Course 4</FormLabel>
              <FormControl>
                <Input v-bind="componentField" v-model="updatedStudent.course4" type="text" placeholder="No Course" />
              </FormControl>
              <FormMessage />
              </FormItem>
          </FormField>

          <FormField v-slot="{ componentField }" name="course5">
              <FormItem>
              <FormLabel>Course 5</FormLabel>
              <FormControl>
                <Input v-bind="componentField" v-model="updatedStudent.course5" type="text" placeholder="No Course" />
              </FormControl>
              <FormMessage />
              </FormItem>
          </FormField>
        </form>

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