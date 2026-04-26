import { svelte } from "@sveltejs/vite-plugin-svelte";
import { playwright } from "@vitest/browser-playwright";
import { defineConfig } from "vitest/config";

export default defineConfig({
  plugins: [svelte()],
  test: {
    include: ["src/**/*.svelte.test.ts"],
    setupFiles: ["vitest-browser-svelte"],
    browser: {
      enabled: true,
      provider: playwright(),
      instances: [{ browser: "chromium" }],
    },
    alias: {
      $lib: new URL("./src/lib", import.meta.url).pathname,
      $api: new URL("./src/lib/api/", import.meta.url).pathname,
      "$api-clients": new URL(
        "./src/lib/api/generated/nocturne-api-client",
        import.meta.url
      ).pathname,
      $routes: new URL("./src/routes", import.meta.url).pathname,
    },
  },
});
