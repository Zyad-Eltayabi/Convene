import { AccessTime, Place } from "@mui/icons-material";
import {
  Avatar,
  Box,
  Button,
  Card,
  CardContent,
  CardHeader,
  Chip,
  Divider,
  Typography,
} from "@mui/material";
import { Link } from "react-router";
import { formatDate } from "../../../lib/util/util";

type Props = {
  activity: Activity;
};

export default function ActivityCard({ activity }: Props) {
  const color = activity.isHost
    ? "secondary"
    : activity.isGoing
      ? "warning"
      : "default";
  const label = activity.isHost
    ? "You are hosting this activity"
    : "You are going to this activity";
  return (
    <Card elevation={3} sx={{ borderRadius: 3 }}>
      <Box display="flex" justifyContent="space-between" alignItems="center">
        <CardHeader
          avatar={<Avatar sx={{ width: 80, height: 80 }} />}
          title={activity.title}
          titleTypographyProps={{ fontSize: 20, fontWeight: "bold" }}
          subHeader={
            <>
              Hosted by{" "}
              <Link to={`/profiles/${activity.hostId}`}>
                {activity.hostDisplayName}
              </Link>
            </>
          }
        />
        <Box display="flex" flexDirection="column" gap={2} mr={2}>
          {(activity.isHost || activity.isGoing) && (
            <Chip label={label} color={color} sx={{ borderRadius: 2 }} />
          )}
          {activity.isCancelled && (
            <Chip label="Cancelled" color="error" sx={{ borderRadius: 2 }} />
          )}
        </Box>
      </Box>
      <Divider sx={{ mb: 3 }} />
      <CardContent sx={{ p: 0 }}>
        <Box display="flex" alignItems="center" mb={2} px={2}>
          <Box display="flex" flexGrow={0} alignItems="center">
            <AccessTime sx={{ mr: 1 }} />
            <Typography variant="body2" noWrap>
              {formatDate(activity.date)}
            </Typography>
          </Box>

          <Place sx={{ ml: 3, mr: 1 }} />
          <Typography variant="body2">{activity.venue}</Typography>
        </Box>
        <Divider />
        <Box
          display="flex"
          gap={2}
          sx={{ backgroundColor: "grey.200", py: 3, pl: 3 }}
        >
          {activity.attendees.map((attendee) => (
            <Avatar
              key={attendee.id}
              src={attendee.imageUrl}  
              alt={attendee.displayName}
              sx={{ width: 40, height: 40 }}
              component={Link}
              to={`/profiles/${attendee.id}`}
            />
          ))}
        </Box>
      </CardContent>
      <CardContent sx={{ paddingBottom: 3 }}>
        <Typography variant="body2">{activity.description}</Typography>
        <Button
          component={Link}
          to={`/activities/${activity.id}`}
          variant="contained"
          color="primary"
          sx={{ display: "flex", justifySelf: "self-end", borderRadius: 3 }}
        >
          View
        </Button>
      </CardContent>
    </Card>
  );
}
