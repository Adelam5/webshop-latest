import { useQuery, useQueryClient } from "@tanstack/react-query";
import axios from "axios";
import { useNavigate } from "react-router-dom";

const logout = async () => {
  const { data } = await axios.post("/api/auth/logout");
  return data;
};

export const useLogout = () => {
  const navigate = useNavigate();
  const queryClient = useQueryClient();
  return useQuery(["logout"], logout, {
    enabled: false,
    onSuccess: () => {
      queryClient.invalidateQueries(["user"]);
      queryClient.removeQueries(["cart"]);
      navigate("/");
    }
  });
};
