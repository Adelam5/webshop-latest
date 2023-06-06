import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import Grid from "@mui/material/Grid";
import Link from "common/components/link/Link";
import { Form, Formik } from "formik";
import AddressFields from "./AddressFields";
import { registerSchema } from "./register.validation";
import UserDataFields from "./UserDataFields";
import useRegisterCustomer from "./useRegisterCustomer";
import LoadingButton from "@mui/lab/LoadingButton";
import FormErrorMessage from "common/components/form/FormErrorMessage";

const RegisterForm = () => {
  const initialValues = {
    firstName: "",
    lastName: "",
    email: "",
    password: "",
    confirmPassword: "",
    street: "",
    city: "",
    state: "",
    zipcode: "",
    allowExtraEmails: false
  };

  const { mutate: register, error, isLoading } = useRegisterCustomer();

  const onSubmit = (values) => {
    var { firstName, lastName, email, password, street, city, state, zipcode } =
      values;
    register({
      userData: {
        firstName,
        lastName,
        email,
        password
      },
      address: {
        street,
        city,
        state,
        zipcode
      }
    });
  };

  return (
    <Formik
      initialValues={initialValues}
      validationSchema={registerSchema}
      onSubmit={onSubmit}
    >
      {() => (
        <Box component={Form} sx={{ mt: 3 }}>
          <Grid container columnSpacing={2}>
            <Grid item xs={6}>
              <UserDataFields />
            </Grid>
            <Grid item xs={6}>
              <AddressFields />
            </Grid>
          </Grid>
          {isLoading ? (
            <LoadingButton
              loading
              disabled
              type="submit"
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
            >
              Loading...
            </LoadingButton>
          ) : (
            <Button
              type="submit"
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
            >
              Register
            </Button>
          )}
          <FormErrorMessage
            message={
              error?.response?.data?.errors?.[0]?.message ||
              error?.response?.data?.detail
            }
          />
          <Grid container justifyContent="flex-end">
            <Grid item>
              <Link to="/login" variant="body2">
                Already have an account? Login
              </Link>
            </Grid>
          </Grid>
        </Box>
      )}
    </Formik>
  );
};

export default RegisterForm;
