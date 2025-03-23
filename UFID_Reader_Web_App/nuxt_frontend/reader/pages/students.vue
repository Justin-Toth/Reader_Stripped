<!-- eslint-disable @typescript-eslint/no-explicit-any -->
<script setup lang="ts">
import { toast } from '~/components/ui/toast' 
import type { Student } from '@/composables/Students/useStudentApi'
import { useStudentApi } from '@/composables/Students/useStudentApi'
import { useStudentForm } from '@/composables/Students/useStudentForm'
import { baseColumns } from '~/components/Pages/Students/columns'
import DropdownAction from '~/components/Pages/Students/dataTableDropdown.vue'

const { fetchStudents, addStudent, updateStudent, deleteStudent } = useStudentApi()
const { createStudentSchema } = useStudentForm()
const { data: students, refresh } = useAsyncData<Student[]>('students', fetchStudents)


async function onSubmit(newStudent: Student) {
    try {
        await addStudent(newStudent)
        await refresh()
        toast({
            title: 'Student Added',
            description: 'The student has been successfully added.',
            duration: 3000,
        })

    } catch (error) {
        console.error("Error adding student:", error)
        toast({
            title: 'Error',
            description: 'There was an error adding the student. Please try again.',
            duration: 3000,
        })
    }
}


async function handleUpdate(updatedStudent: Student) {
    const coursesCopy = {
        course1: updatedStudent.course1,
        course2: updatedStudent.course2,
        course3: updatedStudent.course3,
        course4: updatedStudent.course4,
        course5: updatedStudent.course5
    }

    for (const key in coursesCopy) {
        const typedKey = key as keyof typeof coursesCopy;
        if (coursesCopy[typedKey] === undefined || coursesCopy[typedKey] === null) {
            coursesCopy[typedKey] = "No Course";
        }
    }

    try {
        await updateStudent(updatedStudent)
        await refresh()
        toast({
            title: 'Student Updated',
            description: h('div', {}, [
                h('p', {}, 'The student has been successfully updated.'),
                h('p', {}, 'New Courses are:'),
                h('p', {class:"pl-4"}, `Course 1: ${coursesCopy.course1}`),
                h('p', {class:"pl-4"}, `Course 2: ${coursesCopy.course2}`),
                h('p', {class:"pl-4"}, `Course 3: ${coursesCopy.course3}`),
                h('p', {class:"pl-4"}, `Course 4: ${coursesCopy.course4}`),
                h('p', {class:"pl-4"}, `Course 5: ${coursesCopy.course5}`)
            ]),
            duration: 10000,
        })
    } catch (error) {
        console.error("Error updating student:", error)
        toast({
            title: 'Error',
            description: 'There was an error updating the student. Please try again.',
            duration: 3000,
        })
    }
}


async function handleDelete(student: Student) {
    try {
        await deleteStudent(student.ufid)
        await refresh()
        toast({
            title: 'Student Deleted',
            description: 'The student has been successfully deleted.',
            duration: 3000,
        })
    } catch (error) {
        console.error('Error deleting student:', error)
        toast({
            title: 'Error',
            description: 'There was an error deleting the student. Please try again.',
            duration: 3000,
        })
    }
}


const columns = baseColumns.map(col => {
    if (col.id !== 'actions') 
        return col

    return {
        ...col,
        cell: ({ row }: any) =>
            h(DropdownAction, {
                student: row.original,
                onUpdate: (updatedStudent: Student) => handleUpdate(updatedStudent),
                onDelete: () => handleDelete(row.original),
                onExpand: () => row.toggleExpanded(),
            }),
    }    
})
</script>

<template>
  <div>
    <Dialog>

    <!--Content Above our Card-->
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
              Add Student
          </span>
      </Button>
      </DialogTrigger>
      
    </div>
      <!--Card to hold our DataTable-->
      <Card>
        <CardHeader>
          <CardTitle>Students</CardTitle>
          <CardDescription>List of all students.</CardDescription>
        </CardHeader>

        <CardContent>
            <ClientOnly>
                <PagesStudentsDataTable :columns="columns" :data="students || []" />
            </ClientOnly>
        </CardContent>
      </Card>
    </main>

    <!--Dialog to add a Student-->
    <Form v-slot="{ handleSubmit }" :validation-schema="createStudentSchema">
        <DialogContent class="sm:max-w-[425px]">
            <DialogHeader>
            <DialogTitle>Add Student</DialogTitle>
            <DialogDescription>
                Add student details here. Click submit when done.
            </DialogDescription>
            </DialogHeader>

            <form id="dialogForm" @submit="handleSubmit($event, (values) => onSubmit(values as Student))">
            <FormField v-slot="{ componentField }" name="ufid">
                <FormItem>
                <FormLabel>UFID</FormLabel>
                <FormControl>
                    <Input v-bind="componentField" type="text" placeholder="Enter UFID" />
                </FormControl>
                <FormMessage />
                </FormItem>
            </FormField>

            <FormField v-slot="{ componentField }" name="iso">
                <FormItem>
                <FormLabel>ISO</FormLabel>
                <FormControl>
                    <Input v-bind="componentField" type="text" placeholder="Enter ISO" />
                </FormControl>
                <FormMessage />
                </FormItem>
            </FormField>

            <FormField v-slot="{ componentField }" name="full_name">
                <FormItem>
                <FormLabel>Full Name</FormLabel>
                <FormControl>
                    <Input v-bind="componentField" type="text" placeholder="Enter Full Name" />
                </FormControl>
                <FormMessage />
                </FormItem>
            </FormField>

            <FormField v-slot="{ componentField }" name="email">
                <FormItem>
                <FormLabel>Email</FormLabel>
                <FormControl>
                    <Input v-bind="componentField" type="text" placeholder="Enter Email" />
                </FormControl>
                <FormMessage />
                </FormItem>
            </FormField>

            <FormField v-slot="{ componentField }" name="course1">
                <FormItem>
                <FormLabel>Course 1</FormLabel>
                <FormControl>
                    <Input v-bind="componentField" type="text" placeholder="No Course" />
                </FormControl>
                <FormMessage />
                </FormItem>
            </FormField>

            <FormField v-slot="{ componentField }" name="course2">
                <FormItem>
                <FormLabel>Course 2</FormLabel>
                <FormControl>
                    <Input v-bind="componentField" type="text" placeholder="No Course" />
                </FormControl>
                <FormMessage />
                </FormItem>
            </FormField>

            <FormField v-slot="{ componentField }" name="course3">
                <FormItem>
                <FormLabel>Course 3</FormLabel>
                <FormControl>
                    <Input v-bind="componentField" type="text" placeholder="No Course" />
                </FormControl>
                <FormMessage />
                </FormItem>
            </FormField>

            <FormField v-slot="{ componentField }" name="course4">
                <FormItem>
                <FormLabel>Course 4</FormLabel>
                <FormControl>
                    <Input v-bind="componentField" type="text" placeholder="No Course" />
                </FormControl>
                <FormMessage />
                </FormItem>
            </FormField>

            <FormField v-slot="{ componentField }" name="course5">
                <FormItem>
                <FormLabel>Course 5</FormLabel>
                <FormControl>
                    <Input v-bind="componentField" type="text" placeholder="No Course" />
                </FormControl>
                <FormMessage />
                </FormItem>
            </FormField>
            </form>

            <DialogFooter>
            <DialogClose as-child>
                <Button type="submit" form="dialogForm"> Submit </Button>
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