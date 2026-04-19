<script lang="ts">
  import { Loader2 } from "lucide-svelte";

  let {
    selectedDevice,
  }: {
    selectedDevice: string;
  } = $props();

  let elapsed = $state(0);

  $effect(() => {
    const interval = setInterval(() => {
      elapsed += 1;
    }, 1000);
    return () => clearInterval(interval);
  });

  const deviceNames: Record<string, string> = {
    dexcom: "Dexcom G7 \u00b7 share",
    libre: "Libre 3 \u00b7 libreview",
    xdrip: "xDrip+ \u00b7 webhook",
    ns: "Nightscout \u00b7 bridge",
    aaps: "AAPS \u00b7 direct",
    none: "Pending",
  };

  function formatUptime(seconds: number): string {
    const h = String(Math.floor(seconds / 3600)).padStart(2, "0");
    const m = String(Math.floor((seconds % 3600) / 60)).padStart(2, "0");
    const s = String(seconds % 60).padStart(2, "0");
    return `${h}:${m}:${s}`;
  }
</script>

<div class="flex flex-col items-center gap-10 px-4 py-8">
  <!-- Heading -->
  <div class="flex flex-col items-center gap-4 text-center">
    <h1
      class="font-[Montserrat] font-[250] leading-tight tracking-tight text-white"
      style="font-size: clamp(32px, 4vw, 48px);"
    >
      Checking for your <em
        class="not-italic font-[300]"
        style="color: var(--onb-accent);"
        >first</em
      > reading&hellip;
    </h1>
    <p class="max-w-[560px] text-base leading-relaxed text-white/50">
      We're listening on your uploader endpoint. As soon as a reading arrives,
      we'll confirm the connection and send you to the dashboard.
    </p>
  </div>

  <!-- Two-column layout -->
  <div
    class="grid w-full max-w-[780px] grid-cols-[1.2fr_0.9fr] gap-7 max-sm:grid-cols-1"
  >
    <!-- Left column: Listener probe panel -->
    <div
      class="rounded-xl border border-white/[0.06] p-5"
      style="background: var(--onb-panel);"
    >
      <!-- Header -->
      <div class="mb-4 flex items-center justify-between">
        <span
          class="text-xs font-semibold uppercase tracking-wide text-white/40"
          >Listener</span
        >
        <span
          class="pulse-dot size-2 rounded-full"
          style="background: var(--onb-ok); box-shadow: 0 0 6px var(--onb-ok);"
        ></span>
      </div>

      <!-- Rows -->
      <div
        class="flex flex-col gap-2.5 font-mono text-[12.5px] text-white/40"
      >
        <div class="flex items-center justify-between">
          <span>Endpoint</span>
          <span class="text-white">https://nocturne.local/api/v1/entries</span>
        </div>
        <div class="flex items-center justify-between">
          <span>Auth</span>
          <span style="color: var(--onb-ok);">API secret issued</span>
        </div>
        <div class="flex items-center justify-between">
          <span>Source</span>
          <span class="text-white"
            >{deviceNames[selectedDevice] ?? "Pending"}</span
          >
        </div>
        <div class="flex items-center justify-between">
          <span>Uptime</span>
          <span class="text-white">{formatUptime(elapsed)}</span>
        </div>
      </div>

      <!-- Bottom row -->
      <div
        class="mt-4 flex items-center justify-between border-t border-white/[0.06] pt-4 font-mono text-[12.5px] text-white/40"
      >
        <span>Waiting for reading&hellip;</span>
        <Loader2 class="size-4 animate-spin" style="color: var(--onb-accent);" />
      </div>
    </div>

    <!-- Right column: Dry-run BG preview -->
    <div
      class="flex flex-col items-start gap-1 rounded-2xl border border-white/[0.06] p-[22px]"
      style="background: var(--onb-panel);"
    >
      <span class="text-[11px] uppercase tracking-wide text-white/40"
        >Dry run</span
      >
      <span
        class="font-[Montserrat] font-[200] text-[74px] leading-none tabular-nums"
        style="color: var(--onb-teal);"
        >142</span
      >
      <span class="text-sm text-white/40">mg/dl &middot; in range</span>

      <!-- Mini glucose curve -->
      <svg
        viewBox="0 0 260 80"
        class="mt-3 h-15 w-full"
        fill="none"
        xmlns="http://www.w3.org/2000/svg"
      >
        <path
          d="M0 52 C20 48, 35 30, 55 28 C75 26, 85 42, 105 40 C125 38, 135 22, 155 20 C175 18, 190 35, 210 32 C230 29, 245 24, 260 26"
          stroke="var(--onb-teal)"
          stroke-width="2"
          stroke-linecap="round"
        />
        <circle cx="260" cy="26" r="3.5" fill="var(--onb-teal)" />
      </svg>
    </div>
  </div>
</div>

<style>
  @keyframes pulse {
    0%,
    100% {
      opacity: 1;
      box-shadow: 0 0 6px oklch(0.72 0.17 150 / 0.6);
    }
    50% {
      opacity: 0.5;
      box-shadow: 0 0 12px oklch(0.72 0.17 150 / 0.3);
    }
  }

  .pulse-dot {
    animation: pulse 1.6s infinite;
  }
</style>
