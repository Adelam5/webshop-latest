import { useLocation } from "react-router-dom";
import Lottie from "lottie-react";
import failed from "common/assets/lottie/failed.json";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import Box from "@mui/material/Box";

const PaymentFailed = () => {
  const { state } = useLocation();

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
          <Lottie animationData={failed} loop={false} />
        </Container>
        <Typography variant="h5" gutterBottom>
          Payment Failed
        </Typography>
        <Typography variant="subtitle1">{state?.error?.message}</Typography>
      </Box>
    </Container>
  );
};

export default PaymentFailed;
