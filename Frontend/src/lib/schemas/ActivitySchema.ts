import { z } from "zod";

const requiredField = (fieldName: string) =>
  z.string().min(1, `${fieldName} is required`);

export const activitySchema = z.object({
  title: requiredField("Title")
    .min(3, "Title must be between 3 and 265 characters")
    .max(265, "Title must be between 3 and 265 characters"),
  date: z.coerce.date({
    required_error: "Date is required",
    invalid_type_error: "Date must be a valid date",
  }),
  description: requiredField("Description")
    .min(10, "Description must be between 10 and 2000 characters")
    .max(2000, "Description must be between 10 and 2000 characters"),
  category: requiredField("Category")
    .min(2, "Category must be between 2 and 100 characters")
    .max(100, "Category must be between 2 and 100 characters"),
  // city: requiredField("City")
  //   .min(2, "City must be between 2 and 100 characters")
  //   .max(100, "City must be between 2 and 100 characters"),
  // venue: requiredField("Venue")
  //   .min(2, "Venue must be between 2 and 200 characters")
  //   .max(200, "Venue must be between 2 and 200 characters"),
  location: z.object({
    city: z.string().optional(),
    venue: z
      .string()
      .min(2, "Venue must be between 2 and 200 characters")
      .max(200, "Venue must be between 2 and 200 characters"),
    latitude: z.coerce.number(),
    longitude: z.coerce.number(),
  }),
});
export type ActivitySchemaType = z.infer<typeof activitySchema>;
