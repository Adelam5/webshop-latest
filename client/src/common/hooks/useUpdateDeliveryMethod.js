import { useMutation, useQueryClient } from "@tanstack/react-query";
import axios from "axios";
import useCreateOrUpdatePaymentIntent from "common/hooks/usePaymentIntent";

const updateDeliveryMethod = async (deliveryMethod) => {
  const { data } = await axios.put(
    "/api/carts/update-delivery",
    deliveryMethod
  );
  return data;
};

export default function useUpdateDeliveryMethod() {
  const queryClient = useQueryClient();
  const { mutate: createOrUpdatePaymentIntent } =
    useCreateOrUpdatePaymentIntent();
  return useMutation(updateDeliveryMethod, {
    onMutate: async (deliveryMethod) => {
      await queryClient.cancelQueries({ queryKey: ["cart"] });
      const cart = queryClient.getQueryData(["cart"]);
      cart.deliveryMethodId = deliveryMethod.deliveryMethodId;
      cart.deliveryPrice = deliveryMethod.deliveryMethodPrice;
      queryClient.setQueryData(["cart"], cart);
      return { cart };
    },
    onSuccess: () => {
      createOrUpdatePaymentIntent();
    },
    onError: (err, data, context) => {
      queryClient.setQueryData(["cart"], context.cart);
    },
    onSettled: () => {
      queryClient.invalidateQueries({ queryKey: ["cart"] });
    }
  });
}
