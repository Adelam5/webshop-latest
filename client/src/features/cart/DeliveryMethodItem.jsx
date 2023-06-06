import ListItem from "@mui/material/ListItem";
import ListItemButton from "@mui/material/ListItemButton";
import ListItemIcon from "@mui/material/ListItemIcon";
import ListItemText from "@mui/material/ListItemText";
import Checkbox from "@mui/material/Checkbox";
import Chip from "@mui/material/Chip";
import useGetCart from "./useGetCart";
import useUpdateDeliveryMethod from "common/hooks/useUpdateDeliveryMethod";
import { useIsFetching, useIsMutating } from "@tanstack/react-query";

const DeliveryMethodItem = ({ method }) => {
  const { data: cart } = useGetCart();
  const { mutate: setDeliveryMethod } = useUpdateDeliveryMethod();

  const isFetching = useIsFetching();
  const isMutating = useIsMutating();

  return (
    <ListItem
      secondaryAction={
        <Chip
          label={`${method?.price.toFixed(2)} USD`}
          sx={{ marginLeft: 5, minWidth: 110 }}
        />
      }
      disablePadding
    >
      <ListItemButton
        role={undefined}
        onClick={() =>
          setDeliveryMethod({
            deliveryMethodId: method?.id,
            deliveryMethodPrice: method?.price
          })
        }
        dense
      >
        <ListItemIcon>
          <Checkbox
            disabled={isFetching > 0 || isMutating > 0}
            edge="start"
            checked={method.id === cart?.deliveryMethodId}
            tabIndex={-1}
            disableRipple
          />
        </ListItemIcon>
        <ListItemText id={method?.id} primary={method?.name} />
      </ListItemButton>
    </ListItem>
  );
};
export default DeliveryMethodItem;
