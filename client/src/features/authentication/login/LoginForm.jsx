import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import LoadingButton from "@mui/lab/LoadingButton";
import Grid from "@mui/material/Grid";
import CheckControl from "common/components/form/CheckControl";
import InputControl from "common/components/form/InputControl";
import { Field, Form, Formik } from "formik";
import { loginSchema } from "./Login.validation";
import { useLogin } from "./useLogin";
import Link from "common/components/link/Link";
import FormErrorMessage from "common/components/form/FormErrorMessage";

const LoginForm = () => {
  const initialValues = {
    email: "",
    password: "",
    remember: false
  };

  const { mutate: login, error } = useLogin();

  const onSubmit = (values, { setSubmitting }) => {
    setSubmitting(true);
    login({ values, setSubmitting });
  };

  return (
    <Formik
      initialValues={initialValues}
      validationSchema={loginSchema}
      onSubmit={onSubmit}
    >
      {({ isSubmitting }) => (
        <Box component={Form} sx={{ mt: 1 }}>
          <Field
            label="Email"
            type="email"
            name="email"
            component={InputControl}
          />
          <Field
            label="Password"
            type="password"
            name="password"
            component={InputControl}
          />
          <Field label="Remember me" name="remember" component={CheckControl} />
          {isSubmitting ? (
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
              Login
            </Button>
          )}
          <FormErrorMessage
            message={
              error?.response?.data?.errors?.[0]?.message ||
              error?.response?.data?.detail
            }
          />
          <Grid container>
            <Grid item xs>
              <Link href="#" variant="body2">
                Forgot password?
              </Link>
            </Grid>
            <Grid item>
              <Link to="/register" variant="body2">
                {"Don't have an account? Register"}
              </Link>
            </Grid>
          </Grid>
        </Box>
      )}
    </Formik>
  );
};

export default LoginForm;
