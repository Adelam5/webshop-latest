import List from "@mui/material/List";
import Grid from "@mui/material/Grid";
import Typography from "@mui/material/Typography";
import CartItem from "./CartItem";
import Divider from "@mui/material/Divider";
import useGetCart from "./useGetCart";
import { useNavigate } from "react-router-dom";

const CartItemsList = () => {
  const { data: cart } = useGetCart();
  const navigate = useNavigate();

  if (cart && cart?.items?.length < 1) {
    navigate("/");
  }

  return (
    <Grid item xs={12} md={6}>
      <Divider textAlign="left">
        <Typography variant="button" component="h6">
          Items in your cart
        </Typography>
      </Divider>
      <List dense={true}>
        {cart?.items.map((item) => (
          <CartItem key={item.id} item={item} />
        ))}
      </List>
    </Grid>
  );
};
export default CartItemsList;
