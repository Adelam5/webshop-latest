import { useQueryClient } from "@tanstack/react-query";

export default function useOrder() {
  const queryClient = useQueryClient();
  return queryClient.getQueryData(["order"]);
}
