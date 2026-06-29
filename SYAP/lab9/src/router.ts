import { createRouter } from "@tanstack/react-router";
import { routeTree } from "./routeTree.gen";
import type { AuthContextValue } from "./contexts/AuthContext";

export interface RouterContext {
  auth: AuthContextValue;
}

export const router = createRouter({
  routeTree,
  context: {
    auth: undefined!
  } satisfies RouterContext
});

declare module "@tanstack/react-router" {
  interface Register {
    router: typeof router;
  }
}
