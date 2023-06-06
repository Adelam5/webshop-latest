import { getIn } from "formik";
import FormControlLabel from "@mui/material/FormControlLabel";
import Checkbox from "@mui/material/Checkbox";
import FormHelperText from "@mui/material/FormHelperText";

export const CheckControl = ({
  field,
  form,
  options,
  label,
  placeholder,
  small,
  ...props
}) => {
  const errorText =
    getIn(form.touched, field.name) && getIn(form.errors, field.name);
  return (
    <>
      <FormControlLabel
        control={
          <Checkbox value={field.name} color="primary" {...field} {...props} />
        }
        label={label}
      />
      {!!errorText && (
        <FormHelperText error id="component-error-text">
          {errorText}
        </FormHelperText>
      )}
    </>
  );
};
export default CheckControl;
