import { Card } from "@heroui/react";
import ActivityCard from "./ActivityCard";
type props = {
  activities: Activity[];
  onSelectActivity: (activityId: string) => void;
  onDeleteActivity: (activityId: string) => void;
};

export default function ActivityList({
  activities,
  onSelectActivity,
  onDeleteActivity,
}: props) {
  return (
    <div className="flex flex-col gap-4">
      <Card className="" variant="transparent">
        {activities.map((activity) => (
          <ActivityCard
            key={activity.id}
            activity={activity}
            onSelectActivity={onSelectActivity}
            onDeleteActivity={onDeleteActivity}
          />
        ))}
      </Card>
    </div>
  );
}
