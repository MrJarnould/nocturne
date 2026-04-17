<script lang="ts">
  import { onMount } from "svelte";
  import {
    Card,
    CardContent,
    CardDescription,
    CardHeader,
    CardTitle,
  } from "$lib/components/ui/card";
  import { Button } from "$lib/components/ui/button";
  import { Badge } from "$lib/components/ui/badge";
  import * as AlertDialog from "$lib/components/ui/alert-dialog";
  import {
    Key,
    Loader2,
    CheckCircle,
    AlertCircle,
    RefreshCw,
    AlertTriangle,
    Copy,
    Check,
  } from "lucide-svelte";
  import { toast } from "svelte-sonner";
  import {
    getStatus as getApiSecretStatus,
    regenerate as regenerateApiSecretRemote,
  } from "$api/generated/apiSecrets.generated.remote";

  let apiSecretStatus = $state<{ hasSecret: boolean } | null>(null);
  let generatedSecret = $state<string | null>(null);
  let isRegeneratingSecret = $state(false);
  let showRegenerateDialog = $state(false);
  let copiedField = $state<string | null>(null);

  onMount(async () => {
    await loadApiSecretStatus();
  });

  async function loadApiSecretStatus() {
    try {
      const result = await getApiSecretStatus();
      if (result) {
        apiSecretStatus = { hasSecret: result.hasSecret ?? false };
      }
    } catch {
      // Non-admin or network error — hide the section
    }
  }

  async function regenerateApiSecret() {
    isRegeneratingSecret = true;
    generatedSecret = null;
    try {
      const result = await regenerateApiSecretRemote();
      generatedSecret = result.apiSecret ?? null;
      apiSecretStatus = { hasSecret: true };
      toast.success("API secret regenerated successfully");
    } catch (e) {
      toast.error(e instanceof Error ? e.message : "Failed to regenerate API secret");
    } finally {
      isRegeneratingSecret = false;
      showRegenerateDialog = false;
    }
  }

  async function copyToClipboard(text: string, field: string) {
    await navigator.clipboard.writeText(text);
    copiedField = field;
    setTimeout(() => {
      copiedField = null;
    }, 2000);
  }
</script>

<Card>
  <CardHeader>
    <CardTitle class="flex items-center gap-2">
      <Key class="h-5 w-5" />
      API Secret
    </CardTitle>
    <CardDescription>
      Manage the API secret used by Nightscout-compatible uploaders and CGM apps
    </CardDescription>
  </CardHeader>
  <CardContent class="space-y-4">
    {#if apiSecretStatus === null}
      <div class="flex items-center gap-2 text-sm text-muted-foreground">
        <Loader2 class="h-4 w-4 animate-spin" />
        Checking status...
      </div>
    {:else}
      <div class="flex items-center gap-3">
        {#if apiSecretStatus.hasSecret}
          <Badge variant="outline" class="gap-1.5">
            <CheckCircle class="h-3.5 w-3.5 text-green-500" />
            Configured
          </Badge>
        {:else}
          <Badge variant="destructive" class="gap-1.5">
            <AlertCircle class="h-3.5 w-3.5" />
            Not configured
          </Badge>
        {/if}
        <Button
          variant="outline"
          size="sm"
          disabled={isRegeneratingSecret}
          onclick={() => (showRegenerateDialog = true)}
        >
          {#if isRegeneratingSecret}
            <Loader2 class="h-4 w-4 animate-spin mr-2" />
          {:else}
            <RefreshCw class="h-4 w-4 mr-2" />
          {/if}
          {apiSecretStatus.hasSecret ? "Regenerate" : "Generate"}
        </Button>
      </div>
      <p class="text-sm text-muted-foreground">
        {#if apiSecretStatus.hasSecret}
          Your API secret is configured. Regenerating it will invalidate the current secret
          and disconnect any uploaders using it.
        {:else}
          No API secret is configured. Generate one to allow Nightscout-compatible
          uploaders to send data to your site.
        {/if}
      </p>
    {/if}

    {#if generatedSecret}
      <div class="rounded-lg border border-amber-500/50 bg-amber-50 dark:bg-amber-950/20 p-4 space-y-3">
        <div class="flex items-start gap-2">
          <AlertTriangle class="h-5 w-5 text-amber-500 shrink-0 mt-0.5" />
          <div class="space-y-1">
            <p class="text-sm font-medium">Save your new API secret now</p>
            <p class="text-sm text-muted-foreground">
              This is the only time it will be shown. Copy it and configure it in your uploader app.
            </p>
          </div>
        </div>
        <div class="flex gap-2">
          <code class="flex-1 px-3 py-2 rounded-md bg-muted text-sm font-mono break-all select-all">
            {generatedSecret}
          </code>
          <Button
            variant="outline"
            size="icon"
            onclick={() => copyToClipboard(generatedSecret!, "apiSecret")}
          >
            {#if copiedField === "apiSecret"}
              <Check class="h-4 w-4 text-green-500" />
            {:else}
              <Copy class="h-4 w-4" />
            {/if}
          </Button>
        </div>
      </div>
    {/if}
  </CardContent>
</Card>

<AlertDialog.Root bind:open={showRegenerateDialog}>
  <AlertDialog.Content>
    <AlertDialog.Header>
      <AlertDialog.Title>
        {apiSecretStatus?.hasSecret ? "Regenerate API Secret?" : "Generate API Secret?"}
      </AlertDialog.Title>
      <AlertDialog.Description>
        {#if apiSecretStatus?.hasSecret}
          This will replace your current API secret. Any uploaders or apps using the
          current secret will stop working until reconfigured with the new secret.
        {:else}
          This will generate a new API secret that you can use to authenticate
          Nightscout-compatible uploaders and CGM apps.
        {/if}
      </AlertDialog.Description>
    </AlertDialog.Header>
    <AlertDialog.Footer>
      <AlertDialog.Cancel>Cancel</AlertDialog.Cancel>
      <AlertDialog.Action onclick={regenerateApiSecret}>
        {apiSecretStatus?.hasSecret ? "Regenerate" : "Generate"}
      </AlertDialog.Action>
    </AlertDialog.Footer>
  </AlertDialog.Content>
</AlertDialog.Root>
