import List from "@mui/material/List";
import Grid from "@mui/material/Grid";
import Typography from "@mui/material/Typography";
import Divider from "@mui/material/Divider";
import useGetDeliveryMethods from "./useGetDeliveryMethods";
import DeliveryMethodItem from "./DeliveryMethodItem";

const DeliveryMethodsList = () => {
  const { data: deliveryMethods } = useGetDeliveryMethods();

  return (
    <Grid item xs={12} md={6}>
      <Divider textAlign="left">
        <Typography variant="button" component="h6">
          Choose delivery method
        </Typography>
      </Divider>
      <List dense={true}>
        {deliveryMethods?.map((method) => (
          <DeliveryMethodItem key={method.id} method={method} />
        ))}
      </List>
    </Grid>
  );
};
export default DeliveryMethodsList;
