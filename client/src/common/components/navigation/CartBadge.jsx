import Badge from "@mui/material/Badge";
import { styled } from "@mui/material/styles";
import IconButton from "@mui/material/IconButton";
import ShoppingCartIcon from "@mui/icons-material/ShoppingCart";
import Avatar from "@mui/material/Avatar";
import { Link } from "react-router-dom";

const StyledBadge = styled(Badge)(({ theme }) => ({
  "& .MuiBadge-badge": {
    right: 1,
    top: 7,
    border: `2px solid ${theme.palette.background.paper}`,
    padding: "0 4px"
  }
}));

const CartBadge = ({ itemsQuantity }) => {
  return (
    <IconButton
      aria-label="cart"
      component={Link}
      to="/checkout"
      disabled={itemsQuantity < 1}
    >
      <StyledBadge badgeContent={itemsQuantity} color="secondary">
        <Avatar>
          <ShoppingCartIcon />
        </Avatar>
      </StyledBadge>
    </IconButton>
  );
};
export default CartBadge;
