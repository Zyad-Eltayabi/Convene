import { Box, Button, Paper, Typography } from "@mui/material";
import { useActivities } from "../../../lib/hooks/useActivities";
import { useForm } from "react-hook-form";
import { useNavigate, useParams } from "react-router";
import { useEffect } from "react";

import { zodResolver } from "@hookform/resolvers/zod";
import {
  activitySchema,
  type ActivitySchemaType,
} from "../../../lib/schemas/ActivitySchema";
import TextInput from "../../../app/shared/components/TextInput";
import SelectInput from "../../../app/shared/components/SelectInput";
import { categoryOptions } from "./CategoryOptions";
import DateTimeInput from "../../../app/shared/components/DateTimeInput";
import LocationInput from "../../../app/shared/components/LocationInput";

export default function ActivityForm() {
  const { reset, handleSubmit, control } = useForm<ActivitySchemaType>({
    mode: "onTouched",
    resolver: zodResolver(activitySchema),
  });
  const { id } = useParams<{ id: string }>();
  const { updateActivity, createActivity, activity, isActivityLoading } =
    useActivities(id);
  const navigate = useNavigate();

  const onSubmit = async (data: ActivitySchemaType) => {
    const { location, ...rest } = data;
    const flattenedData = {
      ...rest,
      ...location,
    };
    try {
      if (activity) {
        await updateActivity.mutateAsync(
          { ...activity, ...flattenedData },
          { onSuccess: () => navigate(`/activities/${activity.id}`) },
        );
      } else {
        await createActivity.mutateAsync(flattenedData as Activity, {
          onSuccess: (id) => navigate(`/activities/${id}`),
        });
      }
    } catch (error) {
      console.error("Error submitting form:", error);
    }
  };

  useEffect(() => {
    if (activity) {
      reset({
        ...activity,
        date: activity.date ? new Date(activity.date) : undefined,
        location: {
          venue: activity?.venue || "",
          city: activity?.city || "",
          latitude: activity?.latitude || undefined,
          longitude: activity?.longitude || undefined,
        },
      });
    }
  }, [activity, reset]);

  if (isActivityLoading) {
    return <Typography>Loading...</Typography>;
  }
  return (
    <Paper sx={{ borderRadius: 3, padding: 3 }}>
      <Typography variant="h5" gutterBottom color="primary">
        {activity ? "Edit Activity" : "Create Activity"}
      </Typography>
      <Box
        component="form"
        onSubmit={handleSubmit(onSubmit)}
        display="flex"
        flexDirection="column"
        gap={3}
      >
        <TextInput label="Title" name="title" control={control} />
        <TextInput
          label="description"
          name="description"
          control={control}
          multiline
          rows={3}
        />
        <Box display="flex" gap={3}>
          <SelectInput
            label="category"
            name="category"
            control={control}
            items={categoryOptions}
            open={false}
          />
          <DateTimeInput label="Date" name="date" control={control} />
        </Box>
        <LocationInput
          label="Enter Location"
          name="location"
          control={control}
        />

        <Box display="flex" justifyContent="end" gap={3}>
          <Button color="inherit">Cancel</Button>
          <Button
            type="submit"
            color="success"
            variant="contained"
            disabled={updateActivity.isPending || createActivity.isPending}
          >
            Submit
          </Button>
        </Box>
      </Box>
    </Paper>
  );
}
