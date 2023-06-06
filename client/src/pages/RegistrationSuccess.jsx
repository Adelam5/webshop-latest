import Lottie from "lottie-react";
import login from "common/assets/lottie/login.json";
import Box from "@mui/material/Box";
import Container from "@mui/material/Container";
import Typography from "@mui/material/Typography";

const RegistrationSuccess = () => {
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
        <Container maxWidth="xs">
          <Lottie animationData={login} loop={true} />
        </Container>
        <Typography
          component="h5"
          variant="h5"
          align="center"
          color="text.primary"
          gutterBottom
        >
          Great news! Your registration was successful.
        </Typography>
        <Typography
          component="p"
          variant="p"
          align="center"
          color="text.primary"
          gutterBottom
        >
          We have sent you a confirmation email.
          <br /> Before you can log in, please confirm your account.
        </Typography>
      </Box>
    </Container>
  );
};

export default RegistrationSuccess;
