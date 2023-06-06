import Grid from "@mui/material/Grid";
import CheckControl from "common/components/form/CheckControl";
import InputControl from "common/components/form/InputControl";
import { Field } from "formik";

const AddressFields = () => {
  return (
    <Grid container columnSpacing={1}>
      <Grid item xs={12}>
        <Field label="Street" name="street" component={InputControl} />
      </Grid>
      <Grid item xs={12}>
        <Field label="City" name="city" component={InputControl} />
      </Grid>
      <Grid item xs={12} sm={6}>
        <Field label="State" name="state" component={InputControl} />
      </Grid>
      <Grid item xs={12} sm={6}>
        <Field label="Zipcode" name="zipcode" component={InputControl} />
      </Grid>
    </Grid>
  );
};
export default AddressFields;
