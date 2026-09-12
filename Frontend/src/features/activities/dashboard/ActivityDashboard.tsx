import ActivityDetails from "../details/ActivityDetails";
import ActivityList from "./ActivityList";

type Props = {
  activities: Activity[];
  onSelectActivity: (activityId: string) => void;
  onCancelSelectActivity: () => void;
  selectedActivity: Activity | null;
};
export default function ActivityDashboard({ activities, onSelectActivity, onCancelSelectActivity, selectedActivity }: Props) {
  return (
    <div className="grid grid-cols-12 gap-3">
      <div className="col-span-7">
        <ActivityList activities={activities} onSelectActivity={onSelectActivity} />
      </div>
      <div className="col-span-5">
        {selectedActivity && <ActivityDetails activity={selectedActivity} onCancel={onCancelSelectActivity} />}
      </div>
    </div>
  );
}
