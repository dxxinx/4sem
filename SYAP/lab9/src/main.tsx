import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import { RecoilRoot } from "recoil";
import App from "./App.tsx";
import { AuthProvider } from "./contexts/AuthContext";
import "./index.css";

const queryClient = new QueryClient();

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <RecoilRoot>
      <AuthProvider>
        <QueryClientProvider client={queryClient}>
          <App />
        </QueryClientProvider>
      </AuthProvider>
    </RecoilRoot>
  </StrictMode>,
)
