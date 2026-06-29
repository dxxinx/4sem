/* eslint-disable react-refresh/only-export-components */
import { createContext, useContext, useMemo, useState } from "react";
import type { ReactNode } from "react";
import type { IFormData } from "../schemas/registrationSchema";

const AUTH_STORAGE_KEY = "lab7_user";

export type User = Pick<IFormData, "email" | "username" | "city" | "occupation">;

interface AuthContextValue {
  user: User | null;
  isAuthenticated: boolean;
  login: (user: User) => void;
  logout: () => void;
}

const AuthContext = createContext<AuthContextValue | null>(null);

const isUser = (value: unknown): value is User => {
  if (!value || typeof value !== "object") return false;

  const candidate = value as Record<keyof User, unknown>;

  return (
    typeof candidate.email === "string" &&
    typeof candidate.username === "string" &&
    typeof candidate.city === "string" &&
    typeof candidate.occupation === "string"
  );
};

export const AuthProvider = ({ children }: { children: ReactNode }) => {
  const [user, setUser] = useState<User | null>(() => {
    const savedUser = localStorage.getItem(AUTH_STORAGE_KEY);
    if (!savedUser) return null;

    try {
      const parsedUser: unknown = JSON.parse(savedUser);
      return isUser(parsedUser) ? parsedUser : null;
    } catch {
      localStorage.removeItem(AUTH_STORAGE_KEY);
      return null;
    }
  });

  const login = (nextUser: User) => {
    setUser(nextUser);
    localStorage.setItem(AUTH_STORAGE_KEY, JSON.stringify(nextUser));
  };

  const logout = () => {
    setUser(null);
    localStorage.removeItem(AUTH_STORAGE_KEY);
  };

  const value = useMemo(
    () => ({
      user,
      isAuthenticated: Boolean(user),
      login,
      logout
    }),
    [user]
  );

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth must be used inside AuthProvider");
  }

  return context;
};
