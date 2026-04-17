<script lang="ts">
  import * as Dialog from "$lib/components/ui/dialog";
  import { Button } from "$lib/components/ui/button";
  import { Loader2, Download, CheckCircle, AlertCircle } from "lucide-svelte";

  export interface BatchSyncResult {
    success: boolean;
    errorMessage?: string;
    totalConnectors: number;
    successfulConnectors: number;
    failedConnectors: number;
    startTime: Date;
    endTime: Date;
    connectorResults: {
      connectorName: string;
      success: boolean;
      errorMessage?: string;
      duration?: string;
    }[];
  }

  let {
    open = $bindable(false),
    isManualSyncing = false,
    manualSyncResult = null,
  } = $props<{
    open: boolean;
    isManualSyncing: boolean;
    manualSyncResult: BatchSyncResult | null;
  }>();
</script>

<Dialog.Root bind:open>
  <Dialog.Content class="max-w-2xl max-h-[80vh] overflow-y-auto">
    <Dialog.Header>
      <Dialog.Title class="flex items-center gap-2">
        <Download class="h-5 w-5" />
        Manual Sync Results
      </Dialog.Title>
      <Dialog.Description>
        Re-sync data from all enabled connectors for the configured lookback
        period
      </Dialog.Description>
    </Dialog.Header>

    <div class="space-y-4 py-4">
      {#if isManualSyncing}
        <div class="flex flex-col items-center justify-center py-8 gap-4">
          <Loader2 class="h-12 w-12 animate-spin text-primary" />
          <p class="text-sm text-muted-foreground">
            Syncing all enabled connectors...
          </p>
        </div>
      {:else if manualSyncResult}
        {#if manualSyncResult.success}
          <div class="rounded-lg border border-green-200 dark:border-green-800 bg-green-50 dark:bg-green-950/20 p-4">
            <div class="flex items-center gap-2 text-green-800 dark:text-green-200">
              <CheckCircle class="h-5 w-5" />
              <span class="font-medium">Sync completed successfully</span>
            </div>
            <p class="text-sm text-green-700 dark:text-green-300 mt-1">
              {manualSyncResult.successfulConnectors} of {manualSyncResult.totalConnectors}
              connectors synced in {Math.round(
                (new Date(manualSyncResult.endTime!).getTime() -
                  new Date(manualSyncResult.startTime!).getTime()) /
                  1000
              )}s
            </p>
          </div>
        {:else}
          <div class="rounded-lg border border-red-200 dark:border-red-800 bg-red-50 dark:bg-red-950/20 p-4">
            <div class="flex items-center gap-2 text-red-800 dark:text-red-200">
              <AlertCircle class="h-5 w-5" />
              <span class="font-medium">Sync failed</span>
            </div>
            <p class="text-sm text-red-700 dark:text-red-300 mt-1">
              {manualSyncResult.errorMessage}
            </p>
          </div>
        {/if}

        {#if manualSyncResult.connectorResults && manualSyncResult.connectorResults.length > 0}
          <div class="space-y-3">
            <h4 class="font-medium text-sm">Connector Results</h4>
            <div class="space-y-2">
              {#each manualSyncResult.connectorResults as result}
                <div class="flex items-center justify-between p-3 rounded-lg border {result.success ? 'border-green-200 dark:border-green-800 bg-green-50/50 dark:bg-green-950/10' : 'border-red-200 dark:border-red-800 bg-red-50/50 dark:bg-red-950/10'}">
                  <div class="flex items-center gap-3">
                    {#if result.success}
                      <CheckCircle class="h-4 w-4 text-green-600 dark:text-green-400" />
                    {:else}
                      <AlertCircle class="h-4 w-4 text-red-600 dark:text-red-400" />
                    {/if}
                    <div>
                      <p class="font-medium text-sm">{result.connectorName}</p>
                      {#if !result.success && result.errorMessage}
                        <p class="text-xs text-muted-foreground">
                          {result.errorMessage}
                        </p>
                      {/if}
                    </div>
                  </div>
                  <div class="text-right text-xs text-muted-foreground">
                    {#if result.duration}
                      {result.duration}
                    {/if}
                  </div>
                </div>
              {/each}
            </div>
          </div>
        {/if}
      {/if}
    </div>

    <Dialog.Footer>
      <Button variant="outline" onclick={() => (open = false)}>
        Close
      </Button>
    </Dialog.Footer>
  </Dialog.Content>
</Dialog.Root>