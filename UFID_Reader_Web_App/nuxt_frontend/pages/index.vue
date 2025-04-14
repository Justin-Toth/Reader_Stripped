<script setup lang="ts">
import ClassTable from '~/components/Pages/Dashboard/ClassTable/dataTable.vue'
import StudentTable from '~/components/Pages/Dashboard/StudentTable/dataTable.vue'
import { baseClassColumns} from '~/components/Pages/Dashboard/ClassTable/columns'
import { baseStudentColumns } from '~/components/Pages/Dashboard/StudentTable/columns'

import type { Course } from '~/components/Pages/Dashboard/ClassTable/columns'
import type { Student } from '~/components/Pages/Dashboard/StudentTable/columns'


// Sample data for classes and students
// This data is used to populate the tables in the dashboard
// This should be fetched from the actual db in the future
// - Fetch data (GET) courses for (user:Professor)
//   - For base selected course, fetch students (GET) for course
//   - For base selected course, fetch check-in history (GET) for course

// Show the data in the tables
// - Course Table
//    - Student table for "selected course"
//    - Check-in history graph for "selected course"

// The above requires massively upgrade our api and db structure

const classData: Course[] = [
  { name: 'Capstone', code: 'CEN4908C', section: '29563', room: 'NSC215'},
  { name: 'Software Engineering', code: 'CEN4010', section: '29564', room: 'NSC215'},
  { name: 'Computer Networks', code: 'CEN4020', section: '29565', room: 'NSC210'},
  { name: 'Database Systems', code: 'CEN4021', section: '29566', room: 'NSC220'},
  { name: 'Operating Systems', code: 'CEN4010', section: '29567', room: 'NSC225'},
  { name: 'Computer Architecture', code: 'CEN4011', section: '29568', room: 'NSC230'},
  { name: 'Software Testing', code: 'CEN4012', section: '29569', room: 'NSC235'},
  { name: 'Web Development', code: 'CEN4013', section: '29570', room: 'NSC240'},
  { name: 'Mobile Development', code: 'CEN4014', section: '29571', room: 'NSC245'},
  { name: 'Machine Learning', code: 'CEN4015', section: '29572', room: 'NSC250'},
  { name: 'Artificial Intelligence', code: 'CEN4016', section: '29573', room: 'NSC255'},
  { name: 'Data Science', code: 'CEN4017', section: '29574', room: 'NSC260'},
  { name: 'Cloud Computing', code: 'CEN4018', section: '29575', room: 'NSC265'},
  { name: 'Cybersecurity', code: 'CEN4019', section: '29576', room: 'NSC270'},
  { name: 'Human-Computer Interaction', code: 'CEN4020', section: '29577', room: 'NSC275'},
]

const studentData: Student[] = [
{ full_name: 'Justin Toth', email: 'JustinToth@ufl.edu'},
  { full_name: 'Jane Doe', email: 'JaneDoe@ufl.edu'},
  { full_name: 'John Smith', email: 'JohnSmith@ufl.edu'},
  { full_name: 'Alice Johnson', email: 'AliceJohnson@ufl.edu'},
  { full_name: 'Bob Brown', email: 'BobBrown@ufl.edu'},
  { full_name: 'Charlie Davis', email: 'CharlieDavis@ufl.edu'},
  { full_name: 'Diana Evans', email: 'DianaEvans@ufl.edu'},
  { full_name: 'Ethan Foster', email: 'EthanFoster@ufl.edu'},
  { full_name: 'Fiona Green', email: 'FionaGreen@ufl.edu'},
  { full_name: 'George Harris', email: 'GeorgeHarris@ufl.edu'},
  { full_name: 'Hannah White', email: 'HannahWhite@ufl.edu'},
  { full_name: 'Ian King', email: 'IanKing@ufl.edu'},
  { full_name: 'Julia Lewis', email: 'JuliaLewis@ufl.edu'},
  { full_name: 'Kevin Moore', email: 'KevinMoore@ufl.edu'},
  { full_name: 'Laura Scott', email: 'LauraScott@ufl.edu'},
  { full_name: 'Michael Taylor', email: 'MichaelTaylor@ufl.edu'},
  { full_name: 'Natalie Walker', email: 'NatalieWalker@ufl.edu'},
  { full_name: 'Oliver Young', email: 'OliverYoung@ufl.edu'},
  { full_name: 'Paula Adams', email: 'PaulaAdams@ufl.edu'},
  { full_name: 'Quinn Baker', email: 'QuinnBaker@ufl.edu'}
]

</script>

<template>
  <div class="grid grid-cols-1 gap-6 px-6">
    <!-- Top: Two Cards Side-by-Side -->
    <div class="grid grid-cols-1 md:grid-cols-2 gap-6">
      <!-- Classes Card -->
      <Card>
        <CardHeader>
          <CardTitle>Your Classes</CardTitle>
        </CardHeader>
        <CardContent>
          <ClassTable :columns="baseClassColumns" :data="classData ?? []"/>
        </CardContent>
      </Card>

      <!-- Students Card -->
      <Card>
        <CardHeader>
          <CardTitle>Your Students</CardTitle>
        </CardHeader>
        <CardContent>
          <StudentTable :columns="baseStudentColumns" :data="studentData ?? []" />
        </CardContent>
      </Card>
    </div>

    <!-- Graph/Data Visualization -->
    <Card>
      <CardHeader>
        <CardTitle>Active Check-in Graph</CardTitle>
      </CardHeader>
      <CardContent>
        <div class="h-[350px] flex items-center justify-center text-muted-foreground">
          (Insert Chart Here)
        </div>
      </CardContent>
    </Card>
  </div>
</template>
