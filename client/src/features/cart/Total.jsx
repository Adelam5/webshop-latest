import List from "@mui/material/List";
import Grid from "@mui/material/Grid";
import Typography from "@mui/material/Typography";
import Divider from "@mui/material/Divider";
import ListItem from "@mui/material/ListItem";
import ListItemText from "@mui/material/ListItemText";
import { Chip } from "@mui/material";

const Total = ({ cart }) => {
  return (
    <Grid item xs={12} md={6}>
      <Divider textAlign="left">
        <Typography variant="button" component="h6">
          Total
        </Typography>
      </Divider>
      <List dense={true}>
        <ListItem
          secondaryAction={
            <Chip
              label={`${cart.subtotal.toFixed(2)} USD`}
              sx={{ marginLeft: 5, minWidth: 110 }}
            />
          }
        >
          <ListItemText
            primary={<Typography variant="button">Subtotal: </Typography>}
            sx={{ textAlign: "end", marginRight: "120px" }}
          />
        </ListItem>
        <ListItem
          secondaryAction={
            <Chip
              label={`${cart.deliveryMethodPrice.toFixed(2)} USD`}
              sx={{ marginLeft: 5, minWidth: 110 }}
            />
          }
        >
          <ListItemText
            primary={<Typography variant="button">Delivery: </Typography>}
            sx={{ textAlign: "end", marginRight: "120px" }}
          />
        </ListItem>
        <ListItem
          secondaryAction={
            <Chip
              label={`${(cart.subtotal + cart.deliveryMethodPrice).toFixed(
                2
              )} USD`}
              sx={{ marginLeft: 5, minWidth: 110 }}
            />
          }
        >
          <ListItemText
            primary={<Typography variant="button"> Total: </Typography>}
            sx={{ textAlign: "end", marginRight: "120px" }}
          />
        </ListItem>
      </List>
    </Grid>
  );
};
export default Total;
