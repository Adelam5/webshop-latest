import Container from "@mui/material/Container";
import Typography from "@mui/material/Typography";
import { hero } from "./hero.data";

const Hero = () => {
  return (
    <Container
      disableGutters
      maxWidth="sm"
      component="main"
      sx={{ pt: 8, pb: 6 }}
    >
      <Typography
        component="h1"
        variant="h2"
        align="center"
        color="text.primary"
        gutterBottom
      >
        {hero.title}
      </Typography>
      <Typography
        variant="h5"
        align="center"
        color="text.secondary"
        component="p"
      >
        {hero.text}
      </Typography>
    </Container>
  );
};
export default Hero;
