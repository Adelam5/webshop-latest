import { useMutation, useQueryClient } from "@tanstack/react-query";
import axios from "axios";
import useCreateOrUpdatePaymentIntent from "common/hooks/usePaymentIntent";

const removeItem = async (id) => {
  const { data } = await axios.put("/api/carts/remove-item", { id });
  return data;
};

export default function useRemoveItem() {
  const queryClient = useQueryClient();
  const { mutate: createOrUpdatePaymentIntent } =
    useCreateOrUpdatePaymentIntent();
  return useMutation(removeItem, {
    onMutate: async (productId) => {
      await queryClient.cancelQueries({ queryKey: ["cart"] });
      const cart = queryClient.getQueryData(["cart"]);

      const productInCart = cart.items?.find((item) => item.id === productId);
      let items = [];

      if (!productInCart) return;

      if (productInCart && productInCart?.quantity <= 1) {
        items = cart.items.filter((item) => item.id !== productId);
      } else {
        items = cart.items.map((item) => {
          if (item.id === productId) {
            item.quantity = item.quantity - 1;
          }
          return item;
        });
      }

      cart.itemsQuantity = cart.itemsQuantity - 1 || 0;
      cart.items = items;
      queryClient.setQueryData(["cart"], cart);

      return { cart };
    },
    onSuccess: () => {
      createOrUpdatePaymentIntent();
    },
    onError: (err, item, context) => {
      queryClient.setQueryData(["cart"], context.cart);
    },
    onSettled: () => {
      queryClient.invalidateQueries({ queryKey: ["cart"] });
    }
  });
}
