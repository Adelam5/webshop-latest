import { useQuery } from "@tanstack/react-query";
import axios from "axios";
import { useStore } from "store";

const getCart = async () => {
  const { data } = await axios.get(`/api/carts`);
  return data;
};

export default function useGetCart() {
  const setCart = useStore((state) => state.setCart);
  const setItemsQuantity = useStore((state) => state.setItemsQuantity);
  return useQuery(["cart"], () => getCart(), {
    refetchOnWindowFocus: false,
    /*     initialData: {
      items: [],
      deliveryMethodId: "511a434a-52d2-4a28-b5d4-d1be04fdc3f5",
      paymentIntentId: "",
      clientSecret: "",
      itemsQuantity: 0
    }, */
    select: (data) => {
      const itemsQuantity = data?.items?.reduce(
        (acc, curr) => (acc = acc + curr.quantity),
        0
      );
      data.itemsQuantity = itemsQuantity;
      return data;
    }
  });
}
