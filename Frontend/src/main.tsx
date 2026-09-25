import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "./app/layout/styles.css";
import "@fontsource/roboto/300.css";
import "@fontsource/roboto/400.css";
import "@fontsource/roboto/500.css";
import "@fontsource/roboto/700.css";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { ReactQueryDevtools } from "@tanstack/react-query-devtools";
import { router } from "./app/router/Route.tsx";
import { RouterProvider } from "react-router";
import { store, StoreContext } from "./lib/stores/Store.ts";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { LocalizationProvider } from "@mui/x-date-pickers";
import { AdapterDateFns } from "@mui/x-date-pickers/AdapterDateFns";

const queryClient = new QueryClient();
createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <LocalizationProvider dateAdapter={AdapterDateFns}>
      <StoreContext.Provider value={store}>
        <QueryClientProvider client={queryClient}>
          <ReactQueryDevtools initialIsOpen={false} />
          <ToastContainer
            position="bottom-right"
            hideProgressBar={true}
            theme="colored"
          />
          <RouterProvider router={router} />
        </QueryClientProvider>
      </StoreContext.Provider>
    </LocalizationProvider>
  </StrictMode>,
);
