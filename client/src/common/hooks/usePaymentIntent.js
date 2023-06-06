import { useMutation, useQueryClient } from "@tanstack/react-query";
import axios from "axios";

const createOrUpdatePaymentIntent = async () => {
  const { data } = await axios.post("/api/payments");
  return data;
};

export default function useCreateOrUpdatePaymentIntent() {
  const queryClient = useQueryClient();
  return useMutation(createOrUpdatePaymentIntent, {
    onSuccess: () => {
      queryClient.invalidateQueries(["cart"]);
    }
  });
}
