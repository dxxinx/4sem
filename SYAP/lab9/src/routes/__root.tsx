/* eslint-disable react-refresh/only-export-components */
import { Link, Outlet, createRootRouteWithContext } from "@tanstack/react-router";
import { useRecoilCallback, useRecoilValue } from "recoil";
import type { RouterContext } from "../router";
import { useAuth } from "../contexts/AuthContext";
import { cartCountState, uiSettingsState, cartState, favoritesState } from "../store";

import { useEffect } from "react";

const RootLayout = () => {
  const { isAuthenticated, logout, user } = useAuth();
  const cartCount = useRecoilValue(cartCountState);
  const { theme } = useRecoilValue(uiSettingsState);

  useEffect(() => {
    document.body.className = theme === "dark" ? "dark-theme" : "light-theme";
  }, [theme]);

  const resetSettings = useRecoilCallback(({ reset }) => () => {
    reset(uiSettingsState);
    reset(cartState);
    reset(favoritesState);
  }, []);

  return (
    <>
      <nav className="navbar">
        <div className="nav-links">
          <Link to="/" activeOptions={{ exact: true }}>
            Главная
          </Link>
          <Link to="/catalog">Каталог</Link>
          <Link to="/cart" className="cart-link">
            Корзина {cartCount > 0 && <span className="cart-badge">{cartCount}</span>}
          </Link>
        </div>
        <div className="nav-actions">
          <button type="button" className="link-button" onClick={resetSettings}>
            Сбросить настройки
          </button>
          {isAuthenticated ? (
            <button type="button" className="link-button" onClick={logout}>
              Выйти{user ? `: ${user.username}` : ""}
            </button>
          ) : (
            <Link to="/login">Войти</Link>
          )}
        </div>
      </nav>
      <Outlet />
    </>
  );
};

export const Route = createRootRouteWithContext<RouterContext>()({
  component: RootLayout
});
