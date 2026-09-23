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
    const { data, status } = error.response;
    switch (status) {
      case 400:
        if (data.errors) {
          const modelStateErrors: string[] = [];
          for (const key in data.errors) {
            if (data.errors[key]) {
              modelStateErrors.push(data.errors[key]);
            }
          }
          throw modelStateErrors.flat();
        } else {
          toast.error(data.title);
        }
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
        router.navigate("/server-error", {
          state: { error: data },
        });
        break;
    }
    return Promise.reject(error);
  },
);

export default agent;
