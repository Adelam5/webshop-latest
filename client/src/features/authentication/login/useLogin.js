import { useNavigate } from "react-router-dom";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import axios from "axios";

const login = async (loginData) => {
  const { data } = await axios.post("/api/auth/login", loginData.values);
  return data;
};

export const useLogin = () => {
  const navigate = useNavigate();
  const queryClient = useQueryClient();
  return useMutation(login, {
    retry: 0,
    onSuccess: (data) => {
      queryClient.invalidateQueries({ queryKey: ["user"] });
      if (data.roles.includes("Admin")) {
        navigate("/data");
      } else {
        navigate("/");
      }
    },
    onSettled: (data, error, variables) => {
      variables.setSubmitting(false);
    }
  });
};
