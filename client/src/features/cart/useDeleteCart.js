import { useMutation, useQueryClient } from "@tanstack/react-query";
import axios from "axios";

const deleteCart = async () => {
  const { data } = await axios.delete("/api/carts");
  return data;
};

export default function useDeleteCart() {
  const queryClient = useQueryClient();
  return useMutation(deleteCart, {
    onSuccess: () => {
      queryClient.invalidateQueries(["cart"]);
    }
  });
}
