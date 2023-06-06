import Grid from "@mui/material/Grid";
import ProductCard from "./ProductCard";
import useProducts from "features/products/list/useProducts";

const ProductsList = () => {
  const { data: products } = useProducts();

  return (
    <Grid container spacing={4}>
      {products?.map((product) => (
        <Grid item key={product?.id} xs={12} sm={6} md={4}>
          <ProductCard product={product} />
        </Grid>
      ))}
    </Grid>
  );
};
export default ProductsList;
