import Typography from "@mui/material/Typography";
import AdbIcon from "@mui/icons-material/Adb";
import { Link } from "react-router-dom";

const LogoDesktop = () => {
  return (
    <>
      <AdbIcon sx={{ display: { xs: "none", md: "flex" }, mr: 1 }} />
      <Typography
        variant="h6"
        noWrap
        component={Link}
        to="/"
        sx={{
          mr: 2,
          display: { xs: "none", md: "flex" },
          fontFamily: "monospace",
          fontWeight: 700,
          letterSpacing: ".3rem",
          color: "inherit",
          textDecoration: "none"
        }}
      >
        WebShop
      </Typography>
    </>
  );
};
export default LogoDesktop;
