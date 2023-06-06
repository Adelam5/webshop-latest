import { useMutation, useQueryClient } from "@tanstack/react-query";
import { useNavigate } from "react-router-dom";
import axios from "axios";

const registerCustomer = async (newCustomer) => {
  const { data } = await axios.post("/api/customers", newCustomer);
  return data;
};

export default function useRegisterCustomer() {
  const queryClient = useQueryClient();
  const navigate = useNavigate();
  return useMutation(registerCustomer, {
    onSuccess: () => {
      queryClient.invalidateQueries(["customers"]);
      navigate("/register-success");
    }
  });
}
