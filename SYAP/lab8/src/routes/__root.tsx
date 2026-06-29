/* eslint-disable react-refresh/only-export-components */
import { Link, Outlet, createRootRouteWithContext } from "@tanstack/react-router";
import type { RouterContext } from "../router";
import { useAuth } from "../contexts/AuthContext";

const RootLayout = () => {
  const { isAuthenticated, logout, user } = useAuth();

  return (
    <>
      <nav className="navbar">
        <Link to="/" activeOptions={{ exact: true }}>
          Главная
        </Link>
        <Link to="/catalog">Каталог</Link>
        {isAuthenticated ? (
          <button type="button" className="link-button" onClick={logout}>
            Выйти{user ? `: ${user.username}` : ""}
          </button>
        ) : (
          <Link to="/login">Войти</Link>
        )}
      </nav>
      <Outlet />
    </>
  );
};

export const Route = createRootRouteWithContext<RouterContext>()({
  component: RootLayout
});
