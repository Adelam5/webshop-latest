import Container from "@mui/material/Container";
import ProductView from "features/products/details/ProductView";

const ProductDetails = () => {
  return (
    <Container component="main" maxWidth="lg">
      <ProductView />
    </Container>
  );
};
export default ProductDetails;
