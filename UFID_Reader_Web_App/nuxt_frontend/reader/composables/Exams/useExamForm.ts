import { toTypedSchema } from "@vee-validate/zod";
import * as z from "zod";

export function useExamForm() {
    const updateExamSchema = toTypedSchema(z.object({
        room: z
            .string()
            .regex(/^[A-Za-z]{3}\d{3,4}$/, "Room must consist of 3 letters followed by 3 to 4 digits")
            .nonempty("Room is required"),
        date: z
            .string()
            .regex(/^(0[1-9]|1[0-2])\/(0[1-9]|[12]\d|3[01])\/\d{4}$/, "Date must be in MM/DD/YYYY format")
            .nonempty("Date is required"),
        start_time: z
            .string()
            .regex(/^(0[1-9]|1[0-2]):[0-5]\d (AM|PM)$/, "Start time must be in HH:MM AM/PM format")
            .nonempty("Start time is required"),
        end_time: z
            .string()
            .regex(/^(0[1-9]|1[0-2]):[0-5]\d (AM|PM)$/, "End time must be in HH:MM AM/PM format")
            .nonempty("End time is required"),
    }));

    return { updateExamSchema }
}