import axios from "axios";
import { useEffect, useState } from "react";
import Navbar from "./Navbar";
import ActivityDashboard from "../../features/activities/dashboard/ActivityDashboard";

function App() {
  const [activities, setActivities] = useState<Activity[]>([]);
  const [selectedActivity, setSelectedActivity] = useState<Activity | null>(
    null,
  );
  const [editMode, setEditMode] = useState(false);
  useEffect(() => {
    axios
      .get("https://localhost:8080/api/activities")
      .then((response) => setActivities(response.data));
  }, []);

  const handleSelectActivity = (activityId: string) => {
    const activity = activities.find((a) => a.id === activityId) || null;
    setSelectedActivity(activity);
  };

  const handleCancelSelectActivity = () => {
    setSelectedActivity(null);
  };

  const handleOpenForm = (id?: string) => {
    if (id) {
      handleSelectActivity(id);
    } else {
      handleCancelSelectActivity();
    }
    setEditMode(true);
  };

  const handleCloseForm = () => {
    setEditMode(false); 
  };

  return (
    <div className="container mx-auto  min-h-screen">
      <Navbar onOpenForm={handleOpenForm} />
      <ActivityDashboard
        activities={activities}
        onSelectActivity={handleSelectActivity}
        onCancelSelectActivity={handleCancelSelectActivity}
        selectedActivity={selectedActivity}
        editMode={editMode}
        openForm={handleOpenForm}
        closeForm={handleCloseForm}
      />
    </div>
  );
}

export default App;
