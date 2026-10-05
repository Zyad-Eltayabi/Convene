import { z } from "zod";
import { requiredField } from "../util/util";

export const registerSchema = z.object({
  email: z.string().email({ message: "Invalid email address" }),
  password: requiredField("Password"),
    displayName: requiredField("Display Name")
});

export type RegisterSchema = z.input<typeof registerSchema>;
