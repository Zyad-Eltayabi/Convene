import { z } from "zod";
//import { requiredString } from '../util/util';

export const loginSchema = z.object({
  email: z.string().email({ message: "Invalid email address" }),
  password: z
    .string()
    .min(6, { message: "Password must be at least 6 characters" }),
});

export type LoginSchema = z.input<typeof loginSchema>;
