import { render } from "vitest-browser-svelte";
import { expect, test } from "vitest";
import DayOfWeekPicker from "./DayOfWeekPicker.svelte";

test("renders all seven days", async () => {
  const screen = await render(DayOfWeekPicker);

  for (const day of ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"]) {
    await expect
      .element(screen.getByRole("button", { name: day }))
      .toBeVisible();
  }
});

test("toggles a day on click", async () => {
  const screen = await render(DayOfWeekPicker, { activeDays: [1, 3] });

  const fri = screen.getByRole("button", { name: "Fri" });
  await fri.click();

  // Fri should now have the active class
  await expect.element(fri).toHaveClass(/bg-primary/);
});

test("deactivates an active day on click", async () => {
  const screen = await render(DayOfWeekPicker, { activeDays: [1, 3] });

  const mon = screen.getByRole("button", { name: "Mon" });
  await mon.click();

  // Mon should no longer have the active class
  await expect.element(mon).not.toHaveClass(/bg-primary/);
});
