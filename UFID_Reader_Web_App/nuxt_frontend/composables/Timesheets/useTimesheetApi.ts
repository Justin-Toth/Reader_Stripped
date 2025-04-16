const baseUrl = 'http://localhost:8000/timesheet';

// Student Model to be used in the application
export interface Timesheet {
    ufid        : string
    full_name   : string
    course_code : string
    section_num : string
    date        : string
    swipe_time  : string
}


export function useTimesheetApi() {

    async function fetchGlobalTimesheet(): Promise<Timesheet[]> {
        const uri = `${baseUrl}/all`;
        const response = await $fetch(uri);
        return response as Timesheet[];
    }

    async function fetchTimesheetByCourseAndSection(course_code: string, section_num: string): Promise<Timesheet[]> {
        const uri = `${baseUrl}/${course_code}/${section_num}`;
        const response = await $fetch(uri);
        return response as Timesheet[];
    }

    return { fetchGlobalTimesheet, fetchTimesheetByCourseAndSection }
}