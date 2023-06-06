import { useState } from "react";
import AppBar from "@mui/material/AppBar";
import Container from "@mui/material/Container";
import Toolbar from "@mui/material/Toolbar";
import Link from "../link/Link";
import LogoDesktop from "./LogoDesktop";
import LogoMobile from "./LogoMobile";
import MenuDesktop from "./MenuDesktop";
import MenuMobile from "./MenuMobile";
import Settings from "./Settings";
import { useCurrentUser } from "features/authentication/common/useCurrentUser";
import CartBadge from "./CartBadge";
import useGetCart from "features/cart/useGetCart";

function Navigation() {
  const [anchorElNav, setAnchorElNav] = useState(null);
  const [anchorElUser, setAnchorElUser] = useState(null);

  const { data: user } = useCurrentUser();
  const { data: cart } = useGetCart();

  const handleOpenNavMenu = (event) => {
    setAnchorElNav(event.currentTarget);
  };
  const handleOpenUserMenu = (event) => {
    setAnchorElUser(event.currentTarget);
  };

  const handleCloseNavMenu = () => {
    setAnchorElNav(null);
  };

  const handleCloseUserMenu = () => {
    setAnchorElUser(null);
  };

  return (
    <AppBar position="static">
      <Container maxWidth="xl">
        <Toolbar disableGutters>
          <LogoDesktop />
          <MenuMobile
            handleCloseNavMenu={handleCloseNavMenu}
            handleOpenNavMenu={handleOpenNavMenu}
            anchorElNav={anchorElNav}
          />
          <LogoMobile />
          <MenuDesktop handleCloseNavMenu={handleCloseNavMenu} />
          {user?.id ? (
            <>
              <CartBadge itemsQuantity={cart?.itemsQuantity} />
              <Settings
                handleOpenUserMenu={handleOpenUserMenu}
                handleCloseUserMenu={handleCloseUserMenu}
                anchorElUser={anchorElUser}
              />
            </>
          ) : (
            <>
              <Link to="/login" mr={2}>
                LOGIN
              </Link>
              <Link to="/register">REGISTER</Link>
            </>
          )}
        </Toolbar>
      </Container>
    </AppBar>
  );
}
export default Navigation;
