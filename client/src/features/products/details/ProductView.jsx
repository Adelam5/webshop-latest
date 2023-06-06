import useProduct from "features/products/details/useProductDetails";
import { useParams } from "react-router-dom";
import Card from "@mui/material/Card";
import CardMedia from "@mui/material/CardMedia";
import Typography from "@mui/material/Typography";
import CardContent from "@mui/material/CardContent";
import CardActions from "@mui/material/CardActions";
import Box from "@mui/material/Box";
import Chip from "@mui/material/Chip";
import useAddItem from "common/hooks/useAddItem";
import { LoadingButton } from "@mui/lab";
import { useIsFetching, useIsMutating } from "@tanstack/react-query";

const ProductView = () => {
  const { productId } = useParams();
  const { data: product } = useProduct(productId);
  const { mutate: addItem } = useAddItem();
  const isFetching = useIsFetching();
  const isMutating = useIsMutating();

  return (
    <Card
      sx={{
        marginTop: "10px",
        height: "100%",
        display: "flex"
      }}
    >
      <CardMedia
        component="img"
        sx={{
          maxWidth: "600px",
          height: "550px"
        }}
        image={product?.photoUrl}
        alt="random"
      />
      <Box
        sx={{
          width: "100%",
          display: "flex",
          flexDirection: "column",
          justifyContent: "space-between"
        }}
      >
        <CardContent sx={{ flexGrow: 1 }}>
          <Typography gutterBottom variant="h5" component="h2">
            {product?.name}
          </Typography>
          <Typography>
            {product?.description} Lorem ipsum dolor sit, amet consectetur
            adipisicing elit. Aspernatur provident minus commodi, debitis cumque
            facere eaque iusto perferendis culpa id repellendus incidunt porro
            nesciunt similique numquam nobis laudantium ipsum qui! <br /> <br />
            Lorem ipsum dolor sit amet consectetur, adipisicing elit. Magnam
            repellat recusandae rem harum quibusdam facilis aperiam, natus iusto
            alias veritatis. Tenetur magnam debitis neque magni sapiente,
            doloribus harum aliquid aut.
          </Typography>
        </CardContent>
        <CardContent sx={{ justifyContent: "end" }}>
          <Chip label={`${product?.price.toFixed(2)} USD`} />
        </CardContent>
        <CardActions>
          <LoadingButton
            loading={isFetching > 0 || isMutating > 0}
            fullWidth
            variant="contained"
            onClick={() => {
              addItem(product);
            }}
          >
            Add to cart
          </LoadingButton>
        </CardActions>
      </Box>
    </Card>
  );
};

export default ProductView;
