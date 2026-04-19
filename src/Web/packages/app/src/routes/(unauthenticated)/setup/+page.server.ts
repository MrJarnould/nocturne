import { redirect } from "@sveltejs/kit";
import type { PageServerLoad } from "./$types";

export const load: PageServerLoad = async ({ locals }) => {
  if (locals.isAuthenticated) {
    return {};
  }

  // No subjects → create account (covers fresh install and recovery mode)
  // Subjects exist → log in
  let setupRequired = false;
  try {
    const status = await locals.apiClient.passkey.getAuthStatus();
    setupRequired = status?.setupRequired ?? false;
  } catch {
    // If the API call fails, default to login
  }

  if (setupRequired) {
    redirect(302, "/setup/account");
  } else {
    redirect(302, "/auth/login?returnUrl=/setup");
  }
};
