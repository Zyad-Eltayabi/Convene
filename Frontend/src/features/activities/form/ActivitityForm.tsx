import { Box, Button, Paper, TextField, Typography } from "@mui/material";
import { useActivities } from "../../../lib/hooks/useActivities";
import { useForm } from "react-hook-form";
import { useParams } from "react-router";
import { useEffect } from "react";

import { zodResolver } from "@hookform/resolvers/zod";
import {
  activitySchema,
  type ActivitySchemaType,
} from "../../../lib/schemas/Activityschema";

export default function ActivityForm() {
  const {
    register,
    reset,
    handleSubmit,
    formState: { errors },
  } = useForm<ActivitySchemaType>({
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
        date: new Date(activity.date).toISOString().split("T")[0],
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
        <TextField
          {...register("title")}
          label="Title"
          error={!!errors.title}
          helperText={errors.title?.message}
        />
        <TextField
          {...register("description")}
          label="Description"
          multiline
          rows={3}
          error={!!errors.description}
          helperText={errors.description?.message}
        />
        <TextField
          {...register("category")}
          label="Category"
          error={!!errors.category}
          helperText={errors.category?.message}
        />
        <TextField
          {...register("date")}
          label="Date"
          type="date"
          InputLabelProps={{ shrink: true }}
          error={!!errors.date}
          helperText={errors.date?.message}
        />
        <TextField
          {...register("city")}
          label="City"
          error={!!errors.city}
          helperText={errors.city?.message}
        />
        <TextField
          {...register("venue")}
          label="Venue"
          error={!!errors.venue}
          helperText={errors.venue?.message}
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
