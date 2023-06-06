import { useQuery } from "@tanstack/react-query";
import axios from "axios";

const getProducts = async () => {
  const { data } = await axios.get(`api/products`);
  return data;
};

export default function useProducts() {
  return useQuery(["products"], () => getProducts(), {
    refetchOnWindowFocus: false
  });
}
