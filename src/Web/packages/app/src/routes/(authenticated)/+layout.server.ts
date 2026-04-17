import { redirect } from "@sveltejs/kit";
import type { LayoutServerLoad } from "./$types";
import { checkOnboarding } from "$lib/server/onboarding-check";

export const load: LayoutServerLoad = async ({ locals, cookies, url }) => {
  if (!locals.isAuthenticated || !locals.user) {
    const returnUrl = encodeURIComponent(url.pathname + url.search);
    throw redirect(303, `/auth/login?returnUrl=${returnUrl}`);
  }

  const onboarding = checkOnboarding(cookies);
  if (!onboarding.isComplete) {
    throw redirect(303, "/setup");
  }

  return {
    user: locals.user,
  };
};
