import { createFileRoute, redirect } from "@tanstack/react-router";
import { RegistrationForm } from "../components/RegistrationForm";

export const Route = createFileRoute("/login")({
  beforeLoad: ({ context }) => {
    if (context.auth.isAuthenticated) {
      throw redirect({ to: "/catalog" });
    }
  },
  component: RegistrationForm
});
