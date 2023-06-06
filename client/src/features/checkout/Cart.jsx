import { LoadingButton } from "@mui/lab";
import CartItemsList from "../cart/CartItemsList";
import DeliveryMethodsList from "../cart/DeliveryMethodsList";
import Box from "@mui/material/Box";
import { useIsFetching, useIsMutating } from "@tanstack/react-query";
import useCreateOrder from "common/hooks/useCreateOrder";

const Cart = () => {
  const isFetching = useIsFetching();
  const isMutating = useIsMutating();

  const { mutate: createOrder } = useCreateOrder();

  return (
    <>
      <CartItemsList />
      <DeliveryMethodsList />
      <Box sx={{ display: "flex", justifyContent: "flex-end" }}>
        <LoadingButton
          loading={isFetching > 0 || isMutating > 0}
          variant="contained"
          onClick={createOrder}
          sx={{ mt: 3, ml: 1 }}
        >
          Next
        </LoadingButton>
      </Box>
    </>
  );
};
export default Cart;
