const baseUrl = 'http://localhost:8000/exams';

// Exam Model to be used in the application
export interface Exam {
    course_code: string
    course_name: string
    instructors: string
    sections: string
    room: string
    date: string
    start_time: string
    end_time: string
}


export function useExamApi() {

    async function fetchExams(): Promise<Exam[]> {
        const uri = `${baseUrl}/all`;
        const response = await $fetch(uri);
        return response as Exam[];
    }

    async function updateExam(exam : Exam) {
        const uri = `${baseUrl}/update/${exam.course_code}`;
        const response = await $fetch(uri, { method: 'PATCH', body: exam });
        return response;
    }

    async function clearExam(exam: Exam) {
        const uri = `${baseUrl}/clear/${exam.course_code}`;
        const response = await $fetch(uri, { method: 'PATCH' });
        return response;
    }

    return { fetchExams, updateExam, clearExam }
}