import axios from "axios";
import { useEffect, useState } from "react";
import Navbar from "./Navbar";
import ActivityDashboard from "../../features/activities/dashboard/ActivityDashboard";

function App() {
  const [activities, setActivities] = useState<Activity[]>([]);
  const [selectedActivity, setSelectedActivity] = useState<Activity | null>(
    null,
  );
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

  return (
    <div className="container mx-auto  min-h-screen">
      <Navbar />
      <ActivityDashboard
        activities={activities}
        onSelectActivity={handleSelectActivity}
        onCancelSelectActivity={handleCancelSelectActivity}
        selectedActivity={selectedActivity}
      />
    </div>
  );
}

export default App;
