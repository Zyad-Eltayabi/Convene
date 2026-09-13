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
    setEditMode(false);
  };

  const handleCancelSelectActivity = () => {
    setSelectedActivity(null);
  };

  const handleOpenForm = (id?: string) => {
    if (id) {
      console.log("Opening form for activity with ID:", id);
      handleSelectActivity(id);
    } else {
      handleCancelSelectActivity();
    }
    setEditMode(true);
  };

  const handleCloseForm = () => {
    setEditMode(false);
  };

  const handleFormSubmit = (activity: Activity) => {
    if (activity.id) {
      setActivities((prevActivities) =>
        prevActivities.map((a) => (a.id === activity.id ? activity : a)),
      );
    } else {
      // Create new activity
      const newActivity = { ...activity, id: Date.now().toString() };
      setActivities((prevActivities) => [...prevActivities, newActivity]);
    }
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
        onFormSubmit={handleFormSubmit}
      />
    </div>
  );
}

export default App;
