import { command, getRequestEvent } from "$app/server";
import { z } from "zod";

const COOKIE_NAME = "nocturne-setup-complete";
const COOKIE_MAX_AGE = 60 * 60 * 24 * 30; // 30 days

/**
 * Mark onboarding as complete, bypassing the per-step checks.
 * Sets the nocturne-setup-complete cookie so the root layout stops redirecting.
 */
export const markSetupComplete = command(z.void(), async () => {
  const event = getRequestEvent();

  event.cookies.set(COOKIE_NAME, "true", {
    path: "/",
    httpOnly: true,
    secure: event.url.protocol === "https:",
    sameSite: "lax",
    maxAge: COOKIE_MAX_AGE,
  });

  return { success: true };
});

/**
 * Create the initial tenant during setup.
 * Calls POST /api/v4/setup/tenant before account creation.
 */
export const setupTenant = command(
  z.object({ slug: z.string(), displayName: z.string() }),
  async (input) => {
    const event = getRequestEvent();
    const apiBaseUrl = event.locals.apiBaseUrl;

    const res = await event.fetch(`${apiBaseUrl}/api/v4/setup/tenant`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(input),
    });

    if (!res.ok) {
      const body = await res.text();
      throw new Error(body || `Setup tenant failed (${res.status})`);
    }

    return await res.json();
  },
);

/**
 * Validate a slug for availability during setup.
 * Calls POST /api/v4/setup/validate-slug before account creation.
 */
export const validateSetupSlug = command(
  z.object({ slug: z.string() }),
  async (input) => {
    const event = getRequestEvent();
    const apiBaseUrl = event.locals.apiBaseUrl;

    const res = await event.fetch(`${apiBaseUrl}/api/v4/setup/validate-slug`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(input),
    });

    if (!res.ok) {
      return { isValid: false, message: "Could not validate slug" };
    }

    return await res.json();
  },
);
