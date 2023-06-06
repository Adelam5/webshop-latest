import { Link as RouterLink } from "react-router-dom";
import { Link as MuiLink } from "@mui/material";

const Link = ({ to, ...rest }) => (
  <MuiLink component={RouterLink} to={to} {...rest} underline="hover" />
);

export default Link;
