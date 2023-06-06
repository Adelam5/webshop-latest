import { useQuery } from "@tanstack/react-query";
import axios from "axios";

const getDeliveryMethods = async () => {
  const { data } = await axios.get(`api/deliveryMethods`);
  return data;
};

export default function useGetDeliveryMethods() {
  return useQuery(["methods"], () => getDeliveryMethods(), {
    refetchOnWindowFocus: false
  });
}
