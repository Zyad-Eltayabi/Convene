import { Box, Button, Paper, TextField, Typography } from "@mui/material";
import { useActivities } from "../../../lib/hooks/useActivities";

export default function ActivityForm() {
  const { updateActivity, createActivity } = useActivities();
  const activity = {} as Activity; // Replace with actual activity data if editing
  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    const formData = new FormData(event.currentTarget);

    const data: { [key: string]: FormDataEntryValue } = {};
    formData.forEach((value, key) => {
      data[key] = value;
    });

    if (activity) {
      data.id = activity.id;
      await updateActivity.mutateAsync(data as unknown as Activity);
    } else {
      await createActivity.mutateAsync(data as unknown as Activity);
    }
  };

  return (
    <Paper sx={{ borderRadius: 3, padding: 3 }}>
      <Typography variant="h5" gutterBottom color="primary">
        Create activity
      </Typography>
      <Box
        component="form"
        onSubmit={handleSubmit}
        display="flex"
        flexDirection="column"
        gap={3}
      >
        <TextField
          name="title"
          label="Title"
          defaultValue={activity?.title || ""}
        />
        <TextField
          name="description"
          label="Description"
          defaultValue={activity?.description || ""}
          multiline
          rows={3}
        />
        <TextField
          name="category"
          defaultValue={activity?.category || ""}
          label="Category"
        />
        <TextField
          name="date"
          defaultValue={
            activity?.date
              ? new Date(activity.date).toISOString().split("T")[0]
              : ""
          }
          label="Date"
          type="date"
        />
        <TextField
          name="city"
          defaultValue={activity?.city || ""}
          label="City"
        />
        <TextField
          name="venue"
          defaultValue={activity?.venue || ""}
          label="Venue"
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
