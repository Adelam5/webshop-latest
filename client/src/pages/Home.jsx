import Container from "@mui/material/Container";
import Hero from "common/components/hero/Hero";
import ProductsList from "features/products/list/ProductsList";

const Home = () => {
  return (
    <>
      <Hero />
      <Container maxWidth="lg" component="main">
        <ProductsList />
      </Container>
    </>
  );
};

export default Home;
