import axios from "axios";
import { store } from "../stores/Store.ts";
import { toast } from "react-toastify";
import { router } from "../../app/router/Route.tsx";
const agent = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
});

agent.interceptors.request.use((config) => {
  store.uiStore.isBusy();
  return config;
});

agent.interceptors.response.use(
  async (response) => {
    await new Promise((resolve) => setTimeout(resolve, 1000));
    store.uiStore.isIdle();
    return response;
  },
  async (error) => {
    await new Promise((resolve) => setTimeout(resolve, 1000));
    store.uiStore.isIdle();
    console.log("Error in API response:", error.response);
    const status = error.response?.status;
    switch (status) {
      case 400:
        toast.error("Bad Request");
        break;
      case 401:
        toast.error("Unauthorized");
        break;
      case 403:
        toast.error("Forbidden");
        break;
      case 404:
        router.navigate("/not-found");
        break;
      case 500:
        toast.error("Internal Server Error");
        break;
    }
    return Promise.reject(error);
  },
);

export default agent;
