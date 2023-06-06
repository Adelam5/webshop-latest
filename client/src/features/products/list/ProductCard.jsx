import Card from "@mui/material/Card";
import CardMedia from "@mui/material/CardMedia";
import CardContent from "@mui/material/CardContent";
import Typography from "@mui/material/Typography";
import CardActions from "@mui/material/CardActions";
import Link from "common/components/link/Link";
import useAddItem from "common/hooks/useAddItem";
import { LoadingButton } from "@mui/lab";
import { useIsFetching, useIsMutating } from "@tanstack/react-query";
import { useCurrentUser } from "features/authentication/common/useCurrentUser";
import { useNavigate } from "react-router-dom";

const ProductCard = ({ product }) => {
  const { mutate: addItem } = useAddItem();
  const { data: user } = useCurrentUser();
  const isFetching = useIsFetching();
  const isMutating = useIsMutating();

  const navigate = useNavigate();

  return (
    <Card
      sx={{
        height: "100%",
        display: "flex",
        flexDirection: "column"
      }}
    >
      <CardMedia
        component="img"
        sx={{
          height: "250px"
        }}
        image={product?.photo_Url}
        alt="random"
      />
      <CardContent sx={{ flexGrow: 1 }}>
        <Typography gutterBottom variant="h5" component="h2">
          {product?.name}
        </Typography>
        <Typography>{product?.price.toFixed(2)} USD</Typography>
      </CardContent>
      <CardActions>
        <Link to={`/products/${product?.id}`}>VIEW</Link>
        <LoadingButton
          loadingPosition="start"
          loading={isFetching > 0 || isMutating > 0}
          onClick={user ? () => addItem(product) : () => navigate("/login")}
        >
          Add to cart
        </LoadingButton>
      </CardActions>
    </Card>
  );
};
export default ProductCard;
