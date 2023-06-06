import { getIn } from "formik";
import TextField from "@mui/material/TextField";

const InputControl = ({
  field,
  form,
  label,
  placeholder,
  small,
  type = "text",
  ...props
}) => {
  const errorText =
    getIn(form.touched, field.name) && getIn(form.errors, field.name);
  return (
    <TextField
      error={!!errorText}
      type={type}
      margin="normal"
      required
      fullWidth
      id={field.name}
      label={label}
      autoComplete="false"
      helperText={errorText}
      {...field}
      {...props}
    />
  );
};

export default InputControl;
