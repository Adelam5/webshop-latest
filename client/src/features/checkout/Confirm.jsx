import { ListItem } from "@mui/material";
import List from "@mui/material/List";
import Total from "features/cart/Total";
import { useStore } from "store";
import ListItemText from "@mui/material/ListItemText";
import Typography from "@mui/material/Typography";
import Divider from "@mui/material/Divider";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import useOrder from "common/hooks/useOrder";

const Confirm = () => {
  const activeStep = useStore((state) => state.activeStep);
  const setActiveStep = useStore((state) => state.setActiveStep);
  const order = useOrder();

  return (
    <div>
      <Divider textAlign="left">
        <Typography variant="button" component="h6">
          Customer
        </Typography>
      </Divider>
      {order?.customer.firstName} {order?.customer.lastName}
      <Divider textAlign="left">
        <Typography variant="button" component="h6">
          Delivery Address
        </Typography>
      </Divider>
      {order?.customer.address.street} <br /> {order?.customer.address.zipcode}{" "}
      {order?.customer.address.city} <br /> {order?.customer.address.state}{" "}
      <Divider textAlign="left">
        <Typography variant="button" component="h6">
          Cart
        </Typography>
      </Divider>
      <List>
        {order?.items.map((item) => (
          <ListItem
            key={item.id}
            secondaryAction={`${(item.price * item.quantity).toFixed(2)} USD`}
          >
            <ListItemText
              primary={item.name}
              secondary={`Quantity: ${item.quantity}`}
            />
          </ListItem>
        ))}
      </List>
      <Total cart={order} />
      <Box sx={{ display: "flex", justifyContent: "flex-end" }}>
        <Button
          onClick={() => setActiveStep(activeStep - 1)}
          sx={{ mt: 3, ml: 1 }}
        >
          Back
        </Button>

        <Button
          onClick={() => setActiveStep(activeStep + 1)}
          disabled={!order?.deliveryMethodId}
          variant="contained"
          sx={{ mt: 3, ml: 1 }}
        >
          Confirm Order
        </Button>
      </Box>
    </div>
  );
};
export default Confirm;
