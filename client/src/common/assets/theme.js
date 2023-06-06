import { createTheme } from "@mui/material/styles";
import { blue, pink } from "@mui/material/colors";

export const theme = createTheme({
  palette: {
    mode: "dark",
    primary: {
      main: blue[500]
    },
    secondary: {
      main: pink[300]
    },
    grey: {
      700: "#3e5060"
    },
    background: {
      default: "#001e3c",
      paper: "#0a1929"
    }
  }
});
