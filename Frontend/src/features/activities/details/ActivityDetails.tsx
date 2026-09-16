import {
  Button,
  Card,
  CardActions,
  CardContent,
  CardMedia,
  Typography,
} from "@mui/material";
import { Link, useNavigate, useParams } from "react-router";
import { useActivities } from "../../../lib/hooks/useActivities";

export default function ActivityDetails() {
  const {id} = useParams();
  console.log("ActivityDetails component rendered with id:", id);
  const activity = useActivities(id).activity;
  const isActivityLoading = useActivities(id).isActivityLoading;
  const navigate = useNavigate();

  if (isActivityLoading) return <Typography>Loading...</Typography>;

  if (!activity) return <Typography>Activity not found</Typography>;

  return (
    <Card sx={{ borderRadius: 3 }}>
      <CardMedia
        component="img"
        src={`/images/categoryImages/${activity.category}.jpg`}
      />
      <CardContent>
        <Typography variant="h5">{activity.title}</Typography>
        <Typography variant="subtitle1" fontWeight="light">
          {activity.date}
        </Typography>
        <Typography variant="body1">{activity.description}</Typography>
      </CardContent>
      <CardActions>
        <Button
          component={Link}
          to={`/activities/${activity.id}`}
          color="primary"
        >
          Edit
        </Button>
        <Button onClick={() => navigate("/activities")} color="inherit">
          Cancel
        </Button>
      </CardActions>
    </Card>
  );
}
