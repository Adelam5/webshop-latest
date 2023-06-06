import Grid from "@mui/material/Grid";
import CheckControl from "common/components/form/CheckControl";
import InputControl from "common/components/form/InputControl";
import { Field } from "formik";

const UserDataFields = () => {
  return (
    <Grid container columnSpacing={1}>
      <Grid item xs={12} sm={6}>
        <Field label="First Name" name="firstName" component={InputControl} />
      </Grid>
      <Grid item xs={12} sm={6}>
        <Field label="Last Name" name="lastName" component={InputControl} />
      </Grid>
      <Grid item xs={12}>
        <Field
          label="Email"
          type="email"
          name="email"
          component={InputControl}
        />
      </Grid>
      <Grid item xs={6}>
        <Field
          label="Password"
          type="password"
          name="password"
          component={InputControl}
        />
      </Grid>
      <Grid item xs={6}>
        <Field
          label="Confirm Password"
          type="password"
          name="confirmPassword"
          component={InputControl}
        />
      </Grid>
      <Grid item xs={12}>
        <Field
          label="I want to receive marketing promotions and updates via email."
          name="allowExtraEmails"
          component={CheckControl}
        />
      </Grid>
    </Grid>
  );
};
export default UserDataFields;
