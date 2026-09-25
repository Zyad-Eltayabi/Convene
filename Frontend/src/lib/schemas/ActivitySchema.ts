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
  city: requiredField("City")
    .min(2, "City must be between 2 and 100 characters")
    .max(100, "City must be between 2 and 100 characters"),
  venue: requiredField("Venue")
    .min(2, "Venue must be between 2 and 200 characters")
    .max(200, "Venue must be between 2 and 200 characters"),
  latitude: z
    .number({
      required_error: "Latitude is required",
      invalid_type_error: "Latitude must be between -90 and 90.",
    })
    .min(-90, "Latitude must be between -90 and 90.")
    .max(90, "Latitude must be between -90 and 90."),
  longitude: z
    .number({
      required_error: "Longitude is required",
      invalid_type_error: "Longitude must be between -180 and 180.",
    })
    .min(-180, "Longitude must be between -180 and 180.")
    .max(180, "Longitude must be between -180 and 180."),
});

export type ActivitySchemaType = z.infer<typeof activitySchema>;
