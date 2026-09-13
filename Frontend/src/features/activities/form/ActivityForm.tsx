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
import { parseDate } from "@internationalized/date";
type ActivityFormProps = {
  activity?: Activity | null;
  closeForm?: () => void;
  onFormSubmit: (activity: Activity) => void;
};
export function ActivityForm({
  activity,
  closeForm,
  onFormSubmit,
}: ActivityFormProps) {
  const onSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const formData = new FormData(e.currentTarget);
    const data: { [key: string]: FormDataEntryValue } = {};

    // Convert FormData to plain object
    formData.forEach((value, key) => {
      data[key] = value.toString();
    });

    onFormSubmit(data as unknown as Activity);
    alert(`Form submitted with: ${JSON.stringify(data, null, 2)}`);
  };

  return (
    <Form
      className="flex w-full flex-col gap-4 border border-gray-300 p-6 rounded-lg mt-2"
      render={(props) => <form {...props} data-custom="foo" />}
      onSubmit={onSubmit}
    >
      <TextField
        defaultValue={activity?.title || ""}
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
        <Input placeholder="Enter the activity title" />
        <Description>Enter a title for the activity</Description>
        <FieldError />
      </TextField>

      <TextField
        isRequired
        name="description"
        defaultValue={activity?.description || ""}
      >
        <Label>Description</Label>

        <TextArea
          fullWidth
          placeholder="Describe the activity in detail"
          className="mt-1 block rounded-md"
        />

        <FieldError />
      </TextField>

      <TextField
        isRequired
        name="category"
        type="text"
        defaultValue={activity?.category || ""}
      >
        <Label>Category</Label>
        <Input placeholder="Enter the activity category" />
        <Description>Enter a category for the activity</Description>
        <FieldError />
      </TextField>

      <TextField
        isRequired
        name="city"
        type="text"
        defaultValue={activity?.city || ""}
      >
        <Label>City</Label>
        <Input placeholder="Enter the activity city" />
        <Description>Enter the activity city</Description>
        <FieldError />
      </TextField>

      <TextField
        isRequired
        name="venue"
        type="text"
        defaultValue={activity?.venue || ""}
      >
        <Label>Venue</Label>
        <Input placeholder="Enter the activity venue" />
        <Description>Enter the activity venue</Description>
        <FieldError />
      </TextField>

      <DatePicker
        className="w-72"
        name="date"
        isRequired
        //defaultValue={activity?.date || undefined}
        defaultValue={
          activity?.date ? parseDate(activity.date.split("T")[0]) : undefined
        }
      >
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

        <Button onClick={closeForm} type="button">
          Cancel
        </Button>

        <Button type="reset" variant="secondary">
          Reset
        </Button>
      </div>
    </Form>
  );
}
