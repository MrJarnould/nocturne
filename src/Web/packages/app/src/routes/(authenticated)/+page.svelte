<script lang="ts">
  import {
    CurrentBGDisplay,
    GlucoseChartCard,
    OnboardingProgress,
    RecentEntriesCard,
    RecentTreatmentsCard,
    WidgetGrid,
  } from "$lib/components/dashboard";
  import { getSettingsStore } from "$lib/stores/settings-store.svelte";
  import { dashboardTopWidgets } from "$lib/stores/appearance-store.svelte";
  import { WidgetId } from "$lib/api/generated/nocturne-api-client";
  import { isWidgetEnabled } from "$lib/types/dashboard-widgets";
  import { coachmark } from "@nocturne/coach";
  import type { PageData } from "./$types";

  const { data }: { data: PageData } = $props();

  const settingsStore = getSettingsStore();

  // Get widgets array from settings (for main section visibility)
  const widgets = $derived(settingsStore.features?.widgets);

  // Helper to check if a main section is enabled
  const isMainEnabled = (id: (typeof WidgetId)[keyof typeof WidgetId]) =>
    isWidgetEnabled(widgets, id);

  // Get enabled top widgets from persisted appearance store
  const topWidgets = $derived(dashboardTopWidgets.current);

  // Get focusHours setting for chart default time range
  const focusHours = $derived(settingsStore.features?.display?.focusHours ?? 3);

  // Algorithm prediction settings - controls whether predictions are calculated
  const predictionEnabled = $derived(
    settingsStore.algorithm?.prediction?.enabled ?? true
  );
</script>

<div class="@container p-3 @md:p-6 space-y-3 @md:space-y-6">
  <CurrentBGDisplay />

  <OnboardingProgress />

  {#if isMainEnabled(WidgetId.Statistics)}
    <div {@attach coachmark({
      key: "dashboard-discovery.widgets",
      title: "Your stats at a glance",
      description: "These widgets show your key stats at a glance \u2014 customize them in Appearance settings.",
    })}>
      <WidgetGrid widgets={topWidgets} maxWidgets={3} />
    </div>
  {/if}

  {#if isMainEnabled(WidgetId.GlucoseChart)}
    <div {@attach coachmark({
      key: "dashboard-discovery.chart-timerange",
      title: "Time range",
      description: "Adjust the time window to see more or less glucose history.",
      completeOn: { event: "click" },
    })}>
    <GlucoseChartCard
      showPredictions={isMainEnabled(WidgetId.Predictions) && predictionEnabled}
      defaultFocusHours={focusHours}
      initialChartData={data.initialChartData}
      streamedHistoricalData={data.streamed?.historicalChartData}
    />
    </div>
  {/if}

  {#if isMainEnabled(WidgetId.DailyStats)}
    <RecentEntriesCard />
  {/if}

  {#if isMainEnabled(WidgetId.Treatments)}
    <RecentTreatmentsCard />
  {/if}
</div>
