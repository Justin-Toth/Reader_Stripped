import { toTypedSchema } from '@vee-validate/zod'
import * as z from 'zod'

const ufidSchema = z
    .string()
    .regex(/^\d{8}$/, "Must be an 8-digit number")

const isoSchema = z
    .string()
    .regex(/^\d{16}$/, "Must be a 16-digit number")

const fullNameSchema = z
    .string() 
    .min(1, "Name is required")

const emailSchema = z
    .string()
    .email("Invalid email address")

const courseSchema = z.string()
  .regex(/^(|\d{5})$/, "Must be a 5-digit number or empty")
  .optional()
  .nullable()
  .transform(value => value === "" ? undefined : value);

export function useStudentForm() {
    const createStudentSchema = toTypedSchema(z.object({
        ufid: ufidSchema,
        iso: isoSchema,
        full_name: fullNameSchema,
        email: emailSchema,
        course1: courseSchema,
        course2: courseSchema,
        course3: courseSchema,
        course4: courseSchema,
        course5: courseSchema
    }));

    const updateStudentCoursesSchema = toTypedSchema(z.object({
        course1: courseSchema,
        course2: courseSchema,
        course3: courseSchema,
        course4: courseSchema,
        course5: courseSchema
    }));


    return { createStudentSchema, updateStudentCoursesSchema }
}