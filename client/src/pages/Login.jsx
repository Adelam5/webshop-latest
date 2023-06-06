import LoginForm from "features/authentication/login/LoginForm";
import LockOutlinedIcon from "@mui/icons-material/LockOutlined";
import Box from "@mui/material/Box";
import Container from "@mui/material/Container";
import { useNavigate } from "react-router-dom";
import { useCurrentUser } from "features/authentication/common/useCurrentUser";
import FormTitle from "common/components/form/FormTitle";

const Login = () => {
  const navigate = useNavigate();
  const { data: user, isLoading } = useCurrentUser();

  if (!isLoading && !!user) navigate("/");

  return (
    <Container component="main" maxWidth="xs">
      <Box
        sx={{
          marginTop: 8,
          display: "flex",
          flexDirection: "column",
          alignItems: "center"
        }}
      >
        <FormTitle title="Login" icon={<LockOutlinedIcon />} />
        <LoginForm />
      </Box>
    </Container>
  );
};
export default Login;
