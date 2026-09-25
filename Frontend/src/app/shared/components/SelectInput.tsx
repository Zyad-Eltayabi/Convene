import {
  FormControl,
  FormHelperText,
  InputLabel,
  MenuItem,
  Select,
  type MenuProps,
} from "@mui/material";
import {
  useController,
  type FieldValues,
  type UseControllerProps,
} from "react-hook-form";
type Props<T extends FieldValues> = {
  label: string;
  items: { text: string; value: string }[];
} & UseControllerProps<T> &
  MenuProps;

export default function SelectInput<T extends FieldValues>(props: Props<T>) {
  const { field, fieldState } = useController({ ...props });
  return (
    <FormControl fullWidth error={!!fieldState.error}>
      <InputLabel>{props.label}</InputLabel>
      <Select
        value={field.value || ""}
        onChange={field.onChange}
        label={props.label}
      >
        {props.items.map((item) => (
          <MenuItem key={item.value} value={item.value}>
            {item.text}
          </MenuItem>
        ))}
      </Select>
      <FormHelperText>{fieldState.error?.message}</FormHelperText>
    </FormControl>
  );
}
