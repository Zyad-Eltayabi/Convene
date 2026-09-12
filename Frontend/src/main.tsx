import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import App from "./app/layouts/App";
import "./app/layouts/styles.css";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    
    <App />
  </StrictMode>,
);
