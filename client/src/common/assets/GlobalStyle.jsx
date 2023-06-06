import { createGlobalStyle } from "styled-components";
import { GlobalStyles } from "@mui/material";

const GlobalStyle = createGlobalStyle`
a {
  position: relative;
  text-decoration: none;
}

a::before {
  content: '';
  position: absolute;
  width: 100%;
  height: 4px;
  border-radius: 4px;
  background-color: #fff;
  bottom: 0;
  left: 0;
  transform-origin: right;
  transform: scaleX(0);
  transition: transform .3s ease-in-out;
}

a.active::before {
  transform-origin: left;
  transform: scaleX(1);
}
`;

export default GlobalStyle;

export const GlobalStyleMui = () => {
  return (
    <>
      <GlobalStyles
        styles={{
          ul: { margin: 0, padding: 0, listStyle: "none" },
          a: { textDecoration: "none", color: "inherit" }
        }}
      />
    </>
  );
};
