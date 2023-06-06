import { useMutation } from "@tanstack/react-query";
import axios from "axios";
import useCreateOrUpdatePaymentIntent from "common/hooks/usePaymentIntent";

const updateCart = async (cart) => {
  const { data } = await axios.post("/api/carts", cart);
  return data;
};

export default function useUpdateCart() {
  const { mutate: createOrUpdatePaymentIntent } =
    useCreateOrUpdatePaymentIntent();
  return useMutation(updateCart, {
    onSuccess: () => {
      createOrUpdatePaymentIntent();
    }
  });
}
