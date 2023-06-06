import Box from "@mui/material/Box";
import IconButton from "@mui/material/IconButton";
import Typography from "@mui/material/Typography";
import Menu from "@mui/material/Menu";
import Avatar from "@mui/material/Avatar";
import Tooltip from "@mui/material/Tooltip";
import MenuItem from "@mui/material/MenuItem";
import { settings } from "./navigation.data";
import { useLogout } from "./useLogout";
import BackdropSpinner from "./../spinner/BackdropSpinner";
import { Link } from "react-router-dom";

const Settings = ({
  handleOpenUserMenu,
  handleCloseUserMenu,
  anchorElUser
}) => {
  const { refetch: logout, isFetching } = useLogout();

  const handleLogout = () => {
    handleCloseUserMenu();
    logout();
  };

  if (isFetching) {
    <BackdropSpinner />;
  }

  return (
    <Box sx={{ marginLeft: 2, flexGrow: 0 }}>
      <Tooltip title="Open settings">
        <IconButton onClick={handleOpenUserMenu}>
          <Avatar></Avatar>
        </IconButton>
      </Tooltip>
      <Menu
        sx={{ mt: "45px" }}
        id="menu-appbar"
        anchorEl={anchorElUser}
        anchorOrigin={{
          vertical: "top",
          horizontal: "right"
        }}
        keepMounted
        transformOrigin={{
          vertical: "top",
          horizontal: "right"
        }}
        open={Boolean(anchorElUser)}
        onClose={handleCloseUserMenu}
      >
        {settings.map((setting) => (
          <MenuItem key={setting?.name} onClick={handleCloseUserMenu}>
            <Link to={setting?.to}>
              <Typography textAlign="center">{setting?.name}</Typography>
            </Link>
          </MenuItem>
        ))}
        <MenuItem onClick={handleLogout}>
          <Typography textAlign="center">Logout</Typography>
        </MenuItem>
      </Menu>
    </Box>
  );
};
export default Settings;
