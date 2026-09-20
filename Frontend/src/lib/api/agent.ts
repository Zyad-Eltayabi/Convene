import axios from "axios";
import { store } from "../stores/Store.ts";
const agent = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
});

agent.interceptors.request.use(
  (config) => {
    store.uiStore.isBusy();
    return config;
  },
  (error) => Promise.reject(error),
);

agent.interceptors.response.use(async (response) => {
  try {
    // await new Promise((resolve) => setTimeout(resolve, 2000));
    return response;
  } catch (error) {
    console.log(error);
    return Promise.reject(error);
  } finally {
    store.uiStore.isIdle();
  }
});

export default agent;
