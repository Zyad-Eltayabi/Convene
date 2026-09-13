import {
  Button,
  Calendar,
  DateField,
  DatePicker,
  Description,
  FieldError,
  Form,
  Input,
  Label,
  TextArea,
  TextField,
} from "@heroui/react";

type ActivityFormProps = {
  activity?: Activity | null;
  closeForm?: () => void;
};
export function ActivityForm({ activity, closeForm }: ActivityFormProps) {
  const onSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const formData = new FormData(e.currentTarget);
    const data: Record<string, string> = {};

    // Convert FormData to plain object
    formData.forEach((value, key) => {
      data[key] = value.toString();
    });

    alert(`Form submitted with: ${JSON.stringify(data, null, 2)}`);
  };

  return (
    <Form
      className="flex w-full flex-col gap-4 border border-gray-300 p-6 rounded-lg mt-2"
      render={(props) => <form {...props} data-custom="foo" />}
      onSubmit={onSubmit}
    >
      <TextField
        isRequired
        name="title"
        type="text"
        validate={(value) => {
          if (!value || value.trim().length < 3) {
            return "Title must be at least 3 characters long";
          }
          return null;
        }}
      >
        <Label>Title</Label>
        <Input
          placeholder="Enter the activity title"
          value={activity?.title || ""}
        />
        <Description>Enter a title for the activity</Description>
        <FieldError />
      </TextField>

      <div>
        <Label className="text-sm font-medium text-gray-700 block">
          Description
        </Label>
        <TextArea
          fullWidth={true}
          required={true}
          aria-label="Quick project update"
          placeholder="Describe the activity in detail"
          className="mt-1 block  rounded-md"
        >
          <FieldError />
        </TextArea>
      </div>

      <TextField isRequired name="category" type="text">
        <Label>Category</Label>
        <Input placeholder="Enter the activity category" />
        <Description>Enter a category for the activity</Description>
        <FieldError />
      </TextField>

      <TextField isRequired name="city" type="text">
        <Label>City</Label>
        <Input placeholder="Enter the activity city" />
        <Description>Enter a city for the activity</Description>
        <FieldError />
      </TextField>

      <TextField isRequired name="venue" type="text">
        <Label>Venue</Label>
        <Input placeholder="Enter the activity venue" />
        <Description>Enter a venue for the activity</Description>
        <FieldError />
      </TextField>

      <DatePicker className="w-72" name="date" isRequired>
        <Label>Date</Label>
        <DateField.Group fullWidth>
          <DateField.Input>
            {(segment) => <DateField.Segment segment={segment} />}
          </DateField.Input>
          <DateField.Suffix>
            <DatePicker.Trigger>
              <DatePicker.TriggerIndicator />
            </DatePicker.Trigger>
          </DateField.Suffix>
        </DateField.Group>
        <DatePicker.Popover>
          <Calendar aria-label="Event date">
            <Calendar.Header>
              <Calendar.YearPickerTrigger>
                <Calendar.YearPickerTriggerHeading />
                <Calendar.YearPickerTriggerIndicator />
              </Calendar.YearPickerTrigger>
              <Calendar.NavButton slot="previous" />
              <Calendar.NavButton slot="next" />
            </Calendar.Header>
            <Calendar.Grid>
              <Calendar.GridHeader>
                {(day) => <Calendar.HeaderCell>{day}</Calendar.HeaderCell>}
              </Calendar.GridHeader>
              <Calendar.GridBody>
                {(date) => <Calendar.Cell date={date} />}
              </Calendar.GridBody>
            </Calendar.Grid>
            <Calendar.YearPickerGrid>
              <Calendar.YearPickerGridBody>
                {({ year }) => <Calendar.YearPickerCell year={year} />}
              </Calendar.YearPickerGridBody>
            </Calendar.YearPickerGrid>
          </Calendar>
        </DatePicker.Popover>
      </DatePicker>

      <div className="flex gap-2">
        <Button type="submit">Submit</Button>
        <Button onClick={closeForm} type="submit">
          Cancel
        </Button>
        <Button type="reset" variant="secondary">
          Reset
        </Button>
      </div>
    </Form>
  );
}
