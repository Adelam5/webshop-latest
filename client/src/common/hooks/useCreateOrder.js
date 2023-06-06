import { useMutation, useQueryClient } from "@tanstack/react-query";
import axios from "axios";
import { useStore } from "store";

const createOrder = async () => {
  const { data } = await axios.post("/api/orders");
  return data;
};

export default function useCreateOrder() {
  const setActiveStep = useStore((state) => state.setActiveStep);
  const queryClient = useQueryClient();
  return useMutation(createOrder, {
    onSuccess: (data) => {
      queryClient.setQueryData(["order"], data);
      queryClient.invalidateQueries(["order"]);
      setActiveStep(1);
    }
  });
}
