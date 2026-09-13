import ActivityDetails from "../details/ActivityDetails";
import { ActivityForm } from "../form/ActivityForm";
import ActivityList from "./ActivityList";

type Props = {
  activities: Activity[];
  onSelectActivity: (activityId: string) => void;
  onCancelSelectActivity: () => void;
  selectedActivity: Activity | null;
  openForm?: (id?: string) => void;
  closeForm?: () => void;
  editMode?: boolean;
  onFormSubmit: (activity: Activity) => void;
  onDeleteActivity: (activityId: string) => void;
};
export default function ActivityDashboard({
  activities,
  onSelectActivity,
  onCancelSelectActivity,
  selectedActivity,
  openForm,
  closeForm,
  editMode,
  onFormSubmit,
  onDeleteActivity,
}: Props) {
  return (
    <div className="grid grid-cols-9 gap-3">
      <div className="col-span-6">
        <ActivityList
          activities={activities}
          onSelectActivity={onSelectActivity}
          onDeleteActivity={onDeleteActivity}
        />
      </div>
      <div className="col-span-3">
        {selectedActivity && !editMode && (
          <ActivityDetails
            activity={selectedActivity}
            onCancel={onCancelSelectActivity}
            openForm={openForm}
          />
        )}
        {editMode && (
          <ActivityForm
            closeForm={closeForm}
            activity={selectedActivity}
            onFormSubmit={onFormSubmit}
          />
        )}
      </div>
      <div></div>
    </div>
  );
}
