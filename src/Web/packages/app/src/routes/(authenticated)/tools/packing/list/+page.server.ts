import { redirect } from "@sveltejs/kit";
import type { PageServerLoad } from "./$types";

export const load: PageServerLoad = async ({ url }) => {
  const d = url.searchParams.get("d");
  throw redirect(301, d ? `/inventory/packing/list?d=${encodeURIComponent(d)}` : "/inventory/packing");
};
