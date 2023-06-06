import { NavLink as RouterNavLink } from "react-router-dom";
import { Link as MuiLink } from "@mui/material";

const NavLink = ({ to, ...rest }) => (
  <MuiLink
    component={RouterNavLink}
    to={to}
    {...rest}
    underline="none"
    variant="button"
    color="text.primary"
    sx={{ my: 1, mx: 1.5 }}
  />
);

export default NavLink;
