import AddCircleIcon from "@mui/icons-material/AddCircle";
import RemoveCircleIcon from "@mui/icons-material/RemoveCircle";
import Avatar from "@mui/material/Avatar";
import Chip from "@mui/material/Chip";
import IconButton from "@mui/material/IconButton";
import ListItem from "@mui/material/ListItem";
import ListItemAvatar from "@mui/material/ListItemAvatar";
import ListItemText from "@mui/material/ListItemText";
import { useIsFetching, useIsMutating } from "@tanstack/react-query";
import useAddItem from "common/hooks/useAddItem";
import useRemoveItem from "common/hooks/useRemoveItem";

const CartItem = ({ item }) => {
  const { mutate: addItem } = useAddItem();
  const { mutate: removeItem } = useRemoveItem();

  const isFetching = useIsFetching();
  const isMutating = useIsMutating();

  return (
    <ListItem
      secondaryAction={
        <>
          <IconButton
            disabled={isFetching > 0 || isMutating > 0}
            edge="end"
            aria-label="remove"
            onClick={() => removeItem(item?.id)}
          >
            <RemoveCircleIcon />
          </IconButton>
          <IconButton disabled edge="end" aria-label="quantity">
            <Chip label={item?.quantity} />
          </IconButton>
          <IconButton
            disabled={isFetching > 0 || isMutating > 0}
            edge="end"
            aria-label="add"
            onClick={() => addItem(item)}
          >
            <AddCircleIcon />
          </IconButton>
          <Chip
            label={`${(item?.quantity * item?.price).toFixed(2)} USD`}
            sx={{ marginLeft: 5, minWidth: 110 }}
          />
        </>
      }
    >
      <ListItemAvatar>
        <Avatar alt={item?.name} src="https://source.unsplash.com/random" />
      </ListItemAvatar>
      <ListItemText primary={item?.name} />
    </ListItem>
  );
};
export default CartItem;
