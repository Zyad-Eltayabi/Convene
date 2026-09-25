import { Box, Button, Paper, Typography } from "@mui/material";
import { useActivities } from "../../../lib/hooks/useActivities";
import { useForm } from "react-hook-form";
import { useParams } from "react-router";
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

export default function ActivityForm() {
  const { reset, handleSubmit, control } = useForm<ActivitySchemaType>({
    mode: "onTouched",
    resolver: zodResolver(activitySchema),
    defaultValues: {
      latitude: 0,
      longitude: 0,
    },
  });
  const { id } = useParams<{ id: string }>();
  const { updateActivity, createActivity, activity, isActivityLoading } =
    useActivities(id);

  const onSubmit = async (data: ActivitySchemaType) => {
    console.log("Form data before submission:", data);
    const activityData: Activity = {
      ...activity,
      id: activity?.id ?? "",
      isCancelled: activity?.isCancelled ?? false,
      ...data,
      date: new Date(data.date).toISOString(),
    };

    console.log("Submitting activity data:", activityData);
    // if (activity) {
    //   await updateActivity.mutateAsync(activityData);
    // } else {
    //   await createActivity.mutateAsync(activityData);
    // }
  };

  useEffect(() => {
    if (activity) {
      reset({
        ...activity,
        date: activity.date ? new Date(activity.date) : undefined,
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
        <SelectInput
          label="category"
          name="category"
          control={control}
          items={categoryOptions}
          open={false}
        />
        <TextInput label="City" name="city" control={control} />
        <TextInput label="Venue" name="venue" control={control} />
        <DateTimeInput label="Date" name="date" control={control} />

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
