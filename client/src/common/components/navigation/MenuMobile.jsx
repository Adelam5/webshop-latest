import Box from "@mui/material/Box";
import IconButton from "@mui/material/IconButton";
import Typography from "@mui/material/Typography";
import Menu from "@mui/material/Menu";
import MenuIcon from "@mui/icons-material/Menu";
import { pages } from "./navigation.data";
import NavLink from "./NavLink";

const MenuMobile = ({ handleCloseNavMenu, handleOpenNavMenu, anchorElNav }) => {
  return (
    <Box sx={{ flexGrow: 1, display: { xs: "flex", md: "none" } }}>
      <IconButton
        size="large"
        aria-label="account of current user"
        aria-controls="menu-appbar"
        aria-haspopup="true"
        onClick={handleOpenNavMenu}
        color="inherit"
      >
        <MenuIcon />
      </IconButton>
      <Menu
        id="menu-appbar"
        anchorEl={anchorElNav}
        anchorOrigin={{
          vertical: "bottom",
          horizontal: "left"
        }}
        keepMounted
        transformOrigin={{
          vertical: "top",
          horizontal: "left"
        }}
        open={Boolean(anchorElNav)}
        onClose={handleCloseNavMenu}
        sx={{
          display: { xs: "block", md: "none" }
        }}
      >
        {pages.map((page) => (
          <NavLink
            to={page.to}
            key={page.name}
            onClick={handleCloseNavMenu}
            underline="hover"
          >
            <Typography textAlign="center" mx={2}>
              {page.name}
            </Typography>
          </NavLink>
        ))}
      </Menu>
    </Box>
  );
};

export default MenuMobile;
