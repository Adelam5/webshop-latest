import { useMutation } from "@tanstack/react-query";
import axios from "axios";

const verifyEmail = async (confirmationData) => {
  const { data } = await axios.post("/api/auth/verify-email", confirmationData);
  return data;
};

export const useVerifyEmail = () => {
  return useMutation(verifyEmail, {
    retry: 0
  });
};
