import { useQuery } from "@tanstack/react-query";
import axios from "axios";

const getCustomerOrders = async () => {
  const { data } = await axios.get("/api/orders/me");
  return data;
};

export const useCustomerOrders = () => {
  return useQuery(["customer-orders"], getCustomerOrders, {
    refetchOnWindowFocus: false
  });
};
