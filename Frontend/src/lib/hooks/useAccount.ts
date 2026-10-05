import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import agent from "../api/agent.ts";
import type { LoginSchema } from "../schemas/loginSchema.ts";
import { useLocation, useNavigate } from "react-router";
import type { RegisterSchema } from "../schemas/registerSchema.ts";
import { toast } from "react-toastify";

export const useAccount = () => {
  const queryClient = useQueryClient();
  const navigate = useNavigate();
  const location = useLocation();

  const loginUser = useMutation({
    mutationFn: async (creds: LoginSchema) => {
      await agent.post("/login?useCookies=true", creds);
    },
    onSuccess: async () => {
      await queryClient.invalidateQueries({
        queryKey: ["user"],
      });
      await navigate("/activities");
    },
  });

  const registerUser = useMutation({
    mutationFn: async (creds: RegisterSchema) => {
      await agent.post("/accounts/register", creds);
    },
    onSuccess: async () => {
      toast.success("Register successful - you can now login");
      navigate("/login");
    },
  });

  const logoutUser = useMutation({
    mutationFn: async () => {
      await agent.post("/accounts/signout");
    },
    onSuccess: () => {
      queryClient.removeQueries({ queryKey: ["user"] });
      queryClient.removeQueries({ queryKey: ["activities"] });
      navigate("/");
    },
  });

  const { data: currentUser, isLoading: loadingUserInfo } = useQuery({
    queryKey: ["user"],
    queryFn: async () => {
      console.log("Fetching user info...");
      const response = await agent.get<User>("/accounts/userinfo");
      return response.data;
    },
    enabled:
      !queryClient.getQueryData(["user"]) &&
      location.pathname !== "/login" &&
      location.pathname !== "/register",
  });

  return {
    loginUser,
    logoutUser,
    registerUser,
    currentUser,
    loadingUserInfo,
  };
};
