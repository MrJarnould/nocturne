import { redirect } from "@sveltejs/kit";
import type { PageServerLoad } from "./$types";

export const load: PageServerLoad = async ({ locals }) => {
  if (locals.isAuthenticated) {
    return { setupRequired: false };
  }

  // No subjects → setup required (covers fresh install and recovery mode)
  // Subjects exist → log in
  let setupRequired = false;
  try {
    const status = await locals.apiClient.passkey.getAuthStatus();
    setupRequired = status?.setupRequired ?? false;
  } catch {
    // If the API call fails, default to login
  }

  if (setupRequired) {
    // Stay on the setup page — it will show the two-step flow
    // (tenant identity → account creation)
    return { setupRequired: true };
  } else {
    redirect(302, "/auth/login?returnUrl=/setup");
  }
};
