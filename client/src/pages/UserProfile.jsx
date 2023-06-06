import Avatar from "@mui/material/Avatar";
import Grid from "@mui/material/Grid";
import Typography from "@mui/material/Typography";
import Box from "@mui/material/Box";
import Divider from "@mui/material/Divider";
import List from "@mui/material/List";
import ListItem from "@mui/material/ListItem";
import ListItemText from "@mui/material/ListItemText";
import { useProfile } from "features/customer/useCustomer";
import { useCustomerOrders } from "features/customer/useCustomerOrders";

const UserProfile = () => {
  const { data: user } = useProfile();
  const { data: orders } = useCustomerOrders();

  return (
    <Box sx={{ p: 2 }}>
      <Grid container spacing={2} alignItems="center">
        <Grid item>
          <Avatar
            alt={`${user?.firstName} ${user?.lastName}`}
            sx={{ width: 100, height: 100 }}
          />
        </Grid>
        <Grid item>
          <Typography variant="h5" component="div">
            {`${user?.firstName} ${user?.lastName}`}
          </Typography>
          <Typography variant="subtitle1" color="text.secondary">
            {user?.email}
          </Typography>
        </Grid>
      </Grid>
      <Divider sx={{ my: 2 }} />
      <Typography variant="h6" component="div">
        Orders
      </Typography>
      <List>
        {orders?.map((order) => (
          <ListItem key={order?.id}>
            <ListItemText
              primary={`Total: ${order?.total} - Payment Status: ${order?.paymentStatus}`}
              secondary={`Created on: ${new Date(
                order?.createdOnUtc
              ).toLocaleString()} - Modified on: ${new Date(
                order?.modifiedOnUtc
              ).toLocaleString()}`}
            />
          </ListItem>
        ))}
      </List>
    </Box>
  );
};

export default UserProfile;
