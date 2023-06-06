import Container from "@mui/material/Container";
import Paper from "@mui/material/Paper";
import Stepper from "@mui/material/Stepper";
import Step from "@mui/material/Step";
import StepLabel from "@mui/material/StepLabel";
import Typography from "@mui/material/Typography";
import Cart from "features/checkout/Cart";
import Check from "features/checkout/Check";
import Confirm from "features/checkout/Confirm";
import { useStore } from "store";
import { Elements } from "@stripe/react-stripe-js";
import { loadStripe } from "@stripe/stripe-js";
import useGetCart from "features/cart/useGetCart";
import { steps } from "features/checkout/checkout.data";

const stripePromise = loadStripe(import.meta.env.VITE_STRIPE_KEY);

function getStepContent(step) {
  switch (step) {
    case 0:
      return <Cart />;
    case 1:
      return <Confirm />;
    case 2:
      return <Check />;
    default:
      throw new Error(`Unknown step: ${step}`);
  }
}

const Checkout = () => {
  const { data: cart } = useGetCart();
  const activeStep = useStore((state) => state.activeStep);

  const options = {
    clientSecret: cart?.clientSecret,
    appearance: {
      theme: "night",
      labels: "floating"
    }
  };

  return (
    <Container component="main" maxWidth="sm" sx={{ mb: 4 }}>
      <Paper
        variant="outlined"
        sx={{ my: { xs: 3, md: 6 }, p: { xs: 2, md: 3 } }}
      >
        <Typography component="h1" variant="h4" align="center">
          Checkout
        </Typography>
        <Stepper activeStep={activeStep} sx={{ pt: 3, pb: 5 }}>
          {steps.map((label) => (
            <Step key={label}>
              <StepLabel>{label}</StepLabel>
            </Step>
          ))}
        </Stepper>
        {stripePromise && cart?.clientSecret && (
          <Elements stripe={stripePromise} options={options}>
            {getStepContent(activeStep)}
          </Elements>
        )}
      </Paper>
    </Container>
  );
};
export default Checkout;
