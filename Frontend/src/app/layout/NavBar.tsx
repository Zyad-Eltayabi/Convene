import { Group } from "@mui/icons-material";
import {
  Box,
  AppBar,
  Toolbar,
  Typography,
  Container,
  MenuItem,
  CircularProgress,
} from "@mui/material";
import { NavLink } from "react-router";
import MenuItemLink from "../shared/components/MenuItemLink";
import { useStore } from "../../lib/hooks/useStore";
import { Observer } from "mobx-react-lite";
import { useAccount } from "../../lib/hooks/useAccount";
import UserMenu from "./UserMenu";

export default function NavBar() {
  const { uiStore } = useStore();
  const { currentUser } = useAccount();
  return (
    <Box sx={{ flexGrow: 1 }}>
      <AppBar
        position="static"
        sx={{
          backgroundImage:
            "linear-gradient(135deg, #182a73 0%, #218aae 69%, #20a7ac 89%)",
          position: "relative",
        }}
      >
        <Observer>
          {() =>
            uiStore.isLoading ? (
              <CircularProgress
                color="secondary"
                aria-label="Loading…"
                sx={{
                  position: "fixed",
                  left: "50%",
                  top: "50%",
                  transform: "translate(-50%, -50%)",
                  zIndex: (theme) => theme.zIndex.modal + 1,
                }}
              />
            ) : null
          }
        </Observer>
        <Container maxWidth="xl">
          <Toolbar sx={{ display: "flex", justifyContent: "space-between" }}>
            <Box>
              <MenuItem
                sx={{ display: "flex", gap: 2 }}
                component={NavLink}
                to="/"
              >
                <Group fontSize="large" />
                <Typography variant="h4" fontWeight="bold">
                  Convene
                </Typography>
              </MenuItem>
            </Box>
            <Box sx={{ display: "flex" }}>
              <MenuItemLink to="/activities">Activities</MenuItemLink>
              <MenuItemLink to="/counter">Counter</MenuItemLink>
              <MenuItemLink to="/errors">Errors</MenuItemLink>
            </Box>
            <Box sx={{ display: "flex" }}>
              {currentUser ? (
                <>
                  <UserMenu></UserMenu>
                </>
              ) : (
                <>
                  <MenuItemLink to="/login">Login</MenuItemLink>
                  <MenuItemLink to="/register">Register</MenuItemLink>
                </>
              )}
            </Box>
          </Toolbar>
        </Container>
      </AppBar>
    </Box>
  );
}
