import Typography from "@mui/material/Typography";
import AdbIcon from "@mui/icons-material/Adb";
import { Link } from "react-router-dom";

const LogoMobile = () => {
  return (
    <>
      <AdbIcon sx={{ display: { xs: "flex", md: "none" }, mr: 1 }} />
      <Typography
        variant="h5"
        noWrap
        component={Link}
        to="/"
        sx={{
          mr: 2,
          display: { xs: "flex", md: "none" },
          flexGrow: 1,
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

export default LogoMobile;
