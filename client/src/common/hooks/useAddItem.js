import { useMutation, useQueryClient } from "@tanstack/react-query";
import axios from "axios";
import useCreateOrUpdatePaymentIntent from "common/hooks/usePaymentIntent";

const addItem = async (item) => {
  const { data } = await axios.put("/api/carts/add-item", item);
  return data;
};

export default function useAddItem() {
  const queryClient = useQueryClient();
  const { mutate: createOrUpdatePaymentIntent } =
    useCreateOrUpdatePaymentIntent();
  return useMutation(addItem, {
    onMutate: async (product) => {
      await queryClient.cancelQueries({ queryKey: ["cart"] });
      const cart = queryClient.getQueryData(["cart"]);

      if (cart === undefined) return { cart };

      const isProductInCart = cart?.items?.some(
        (item) => item.id === product.id
      );
      let items = [];

      if (!isProductInCart) {
        items = [
          ...cart?.items,
          {
            id: product.id,
            quantity: 1,
            name: product.name,
            price: product.price
          }
        ];
      } else {
        items = cart?.items?.map((item) => {
          if (item.id === product.id) {
            item.quantity = item.quantity + 1;
          }
          return item;
        });
      }

      cart.itemsQuantity = cart.itemsQuantity + 1 || 1;
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
