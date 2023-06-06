import { useQuery, useQueryClient } from "@tanstack/react-query";
import axios from "axios";

const getUser = async () => {
  const { data } = await axios.get("/api/auth/me");
  return data;
};

export const useCurrentUser = () => {
  const queryClient = useQueryClient();
  return useQuery(["user"], getUser, {
    refetchOnWindowFocus: false,
    retry: 0,
    onSuccess: () => {
      queryClient.invalidateQueries(["cart"]);
    },
    onSettled: (data, error) => {
      if (error) {
        queryClient.setQueryData(["user"], null);
      }
    }
  });
};
