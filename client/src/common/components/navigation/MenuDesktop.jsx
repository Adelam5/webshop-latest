import Box from "@mui/material/Box";
import NavLink from "./NavLink";
import { pages } from "./navigation.data";

const MenuDesktop = ({ handleCloseNavMenu }) => {
  return (
    <Box sx={{ flexGrow: 1, display: { xs: "none", md: "flex" } }}>
      {pages.map((page) => (
        <NavLink
          to={`/${page.to}`}
          key={page.name}
          onClick={handleCloseNavMenu}
          sx={{ my: 2, color: "white", display: "block" }}
        >
          {page.name}
        </NavLink>
      ))}
    </Box>
  );
};
export default MenuDesktop;
