import { Button, Card, CloseButton } from "@heroui/react";

type props = {
  activity: Activity;
  onSelectActivity: (activityId: string) => void;
  onDeleteActivity: (activityId: string) => void;
};
export default function ActivityCard({
  activity,
  onSelectActivity,
  onDeleteActivity,
}: props) {
  return (
    <Card className="w-full items-stretch md:flex-row">
      <div className="relative h-[140px] w-full shrink-0 overflow-hidden rounded-2xl sm:h-[120px] sm:w-[120px]">
        <img
          alt="Cherries"
          className="pointer-events-none absolute inset-0 h-full w-full scale-125 object-cover select-none"
          loading="lazy"
          src="https://heroui-assets.nyc3.cdn.digitaloceanspaces.com/docs/cherries.jpeg"
        />
      </div>
      <div className="flex flex-1 flex-col gap-3">
        <Card.Header className="gap-1">
          <Card.Title className="pe-8">{activity.title}</Card.Title>
          <Card.Description>{activity.description}</Card.Description>
          <CloseButton
            aria-label="Close banner"
            className="absolute end-3 top-3"
          />
        </Card.Header>
        <Card.Footer className="mt-auto block">
          <div className="flex flex-col w-full">
            <span className="text-sm font-medium text-foreground">
              {activity.city} / {activity.venue}
            </span>
            <span className="text-xs text-muted">{activity.date}</span>
            <span className="text-xs text-muted rounded-full px-5 py-1 bg-gray-200 w-fit mt-2">
              {activity.category}
            </span>
          </div>
          <div className="mt-2 flex flex-row gap-2 flex-wrap justify-end">
            <Button
              onClick={() => onDeleteActivity(activity.id)}
              variant="danger-soft"
            >
              Delete
            </Button>
            <Button
              className="w-full sm:w-auto"
              onClick={() => onSelectActivity(activity.id)}
            >
              View Details
            </Button>
          </div>
        </Card.Footer>
      </div>
    </Card>
  );
}
