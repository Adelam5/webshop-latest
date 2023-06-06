import Box from "@mui/material/Box";
import LockOutlinedIcon from "@mui/icons-material/LockOutlined";
import Container from "@mui/material/Container";
import RegisterForm from "features/authentication/registration/RegisterForm";
import FormTitle from "common/components/form/FormTitle";

const Register = () => {
  return (
    <Container component="main" maxWidth="md">
      <Box
        sx={{
          marginTop: 8,
          display: "flex",
          flexDirection: "column",
          alignItems: "center"
        }}
      >
        <FormTitle title="Register" icon={<LockOutlinedIcon />} />
        <RegisterForm />
      </Box>
    </Container>
  );
};
export default Register;
