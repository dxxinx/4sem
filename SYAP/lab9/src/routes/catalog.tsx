/* eslint-disable react-refresh/only-export-components */
import { createFileRoute, redirect } from "@tanstack/react-router";
import { z } from "zod";
import { Catalog } from "../components/Catalog";

const catalogSearchSchema = z.object({
  category: z.string().min(1).optional()
});

const CatalogPage = () => {
  const { category } = Route.useSearch();

  return <Catalog category={category} />;
};

export const Route = createFileRoute("/catalog")({
  validateSearch: search => catalogSearchSchema.parse(search),
  beforeLoad: ({ context }) => {
    if (!context.auth.isAuthenticated) {
      throw redirect({ to: "/login" });
    }
  },
  component: CatalogPage
});
