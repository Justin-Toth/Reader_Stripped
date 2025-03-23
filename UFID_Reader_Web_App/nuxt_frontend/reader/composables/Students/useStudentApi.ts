const baseUrl = 'http://localhost:8000/students';

// Student Model to be used in the application
export interface Student {
    ufid        : string
    iso         : string
    full_name   : string
    email       : string
    
    course1 : string | null
    course2 : string | null
    course3 : string | null
    course4 : string | null
    course5 : string | null
}


export function useStudentApi() {

    async function fetchStudents(): Promise<Student[]> {
        const uri = `${baseUrl}/all`;
        const response = await $fetch(uri);
        return response as Student[];
    }

    async function addStudent(student : Student) {
        const uri = `${baseUrl}/create`;
        const response = await $fetch(uri, { method: 'POST', body: student });
        return response;
    }

    async function updateStudent(student : Student) {
        const uri = `${baseUrl}/update/courses/${student.ufid}`;
        const response = await $fetch(uri, { method: 'PATCH', body: student });
        return response;
    }

    async function deleteStudent(ufid : string) {
        const uri = `${baseUrl}/delete/${ufid}`;
        const response = await $fetch(uri, { method: 'DELETE' });
        return response;
    }

    return { fetchStudents, addStudent, updateStudent, deleteStudent }
}