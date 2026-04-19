<script lang="ts">
  import { Radio } from "lucide-svelte";

  let {
    selectedDevice,
    onSelect,
  }: {
    selectedDevice: string | null;
    onSelect: (deviceId: string) => void;
  } = $props();

  const devices = [
    { id: "dexcom", name: "Dexcom G6 / G7", sub: "share / bridge", mark: "D" },
    { id: "libre", name: "Libre 2 / 3", sub: "libreview \u00b7 juggluco", mark: "L" },
    { id: "xdrip", name: "xDrip+", sub: "webhook", mark: "x" },
    { id: "ns", name: "Nightscout", sub: "already connected", mark: "N" },
    { id: "aaps", name: "AAPS / Trio", sub: "direct upload", mark: "A" },
    { id: "none", name: "I\u2019ll set this up later", sub: "skip for now", mark: "\u2014" },
  ];
</script>

<div class="flex flex-col items-center gap-10 px-4 py-8">
  <!-- Heading -->
  <div class="flex flex-col items-center gap-4 text-center">
    <h1
      class="font-[Montserrat] font-[250] leading-tight tracking-tight text-white"
      style="font-size: clamp(32px, 4vw, 48px);"
    >
      Pick your <em class="not-italic font-[300]" style="color: var(--onb-accent);">first</em> data source.
    </h1>
    <p class="max-w-[560px] text-base leading-relaxed text-white/50">
      You can connect more later. If your rig uses an uploader already pointing
      at Nightscout, choose xDrip+ or Nightscout — that's all you need.
    </p>
  </div>

  <!-- Device grid -->
  <div class="grid w-full max-w-170 grid-cols-3 gap-3.5 max-sm:grid-cols-2">
    {#each devices as device}
      {@const selected = selectedDevice === device.id}
      <button
        class="group relative flex cursor-pointer flex-col gap-3 rounded-xl border p-4.5 text-left transition-all duration-200 {selected
          ? 'border-(--onb-accent) bg-(--onb-accent-dim)'
          : 'border-white/8 bg-white/3 hover:border-white/18'}"
        onclick={() => onSelect(device.id)}
      >
        <!-- Selector dot -->
        <span
          class="absolute right-3 top-3 block size-4.5 rounded-full border-2 transition-all duration-200 {selected
            ? 'border-(--onb-accent) bg-(--onb-accent) shadow-[inset_0_0_0_3px_rgb(10_10_14)]'
            : 'border-white/20'}"
        ></span>

        <!-- Brand mark -->
        <div
          class="flex h-8 w-8 items-center justify-center rounded-lg font-mono text-sm font-medium"
          style="background: {selected ? 'var(--onb-accent-dim)' : 'rgb(255 255 255 / 0.07)'}; color: {selected ? 'var(--onb-accent)' : 'rgb(255 255 255 / 0.5)'};"
        >
          {device.mark}
        </div>

        <!-- Name -->
        <span class="text-sm font-medium text-white">{device.name}</span>

        <!-- Subtitle -->
        <span class="font-mono text-[11.5px] tracking-wide text-white/40">
          {device.sub}
        </span>
      </button>
    {/each}
  </div>

  <!-- Callout -->
  <div
    class="flex w-full max-w-170 items-center gap-4 rounded-xl border border-white/8 px-5 py-4"
  >
    <div
      class="flex h-[38px] w-[38px] shrink-0 items-center justify-center rounded-lg"
      style="background: var(--onb-accent-dim);"
    >
      <Radio class="h-5 w-5" style="color: var(--onb-accent);" />
    </div>
    <div class="flex flex-col gap-0.5">
      <span class="text-[13.5px] font-medium text-white">Don't see your device?</span>
      <span class="text-xs text-white/50">
        Any Nightscout-compatible uploader works.
        <a
          href="/docs/uploaders"
          class="underline transition-colors"
          style="color: var(--onb-accent);"
        >
          Advanced setup &rarr;
        </a>
      </span>
    </div>
  </div>
</div>
