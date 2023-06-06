import { useQuery, useQueryClient } from "@tanstack/react-query";
import axios from "axios";

const getProduct = async (productId) => {
  const { data } = await axios.get(`/api/products/${productId}`);
  return data;
};

export default function useProduct(productId) {
  const queryClient = useQueryClient();
  return useQuery(["products", productId], () => getProduct(productId), {
    refetchOnWindowFocus: false,
    initialData: () => {
      return queryClient
        .getQueryData(["products"])
        ?.find((p) => p.id === productId);
    }
  });
}
