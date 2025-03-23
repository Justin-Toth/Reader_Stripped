import { toTypedSchema } from "@vee-validate/zod";
import * as z from "zod";

const serial_numSchema = z
    .string()
    .length(16, "Serial number must be 16 characters long")
    .nonempty("Serial number is required");

const room_numSchema = z
    .string()
    .regex(/^[A-Za-z]{3}\d{3,4}$/, "Room must consist of 3 letters followed by 3 to 4 digits")
    .nonempty("Room is required");


export function useKioskForm() {
    const createKioskSchema = toTypedSchema(z.object({
        serial_num: serial_numSchema,
        room_num: room_numSchema,
    }))

    const updateKioskSchema = toTypedSchema(z.object({
        room_num: room_numSchema,
    }))

    return { createKioskSchema, updateKioskSchema }
}