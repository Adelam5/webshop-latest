import { useQuery } from "@tanstack/react-query";
import axios from "axios";

const getProfile = async () => {
  const { data } = await axios.get("/api/users/profile");
  return data;
};

export const useProfile = () => {
  return useQuery(["profile"], getProfile, {
    refetchOnWindowFocus: false
  });
};
