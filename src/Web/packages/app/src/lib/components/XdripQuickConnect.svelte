<script lang="ts">
  import { onMount } from "svelte";
  import QRCode from "qrcode";
  import { Button } from "$lib/components/ui/button";
  import { Smartphone } from "lucide-svelte";

  let { instanceUrl }: { instanceUrl: string } = $props();

  let qrDataUrl = $state<string | null>(null);
  let isAndroid = $state(false);

  const connectPageUrl = $derived(`${instanceUrl}/connect/xdrip`);
  const deepLink = $derived(
    `xdrip://connect/nocturne?url=${encodeURIComponent(instanceUrl)}`,
  );

  onMount(async () => {
    if (typeof navigator !== "undefined") {
      isAndroid = /android/i.test(navigator.userAgent);
    }
    qrDataUrl = await QRCode.toDataURL(connectPageUrl, {
      width: 200,
      margin: 2,
      color: { dark: "#000000", light: "#ffffff" },
    });
  });
</script>

<div class="space-y-4">
  <div>
    <p class="text-sm font-medium">Quick Connect</p>
    <p class="text-muted-foreground text-sm">
      Automatically configure xDrip+ with your Nocturne instance.
    </p>
  </div>

  {#if isAndroid}
    <Button href={deepLink} class="w-full">
      <Smartphone class="mr-2 h-4 w-4" />
      Open in xDrip+
    </Button>
    {#if qrDataUrl}
      <details class="text-sm">
        <summary class="text-muted-foreground cursor-pointer">
          Or scan from another device
        </summary>
        <div class="flex justify-center pt-3">
          <img src={qrDataUrl} alt="QR code to connect xDrip+" class="rounded" />
        </div>
      </details>
    {/if}
  {:else if qrDataUrl}
    <div class="flex justify-center">
      <img src={qrDataUrl} alt="QR code to connect xDrip+" class="rounded" />
    </div>
    <p class="text-muted-foreground text-center text-sm">
      Scan this QR code with your phone's camera
    </p>
    <details class="text-sm">
      <summary class="text-muted-foreground cursor-pointer">
        Or open this link on your phone
      </summary>
      <code class="bg-muted mt-2 block rounded px-3 py-2 text-xs break-all">
        {connectPageUrl}
      </code>
    </details>
  {/if}
</div>
