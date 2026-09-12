import { Button, Card } from "@heroui/react";

type Props = {
  activity: Activity;
  onCancel ?: () => void;
};
export default function ActivityDetails({ activity, onCancel }: Props) {
  return (
    <div className="flex flex-wrap gap-4">
      <Card className=" gap-2">
        <img
          alt={`${activity.category} community`}
          className="pointer-events-none aspect-square w-full h-100 rounded-2xl object-cover select-none"
          loading="lazy"
          src={`/images/categoryImages/${activity.category}.jpg`}
        />
        <Card.Header>
          <Card.Title>{activity.title}</Card.Title>
          <Card.Description>{activity.description}</Card.Description>
        </Card.Header>
        <Card.Footer className="flex gap-2 flex-col align-start">
          <span className="text-xs block">{activity.date}</span>
          <div>
            <Button className="w-full sm:w-auto m-1">Edit</Button>
            <Button className="w-full sm:w-auto m-1" onClick={onCancel}>
              Cancel
            </Button>
          </div>
        </Card.Footer>
      </Card>
    </div>
  );
}
