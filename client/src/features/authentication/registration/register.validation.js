import * as Yup from "yup";

export const registerSchema = Yup.object({
  firstName: Yup.string().required("First name is required"),
  lastName: Yup.string().required("Last name is required"),
  email: Yup.string()
    .required("Email is required")
    .email("Must be valid email"),
  password: Yup.string()
    .required("Password is required")
    .min(4, "Password must be at least 4 characters"),
  confirmPassword: Yup.string().oneOf(
    [Yup.ref("password"), null],
    "Passwords must match"
  ),
  street: Yup.string().required("Last name is required"),
  city: Yup.string().required("Last name is required"),
  state: Yup.string().required("Last name is required"),
  zipcode: Yup.string().required("Last name is required")
});
