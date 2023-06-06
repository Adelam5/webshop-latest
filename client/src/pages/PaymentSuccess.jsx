import { useEffect } from "react";
import Lottie from "lottie-react";
import success from "common/assets/lottie/success.json";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import Box from "@mui/material/Box";
import useDeleteCart from "features/cart/useDeleteCart";

const PaymentSuccess = () => {
  const { mutate: deleteCart } = useDeleteCart();

  useEffect(() => {
    deleteCart();
  }, []);

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
          <Lottie animationData={success} loop={false} />
        </Container>
        <Typography variant="h5" gutterBottom>
          Thank you for your order.
        </Typography>
      </Box>
    </Container>
  );
};

export default PaymentSuccess;
