import {
  PaymentElement,
  useStripe,
  useElements
} from "@stripe/react-stripe-js";
import { useStore } from "store";
import Button from "@mui/material/Button";
import Box from "@mui/material/Box";
import { useNavigate } from "react-router-dom";
import { baseUrl } from "endpoints";
import useGetCart from "features/cart/useGetCart";
import { useState } from "react";

const Check = () => {
  const navigate = useNavigate();
  const activeStep = useStore((state) => state.activeStep);
  const setActiveStep = useStore((state) => state.setActiveStep);
  const [submitting, setSubmitting] = useState(false);

  const { data: cart } = useGetCart();

  const stripe = useStripe();
  const elements = useElements();

  const handleSubmit = async (event) => {
    event.preventDefault();
    setSubmitting(true);

    if (!stripe || !elements) {
      return;
    }

    const result = await stripe.confirmPayment({
      elements,
      confirmParams: {
        return_url: `${baseUrl}/payment-success`
      }
    });

    if (result.error) {
      navigate("/payment-failed", { state: { error: result.error } });
    }
    setSubmitting(false);
  };

  return (
    <form onSubmit={handleSubmit}>
      <PaymentElement />
      <Box sx={{ display: "flex", justifyContent: "flex-end" }}>
        <Button
          onClick={() => setActiveStep(activeStep - 1)}
          sx={{ mt: 3, ml: 1 }}
        >
          Back
        </Button>
        <Button
          disabled={!stripe || !cart?.deliveryMethodId || submitting}
          type="submit"
          variant="contained"
          sx={{ mt: 3, ml: 1 }}
        >
          Confirm payment
        </Button>
      </Box>
    </form>
  );
};

export default Check;
