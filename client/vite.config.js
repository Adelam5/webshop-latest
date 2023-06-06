import react from "@vitejs/plugin-react";
import { defineConfig } from "vite";
import jsconfigPaths from "vite-jsconfig-paths";

// https://vitejs.dev/config/
export default defineConfig({
  server: {
    proxy: {
      "/api": {
        target: "https://localhost:7051",
        changeOrigin: true,
        secure: false
      }
    }
  },
  plugins: [react(), jsconfigPaths()],
  resolve: {
    alias: {
      "@mui/styled-engine": "@mui/styled-engine-sc"
    }
  }
});
