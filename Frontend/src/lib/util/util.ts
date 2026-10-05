import { format, type DateArg } from "date-fns";
import z from "zod";

export function formatDate(date: DateArg<Date>) {
    return format(date, 'dd MMM yyyy h:mm a')
}

export const requiredField= (field: string) => z.string({
    required_error:  ` ${field} is required`,
}).min(1, { message: ` ${field} is required` }).trim()