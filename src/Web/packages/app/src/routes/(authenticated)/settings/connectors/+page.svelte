<script lang="ts">
  import { onMount, onDestroy } from "svelte";
  import { getAllConnectorStatus } from "$api/generated/configurations.generated.remote";
  import {
    getServicesOverview,
    getConnectorCapabilities,
    deleteDataSourceData as deleteDataSourceDataRemote,
  } from "$api/generated/services.generated.remote";
  import { getDataTypeLabel } from "$lib/utils/data-type-labels";
  import type {
    ServicesOverview,
    UploaderApp,
    UploaderSetupResponse,
    DataSourceInfo,
    ConnectorStatusInfo,
    SyncRequest,
    SyncResult,
    AvailableConnector,
    ConnectorCapabilities,
  } from "$lib/api/generated/nocturne-api-client";
  import { DataSourceCategory } from "$api-clients";

  // Extended type that includes description and runtime fields for UI display.
  // ConnectorStatusInfo has the configuration status (connectorName, isEnabled, etc.).
  // The runtime fields (state, isHealthy, entries, etc.) are optional since they
  // are no longer returned by the API directly.
  interface ConnectorStatusWithDescription extends ConnectorStatusInfo {
    /** Connector id used for sync operations (matches AvailableConnector.id) */
    id?: string;
    /** Display name */
    name?: string;
    /** Human-readable description */
    description?: string;
    /** Runtime state: Idle, Syncing, BackingOff, Error, Configured, Disabled, Offline */
    state?: string;
    /** Optional message describing the current state */
    stateMessage?: string;
    /** Whether the connector is currently healthy */
    isHealthy?: boolean;
    /** Human-readable status string */
    status?: string;
    /** Total number of entries synced by this connector */
    totalEntries?: number;
    /** Number of entries synced in the last 24 hours */
    entriesLast24Hours?: number;
    /** Timestamp of the last entry received */
    lastEntryTime?: Date;
    /** Timestamp of the last sync attempt */
    lastSyncAttempt?: Date;
    /** Timestamp of the last successful sync */
    lastSuccessfulSync?: Date;
    /** Breakdown of total items by data type */
    totalItemsBreakdown?: { [key: string]: number };
    /** Breakdown of items in the last 24h by data type */
    itemsLast24HoursBreakdown?: { [key: string]: number };
  }
  import {
    Card,
    CardContent,
    CardDescription,
    CardHeader,
    CardTitle,
  } from "$lib/components/ui/card";
  import { Button } from "$lib/components/ui/button";
  import { Badge } from "$lib/components/ui/badge";
  import { Separator } from "$lib/components/ui/separator";
  import * as Dialog from "$lib/components/ui/dialog";
  import * as Tabs from "$lib/components/ui/tabs";
  import * as AlertDialog from "$lib/components/ui/alert-dialog";
  import * as Tooltip from "$lib/components/ui/tooltip";

  import {
    RefreshCw,
    CheckCircle,
    AlertCircle,
    Clock,
    ExternalLink,
    Smartphone,
    Cloud,
    Database,
    Loader2,
    Copy,
    Check,
    Wifi,
    WifiOff,
    ChevronRight,
    Trash2,
    AlertTriangle,
    Sparkles,
    Pencil,
    Download,
    Link2,
    Wrench,
    Key,
    MessageSquare,
  } from "lucide-svelte";
  import SettingsPageSkeleton from "$lib/components/settings/SettingsPageSkeleton.svelte";
  import DataSourceRow from "$lib/components/settings/DataSourceRow.svelte";
  import type { DataSourceStatus } from "$lib/components/settings/DataSourceRow.svelte";
  import ConnectedApps from "$lib/components/settings/ConnectedApps.svelte";
  import ApiTokens from "$lib/components/settings/ApiTokens.svelte";
  import XdripQuickConnect from "$lib/components/XdripQuickConnect.svelte";
  import DeduplicationDialog from "$lib/components/connectors/DeduplicationDialog.svelte";
  import ApiSecretSection from "$lib/components/connectors/ApiSecretSection.svelte";
  import UploaderSetupDialog from "$lib/components/connectors/UploaderSetupDialog.svelte";
  import ConnectorDetailsDialog from "$lib/components/connectors/ConnectorDetailsDialog.svelte";
  import ManualSyncDialog, { type BatchSyncResult } from "$lib/components/connectors/ManualSyncDialog.svelte";
  import DemoDataSection from "$lib/components/connectors/DemoDataSection.svelte";
  import Apple from "lucide-svelte/icons/apple";
  import TabletSmartphone from "lucide-svelte/icons/tablet-smartphone";
  import { getApiClient } from "$lib/api";
  import { toast } from "svelte-sonner";
  import { getCategoryIcon, mapConnectorStatus } from "$lib/utils/connector-display";
  import { getRealtimeStore } from "$lib/stores/realtime-store.svelte";

  let servicesOverview = $state<ServicesOverview | null>(null);
  let isLoading = $state(true);
  let error = $state<string | null>(null);
  let selectedUploader = $state<UploaderApp | null>(null);
  let showSetupDialog = $state(false);
  let copiedField = $state<string | null>(null);

  // Demo data dialog state

  let showDemoDataDialog = $state(false);

  // Data source management dialog state
  let selectedDataSource = $state<DataSourceInfo | null>(null);
  let showManageDataSourceDialog = $state(false);
  let showDeleteConfirmDialog = $state(false);
  let isDeletingDataSource = $state(false);
  let deleteConfirmText = $state("");
  let deleteResult = $state<{
    success?: boolean;
    totalDeleted?: number;
    error?: string;
  } | null>(null);

  // Manual sync state

  let isManualSyncing = $state(false);
  let showManualSyncDialog = $state(false);
  let manualSyncResult = $state<BatchSyncResult | null>(null);

  // Connector heartbeat metrics state
  let connectorStatuses = $state<ConnectorStatusInfo[]>([]);
  let isLoadingConnectorStatuses = $state(false);
  let selectedConnector = $state<ConnectorStatusWithDescription | null>(null);
  let selectedConnectorCapabilities = $state<ConnectorCapabilities | null>(
    null
  );
  let connectorCapabilitiesById = $state<
    Record<string, ConnectorCapabilities | null>
  >({});
  let quickSyncingById = $state<Record<string, boolean>>({});
  let showConnectorDialog = $state(false);

  // Realtime sync progress from WebSocket
  const realtimeStore = getRealtimeStore();
  let syncProgressByConnector = $derived(realtimeStore.syncProgressByConnector);

  $effect(() => {
    const progress = syncProgressByConnector;
    const hasCompleted = Object.values(progress).some(
      (p) => p.phase === "Completed" || p.phase === "Failed"
    );
    if (hasCompleted) {
      loadConnectorStatuses();
    }
  });

  // Deduplication state
  let showDeduplicationDialog = $state(false);
  let isDeduplicating = $state(false);

  onMount(async () => {
    await Promise.all([loadServices(), loadConnectorStatuses()]);
  });

  onDestroy(() => {
  });

  async function refreshAll() {
    await Promise.all([loadServices(), loadConnectorStatuses()]);
  }

  async function loadServices() {
    isLoading = true;
    error = null;
    try {
      servicesOverview = await getServicesOverview();
      if (servicesOverview?.availableConnectors) {
        await loadConnectorCapabilitiesMap(servicesOverview.availableConnectors);
      } else {
        connectorCapabilitiesById = {};
      }
    } catch (e) {
      error = e instanceof Error ? e.message : "Failed to load services";
    } finally {
      isLoading = false;
    }
  }

  async function loadConnectorStatuses() {
    isLoadingConnectorStatuses = true;
    try {
      connectorStatuses = await getAllConnectorStatus();
    } catch (e) {
      console.error("Failed to load connector statuses", e);
      connectorStatuses = [];
    } finally {
      isLoadingConnectorStatuses = false;
    }
  }

  async function loadConnectorCapabilitiesFor(connectorId?: string) {
    if (!connectorId) {
      selectedConnectorCapabilities = null;
      return;
    }

    try {
      selectedConnectorCapabilities = await getConnectorCapabilities(connectorId);
    } catch (e) {
      console.error("Failed to load connector capabilities", e);
      selectedConnectorCapabilities = null;
    }
  }

  async function loadConnectorCapabilitiesMap(
    connectors: AvailableConnector[]
  ) {
    const connectorIds = connectors
      .map((connector) => connector.id)
      .filter((id): id is string => !!id);

    if (connectorIds.length === 0) {
      connectorCapabilitiesById = {};
      return;
    }

    try {
      const results = await Promise.all(
        connectorIds.map(async (connectorId) => ({
          connectorId,
          capabilities: await getConnectorCapabilities(connectorId),
        }))
      );

      connectorCapabilitiesById = results.reduce(
        (acc, result) => {
          acc[result.connectorId] = result.capabilities;
          return acc;
        },
        {} as Record<string, ConnectorCapabilities | null>
      );
    } catch (e) {
      console.error("Failed to load connector capabilities map", e);
      connectorCapabilitiesById = {};
    } finally {
    }
  }

  function openUploaderSetup(uploader: UploaderApp) {
    selectedUploader = uploader;
    showSetupDialog = true;
  }

  async function deleteDataSource() {
    if (!selectedDataSource) return;

    isDeletingDataSource = true;
    deleteResult = null;
    try {
      const result = await deleteDataSourceDataRemote(selectedDataSource.id!);
      deleteResult = {
        success: result.success ?? false,
        totalDeleted: result.totalDeleted,
        error: result.error ?? undefined,
      };
      if (result.success) {
        showDeleteConfirmDialog = false;
        await loadServices();
      }
    } catch (e) {
      deleteResult = {
        success: false,
        error: e instanceof Error ? e.message : "Failed to delete data",
      };
    } finally {
      isDeletingDataSource = false;
    }
  }

  async function triggerManualSync() {
    isManualSyncing = true;
    manualSyncResult = null;
    showManualSyncDialog = true;

    const startTime = new Date();
    // Sync all enabled connectors
    const connectorsToSync = connectorStatuses.filter((c) => c.isEnabled !== false);
    const results: BatchSyncResult["connectorResults"] = [];
    let successes = 0;

    // Default lookback 30 days
    const to = new Date();
    const from = new Date(to.getTime() - 30 * 24 * 60 * 60 * 1000);
    const request: SyncRequest = {
      from: from,
      to: to,
    };

    try {
      const apiClient = getApiClient();

      // Execute sequentially to avoid overwhelming sidecars/backend
      for (const connector of connectorsToSync) {
        const connectorId = connector.connectorName;
        if (!connectorId) continue;

        const start = performance.now();
        let success = false;
        let errorMsg = undefined;

        try {
          const result = await apiClient.services.triggerConnectorSync(
            connectorId,
            request
          );
          success = result.success ?? false;
          if (!success) errorMsg = result.message || "Unknown error";
        } catch (e) {
          success = false;
          errorMsg = e instanceof Error ? e.message : "Request failed";
        }

        const durationMs = performance.now() - start;
        results.push({
          connectorName: connectorId,
          success,
          errorMessage: errorMsg,
          duration: `${Math.round(durationMs)}ms`,
        });

        if (success) successes++;
      }

      const endTime = new Date();
      manualSyncResult = {
        success: successes > 0, // Consider partial success as success for the batch
        totalConnectors: connectorsToSync.length,
        successfulConnectors: successes,
        failedConnectors: connectorsToSync.length - successes,
        startTime,
        endTime,
        connectorResults: results,
      };

      if (successes > 0) {
        await Promise.all([loadServices(), loadConnectorStatuses()]);
      }
    } catch (e) {
      manualSyncResult = {
        success: false,
        errorMessage:
          e instanceof Error ? e.message : "Failed to trigger manual sync",
        totalConnectors: 0,
        successfulConnectors: 0,
        failedConnectors: 0,
        startTime: new Date(),
        endTime: new Date(),
        connectorResults: [],
      };
    } finally {
      isManualSyncing = false;
    }
  }

  async function triggerQuickSync(connectorId: string) {
    if (quickSyncingById[connectorId]) return;

    quickSyncingById = { ...quickSyncingById, [connectorId]: true };
    try {
      const apiClient = getApiClient();
      const result = await apiClient.services.triggerConnectorSync(
        connectorId,
        {}
      );

      if (result.success) {
        toast.success("Sync started");
      } else {
        toast.error(result.message || "Sync failed");
      }

      await loadConnectorStatuses();
    } catch (e) {
      toast.error(e instanceof Error ? e.message : "Sync failed");
    } finally {
      quickSyncingById = { ...quickSyncingById, [connectorId]: false };
    }
  }

  // Check if a connector has data in the database (from activeDataSources)
  function getConnectorDataSource(
    connector: AvailableConnector
  ): DataSourceInfo | null {
    if (!servicesOverview?.activeDataSources) return null;
    // Need either dataSourceId or id to match against
    if (!connector.dataSourceId && !connector.id) return null;
    return (
      servicesOverview.activeDataSources.find((source) => {
        // Match by dataSourceId (e.g., "dexcom-connector")
        if (connector.dataSourceId) {
          if (
            source.deviceId === connector.dataSourceId ||
            source.sourceType === connector.dataSourceId
          ) {
            return true;
          }
        }
        // Also match by connector id as fallback (e.g., "dexcom")
        // This handles cases where sourceType was set by device name detection
        if (connector.id) {
          if (source.sourceType === connector.id) {
            return true;
          }
        }
        return false;
      }) ?? null
    );
  }

  // Check if a data source matches an uploader or connector
  function getMatchingUploader(source: DataSourceInfo): UploaderApp | null {
    if (!servicesOverview?.uploaderApps) return null;

    const sourceLower = (source.sourceType ?? source.name ?? "").toLowerCase();
    const deviceLower = (source.deviceId ?? "").toLowerCase();

    for (const uploader of servicesOverview.uploaderApps) {
      const uploaderIdLower = (uploader.id ?? "").toLowerCase();

      // Match by source type
      if (sourceLower === uploaderIdLower) return uploader;

      // Match xDrip+ variations
      if (uploaderIdLower === "xdrip") {
        if (sourceLower.includes("xdrip") || deviceLower.includes("xdrip")) {
          return uploader;
        }
      }

      // Match Loop
      if (uploaderIdLower === "loop") {
        if (
          (sourceLower === "loop" || deviceLower.includes("loop")) &&
          !sourceLower.includes("openaps")
        ) {
          return uploader;
        }
      }

      // Match AAPS/AndroidAPS
      if (uploaderIdLower === "aaps") {
        if (
          sourceLower.includes("aaps") ||
          sourceLower.includes("androidaps") ||
          deviceLower.includes("aaps") ||
          deviceLower.includes("androidaps")
        ) {
          return uploader;
        }
      }

      // Match Trio
      if (uploaderIdLower === "trio") {
        if (sourceLower === "trio" || deviceLower.includes("trio")) {
          return uploader;
        }
      }

      // Match iAPS
      if (uploaderIdLower === "iaps") {
        if (sourceLower === "iaps" || deviceLower.includes("iaps")) {
          return uploader;
        }
      }

      // Match Spike
      if (uploaderIdLower === "spike") {
        if (sourceLower.includes("spike") || deviceLower.includes("spike")) {
          return uploader;
        }
      }
    }

    return null;
  }

  function isUploaderActive(uploader: UploaderApp): boolean {
    if (!servicesOverview?.activeDataSources) return false;

    for (const source of servicesOverview.activeDataSources) {
      const matchingUploader = getMatchingUploader(source);
      if (matchingUploader?.id === uploader.id) {
        return true;
      }
    }

    return false;
  }

  function getStatusBadge(status: string | undefined): {
    variant: "default" | "secondary" | "destructive" | "outline";
    text: string;
    class: string;
  } {
    switch (status) {
      case "active":
        return {
          variant: "default" as const,
          text: "Active",
          class:
            "bg-green-100 text-green-800 dark:bg-green-900 dark:text-green-100",
        };
      case "stale":
        return {
          variant: "secondary" as const,
          text: "Stale",
          class:
            "bg-yellow-100 text-yellow-800 dark:bg-yellow-900 dark:text-yellow-100",
        };
      case "inactive":
        return {
          variant: "outline" as const,
          text: "Inactive",
          class: "",
        };
      default:
        return {
          variant: "secondary" as const,
          text: "Unknown",
          class: "",
        };
    }
  }

  function formatLastSeen(date?: Date): string {
    if (!date) return "Never";
    const d = new Date(date);
    const diff = Date.now() - d.getTime();
    const minutes = Math.floor(diff / 60000);
    if (minutes < 1) return "Just now";
    if (minutes < 60) return `${minutes}m ago`;
    const hours = Math.floor(minutes / 60);
    if (hours < 24) return `${hours}h ago`;
    const days = Math.floor(hours / 24);
    if (days < 7) return `${days}d ago`;
    return d.toLocaleDateString();
  }

  function getPlatformIcon(platform: string | undefined) {
    switch (platform) {
      case "ios":
        return Apple;
      case "android":
        return TabletSmartphone;
      default:
        return Smartphone;
    }
  }

  function mapDataSourceStatus(source: DataSourceInfo): DataSourceStatus {
    if (isDemoDataSource(source)) return "demo";
    switch (source.status) {
      case "active":
        return "active";
      case "stale":
        return "stale";
      default:
        return "inactive";
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

<svelte:head>
  <title>Connectors & Apps - Settings - Nocturne</title>
</svelte:head>

<div class="container mx-auto max-w-4xl p-6 space-y-6">
  <!-- Header -->
  <div class="flex items-center justify-between">
    <div>
      <h1 class="text-2xl font-bold tracking-tight">Connectors & Connected Apps</h1>
      <p class="text-muted-foreground">
        Manage data sources, set up new connections, and control app access
      </p>
    </div>
    <Button variant="outline" size="sm" onclick={refreshAll} class="gap-2">
      <RefreshCw
        class="h-4 w-4 {isLoading || isLoadingConnectorStatuses
          ? 'animate-spin'
          : ''}"
      />
      Refresh
    </Button>
  </div>

  {#if isLoading && !servicesOverview}
    <SettingsPageSkeleton cardCount={3} />
  {:else if error}
    <Card class="border-destructive">
      <CardContent class="py-8">
        <div class="text-center">
          <AlertCircle class="h-12 w-12 mx-auto mb-4 text-destructive" />
          <p class="font-medium">Failed to load services</p>
          <p class="text-sm text-muted-foreground mt-1">{error}</p>
          <Button class="mt-4" onclick={loadServices}>Try Again</Button>
        </div>
      </CardContent>
    </Card>
  {:else if servicesOverview}
    <!-- Active Data Sources -->
    <Card>
      <CardHeader>
        <CardTitle class="flex items-center gap-2">
          <Wifi class="h-5 w-5" />
          Active Data Sources
        </CardTitle>
        <CardDescription>
          Devices and apps currently sending data to this Nocturne instance
        </CardDescription>
      </CardHeader>
      <CardContent>
        {#if !servicesOverview.activeDataSources || servicesOverview.activeDataSources.length === 0}
          <div class="text-center py-8 text-muted-foreground">
            <WifiOff class="h-12 w-12 mx-auto mb-4 opacity-50" />
            <p class="font-medium">No data sources detected</p>
            <p class="text-sm">
              Set up an uploader app to start sending data to Nocturne
            </p>
          </div>
        {:else}
          <div class="space-y-3">
            {#each servicesOverview.activeDataSources as source}
              {@const matchingUploader = getMatchingUploader(source)}
              {@const isDemo = isDemoDataSource(source)}
              <DataSourceRow
                name={source.name ?? "Unknown"}
                icon={getCategoryIcon(source.category)}
                status={mapDataSourceStatus(source)}
                totalEntries={source.totalEntries}
                entriesLast24h={source.entriesLast24h}
                lastSeen={source.lastSeen}
                onclick={() => openDataSourceDialog(source)}
              >
                {#snippet badges()}
                  {#if isDemo}
                    <Badge
                      variant="secondary"
                      class="bg-purple-100 text-purple-800 dark:bg-purple-900 dark:text-purple-100 text-xs"
                    >
                      <Sparkles class="h-3 w-3 mr-1" />
                      Demo
                    </Badge>
                  {/if}
                  {#if matchingUploader}
                    <Badge variant="outline" class="text-xs">
                      {matchingUploader.name}
                    </Badge>
                  {/if}
                {/snippet}
              </DataSourceRow>
            {/each}
          </div>
        {/if}
      </CardContent>
    </Card>

    <!-- Uploader Apps -->
    <Card>
      <CardHeader>
        <CardTitle class="flex items-center gap-2">
          <Smartphone class="h-5 w-5" />
          Set Up an Uploader
        </CardTitle>
        <CardDescription>
          Connect your CGM app or AID system to push data to Nocturne
        </CardDescription>
      </CardHeader>
      <CardContent>
        <Tabs.Root value="cgm">
          <Tabs.List class="grid w-full grid-cols-3">
            <Tabs.Trigger value="cgm">CGM Apps</Tabs.Trigger>
            <Tabs.Trigger value="aid">AID Systems</Tabs.Trigger>
            <Tabs.Trigger value="other">Other</Tabs.Trigger>
          </Tabs.List>

          <Tabs.Content value="cgm" class="mt-4">
            <div class="grid gap-3 sm:grid-cols-2">
              {#each (servicesOverview.uploaderApps ?? []).filter((u: UploaderApp) => u.category === "cgm") as uploader}
                {@const PlatformIcon = getPlatformIcon(uploader.platform)}
                {@const active = isUploaderActive(uploader)}
                <button
                  class="flex items-center gap-4 p-4 rounded-lg border hover:border-primary/50 hover:bg-accent/50 transition-colors text-left group {active
                    ? 'border-green-300 dark:border-green-700 bg-green-50/50 dark:bg-green-950/20'
                    : ''}"
                  onclick={() => openUploaderSetup(uploader)}
                >
                  <div
                    class="flex h-10 w-10 shrink-0 items-center justify-center rounded-lg {active
                      ? 'bg-green-100 dark:bg-green-900/30'
                      : 'bg-primary/10'}"
                  >
                    <PlatformIcon
                      class="h-5 w-5 {active
                        ? 'text-green-600 dark:text-green-400'
                        : 'text-primary'}"
                    />
                  </div>
                  <div class="flex-1 min-w-0">
                    <div class="flex items-center gap-2 flex-wrap">
                      <span class="font-medium">{uploader.name}</span>
                      <Badge variant="outline" class="text-xs capitalize">
                        {uploader.platform}
                      </Badge>
                      {#if active}
                        <Badge
                          class="bg-green-100 text-green-800 dark:bg-green-900 dark:text-green-100 text-xs"
                        >
                          <CheckCircle class="h-3 w-3 mr-1" />
                          Active
                        </Badge>
                      {/if}
                    </div>
                    <p class="text-sm text-muted-foreground truncate">
                      {uploader.description}
                    </p>
                  </div>
                  <ChevronRight
                    class="h-4 w-4 text-muted-foreground group-hover:text-foreground transition-colors"
                  />
                </button>
              {/each}
            </div>
          </Tabs.Content>

          <Tabs.Content value="aid" class="mt-4">
            <div class="grid gap-3 sm:grid-cols-2">
              {#each (servicesOverview.uploaderApps ?? []).filter((u: UploaderApp) => u.category === "aid-system") as uploader}
                {@const PlatformIcon = getPlatformIcon(uploader.platform)}
                {@const active = isUploaderActive(uploader)}
                <button
                  class="flex items-center gap-4 p-4 rounded-lg border hover:border-primary/50 hover:bg-accent/50 transition-colors text-left group {active
                    ? 'border-green-300 dark:border-green-700 bg-green-50/50 dark:bg-green-950/20'
                    : ''}"
                  onclick={() => openUploaderSetup(uploader)}
                >
                  <div
                    class="flex h-10 w-10 shrink-0 items-center justify-center rounded-lg {active
                      ? 'bg-green-100 dark:bg-green-900/30'
                      : 'bg-primary/10'}"
                  >
                    <PlatformIcon
                      class="h-5 w-5 {active
                        ? 'text-green-600 dark:text-green-400'
                        : 'text-primary'}"
                    />
                  </div>
                  <div class="flex-1 min-w-0">
                    <div class="flex items-center gap-2 flex-wrap">
                      <span class="font-medium">{uploader.name}</span>
                      <Badge variant="outline" class="text-xs capitalize">
                        {uploader.platform}
                      </Badge>
                      {#if active}
                        <Badge
                          class="bg-green-100 text-green-800 dark:bg-green-900 dark:text-green-100 text-xs"
                        >
                          <CheckCircle class="h-3 w-3 mr-1" />
                          Active
                        </Badge>
                      {/if}
                    </div>
                    <p class="text-sm text-muted-foreground truncate">
                      {uploader.description}
                    </p>
                  </div>
                  <ChevronRight
                    class="h-4 w-4 text-muted-foreground group-hover:text-foreground transition-colors"
                  />
                </button>
              {/each}
            </div>
          </Tabs.Content>

          <Tabs.Content value="other" class="mt-4">
            <div class="grid gap-3 sm:grid-cols-2">
              {#each (servicesOverview.uploaderApps ?? []).filter((u: UploaderApp) => u.category !== "cgm" && u.category !== "aid-system") as uploader}
                {@const PlatformIcon = getPlatformIcon(uploader.platform)}
                {@const active = isUploaderActive(uploader)}
                <button
                  class="flex items-center gap-4 p-4 rounded-lg border hover:border-primary/50 hover:bg-accent/50 transition-colors text-left group {active
                    ? 'border-green-300 dark:border-green-700 bg-green-50/50 dark:bg-green-950/20'
                    : ''}"
                  onclick={() => openUploaderSetup(uploader)}
                >
                  <div
                    class="flex h-10 w-10 shrink-0 items-center justify-center rounded-lg {active
                      ? 'bg-green-100 dark:bg-green-900/30'
                      : 'bg-primary/10'}"
                  >
                    <PlatformIcon
                      class="h-5 w-5 {active
                        ? 'text-green-600 dark:text-green-400'
                        : 'text-primary'}"
                    />
                  </div>
                  <div class="flex-1 min-w-0">
                    <div class="flex items-center gap-2 flex-wrap">
                      <span class="font-medium">{uploader.name}</span>
                      <Badge variant="outline" class="text-xs capitalize">
                        {uploader.platform}
                      </Badge>
                      {#if active}
                        <Badge
                          class="bg-green-100 text-green-800 dark:bg-green-900 dark:text-green-100 text-xs"
                        >
                          <CheckCircle class="h-3 w-3 mr-1" />
                          Active
                        </Badge>
                      {/if}
                    </div>
                    <p class="text-sm text-muted-foreground truncate">
                      {uploader.description}
                    </p>
                  </div>
                  <ChevronRight
                    class="h-4 w-4 text-muted-foreground group-hover:text-foreground transition-colors"
                  />
                </button>
              {/each}
            </div>
          </Tabs.Content>
        </Tabs.Root>
      </CardContent>
    </Card>

    <!-- Server-Side Connectors -->
    <Card>
      <CardHeader>
        <div class="flex items-center justify-between">
          <div>
            <CardTitle class="flex items-center gap-2">
              <Cloud class="h-5 w-5" />
              Server Connectors
            </CardTitle>
            <CardDescription>
              Connectors that run on the server to pull data from cloud services
            </CardDescription>
          </div>
          <div class="flex gap-2">
            {#if connectorStatuses.length > 0}
              <Button
                variant="outline"
                size="sm"
                onclick={loadConnectorStatuses}
                disabled={isLoadingConnectorStatuses}
                class="gap-2"
              >
                <RefreshCw
                  class="h-4 w-4 {isLoadingConnectorStatuses
                    ? 'animate-spin'
                    : ''}"
                />
                Refresh
              </Button>
            {/if}
            <!-- {#if servicesOverview.manualSyncEnabled} -->
            <Button
              variant="outline"
              size="sm"
              onclick={triggerManualSync}
              disabled={isManualSyncing}
              class="gap-2"
            >
              {#if isManualSyncing}
                <Loader2 class="h-4 w-4 animate-spin" />
                Syncing...
              {:else}
                <Download class="h-4 w-4" />
                Manual Sync
              {/if}
            </Button>
            <!-- {/if} -->
          </div>
        </div>
      </CardHeader>
      <CardContent>
        <div class="grid gap-3 sm:grid-cols-2">
          {#each servicesOverview.availableConnectors ?? [] as connector}
            {@const connectorStatusInfo = connectorStatuses.find(
              (cs) => cs.connectorName === connector.id
            )}
            {@const isConnected = connectorStatusInfo?.isEnabled === true && connectorStatusInfo?.hasDatabaseConfig === true}
            {@const isDisabled = connectorStatusInfo?.isEnabled === false && connectorStatusInfo?.hasDatabaseConfig === true}
            {@const connectorDataSource = getConnectorDataSource(connector)}
            {@const hasData =
              connectorDataSource !== null || isDisabled}
            {@const connectorCapabilities = connector.id
              ? connectorCapabilitiesById[connector.id]
              : null}
            {@const canQuickSync =
              isConnected &&
              (connectorCapabilities?.supportsManualSync ?? true)}

            {#if isConnected && connectorStatusInfo}
              <!-- Connected connector -->
              {@const connectorStatus: ConnectorStatusWithDescription = {
                ...connectorStatusInfo,
                id: connector.id,
                name: connector.name ?? connector.id,
                description: connector.description,
                state: "Configured",
              }}
              <DataSourceRow
                name={connector.name ?? connector.id ?? "Unknown"}
                icon={getCategoryIcon(connector.category)}
                status={syncProgressByConnector[connector.id ?? ""]?.phase === "Syncing" ? "syncing" : mapConnectorStatus(connectorStatus)}
                syncProgress={syncProgressByConnector[connector.id ?? ""] ?? null}
                totalEntries={connectorStatus.totalEntries}
                entriesLast24h={connectorStatus.entriesLast24Hours}
                lastSeen={connectorStatus.lastEntryTime}
                lastSyncAttempt={connectorStatus.lastSyncAttempt}
                lastSuccessfulSync={connectorStatus.lastSuccessfulSync}
                totalBreakdown={connectorStatus.totalItemsBreakdown ?? undefined}
                last24hBreakdown={connectorStatus.itemsLast24HoursBreakdown ?? undefined}
                onclick={async () => {
                  selectedConnector = connectorStatus;

                  await loadConnectorCapabilitiesFor(connector.id);
                  showConnectorDialog = true;
                }}
              >
                {#snippet actions()}
                  {#if connector.id && canQuickSync}
                    <Button
                      variant="outline"
                      size="icon"
                      class="absolute right-3 top-1/2 -translate-y-1/2"
                      disabled={quickSyncingById[connector.id] === true}
                      onclick={(event) => {
                        event.stopPropagation();
                        triggerQuickSync(connector.id!);
                      }}
                    >
                      {#if quickSyncingById[connector.id] === true}
                        <Loader2 class="h-4 w-4 animate-spin" />
                      {:else}
                        <RefreshCw class="h-4 w-4" />
                      {/if}
                    </Button>
                  {/if}
                {/snippet}
              </DataSourceRow>
            {:else if hasData}
              <!-- Has data but not connected/disabled -->
              {@const entryCount = isDisabled
                ? 0
                : (connectorDataSource?.totalEntries ?? 0)}
              {@const entries24h = isDisabled
                ? 0
                : (connectorDataSource?.entriesLast24h ?? 0)}
              {@const lastSeenDate = isDisabled
                ? undefined
                : connectorDataSource?.lastSeen}
              <DataSourceRow
                name={connector.name ?? connector.id ?? "Unknown"}
                icon={getCategoryIcon(connector.category)}
                status={isDisabled ? "disabled" : "offline"}
                syncProgress={syncProgressByConnector[connector.id ?? ""] ?? null}
                totalEntries={entryCount}
                entriesLast24h={entries24h}
                lastSeen={lastSeenDate}
                onclick={async () => {
                  const dataSource = getConnectorDataSource(connector);
                  selectedConnector = {
                    id: connector.id,
                    connectorName: connector.id,
                    name: connector.name ?? connector.id,
                    status: isDisabled ? "Disabled" : "Offline",
                    description: connector.description,
                    totalEntries: dataSource?.totalEntries ?? 0,
                    lastEntryTime: dataSource?.lastSeen,
                    entriesLast24Hours: dataSource?.entriesLast24h ?? 0,
                    state: isDisabled ? "Disabled" : "Offline",
                    isHealthy: false,
                    isEnabled: connectorStatusInfo?.isEnabled,
                    hasDatabaseConfig: connectorStatusInfo?.hasDatabaseConfig,
                    hasSecrets: connectorStatusInfo?.hasSecrets,
                  };
                  granularSyncResult = null;
                  await loadConnectorCapabilitiesFor(connector.id);
                  showConnectorDialog = true;
                }}
              >
                {#snippet badges()}
                  <Badge
                    variant="secondary"
                    class="bg-blue-100 text-blue-800 dark:bg-blue-900 dark:text-blue-100 text-xs"
                  >
                    <Database class="h-3 w-3 mr-1" />
                    Has Data
                  </Badge>
                {/snippet}
              </DataSourceRow>
            {:else}
              <!-- Not connected and no data - show with configure button -->
              {@const Icon = getCategoryIcon(connector.category)}
              <a
                href="/settings/connectors/{connector.id?.toLowerCase()}"
                class="flex items-center gap-4 p-4 rounded-lg border bg-muted/30 hover:border-primary/50 hover:bg-accent/50 transition-colors group"
              >
                <div
                  class="flex h-10 w-10 shrink-0 items-center justify-center rounded-lg bg-primary/10"
                >
                  <Icon class="h-5 w-5 text-primary" />
                </div>
                <div class="flex-1 min-w-0">
                  <div class="flex items-center gap-2 flex-wrap">
                    <span class="font-medium">{connector.name}</span>
                    <Badge variant="outline" class="text-xs">
                      Not Configured
                    </Badge>
                  </div>
                  <p class="text-sm text-muted-foreground">
                    {connector.description}
                  </p>
                </div>
                <div class="flex items-center gap-2">
                  {#if connector.documentationUrl}
                    <Button
                      variant="ghost"
                      size="sm"
                      onclick={(e) => e.stopPropagation()}
                    >
                      <a
                        href={connector.documentationUrl}
                        target="_blank"
                        rel="noopener"
                      >
                        <ExternalLink class="h-4 w-4" />
                      </a>
                    </Button>
                  {/if}
                  <ChevronRight
                    class="h-4 w-4 text-muted-foreground group-hover:text-foreground transition-colors"
                  />
                </div>
              </a>
            {/if}
          {/each}
        </div>
        <p class="text-sm text-muted-foreground mt-4">
          Click on a connector to configure credentials and settings. Changes
          take effect immediately.
        </p>
      </CardContent>
    </Card>

    <!-- API Info -->
    {#if servicesOverview.apiEndpoint}
      <Card>
        <CardHeader>
          <CardTitle class="flex items-center gap-2">
            <Database class="h-5 w-5" />
            API Information
          </CardTitle>
          <CardDescription>
            Use these endpoints to configure uploaders manually
          </CardDescription>
        </CardHeader>
        <CardContent class="space-y-4">
          <div class="grid gap-4 sm:grid-cols-2">
            <div class="space-y-2">
              <span class="text-sm font-medium">Base URL</span>
              <div class="flex gap-2">
                <code
                  class="flex-1 px-3 py-2 rounded-md bg-muted text-sm font-mono truncate"
                >
                  {window.location.origin}
                </code>
                <Button
                  variant="outline"
                  size="icon"
                  onclick={() =>
                    copyToClipboard(
                      window.location.origin,
                      "baseUrl"
                    )}
                >
                  {#if copiedField === "baseUrl"}
                    <Check class="h-4 w-4 text-green-500" />
                  {:else}
                    <Copy class="h-4 w-4" />
                  {/if}
                </Button>
              </div>
            </div>
            <div class="space-y-2">
              <span class="text-sm font-medium">Entries Endpoint</span>
              <div class="flex gap-2">
                <code
                  class="flex-1 px-3 py-2 rounded-md bg-muted text-sm font-mono truncate"
                >
                  {`${window.location.origin}/api/v1/entries`}
                </code>
                <Button
                  variant="outline"
                  size="icon"
                  onclick={() =>
                    copyToClipboard(
                      `${window.location.origin}/api/v1/entries`,
                      "entries"
                    )}
                >
                  {#if copiedField === "entries"}
                    <Check class="h-4 w-4 text-green-500" />
                  {:else}
                    <Copy class="h-4 w-4" />
                  {/if}
                </Button>
              </div>
            </div>
          </div>
          <Separator />
          <p class="text-sm text-muted-foreground">
            Most uploaders use the Nightscout API format. Use your API secret
            for authentication via the <code class="text-xs">api-secret</code>
            header or embed it in the URL.
          </p>
        </CardContent>
      </Card>
    {/if}

    <ApiSecretSection />

    <!-- Data Maintenance -->
    <Card>
      <CardHeader>
        <CardTitle class="flex items-center gap-2">
          <Wrench class="h-5 w-5" />
          Data Maintenance
        </CardTitle>
        <CardDescription>
          Administrative tools for managing your data
        </CardDescription>
      </CardHeader>
      <CardContent class="space-y-4">
        <div class="flex items-start gap-4 p-4 rounded-lg border bg-card">
          <div
            class="flex h-10 w-10 shrink-0 items-center justify-center rounded-lg bg-primary/10"
          >
            <Link2 class="h-5 w-5 text-primary" />
          </div>
          <div class="flex-1">
            <h4 class="font-medium">Deduplicate Records</h4>
            <p class="text-sm text-muted-foreground mt-1">
              Link records from multiple data sources that represent the same
              underlying event. This improves data quality when the same glucose
              readings or treatments are uploaded from different apps.
            </p>
            <Button
              variant="outline"
              size="sm"
              class="mt-3 gap-2"
              onclick={() => (showDeduplicationDialog = true)}
            >
              {#if isDeduplicating}
                <Loader2 class="h-4 w-4 animate-spin" />
                Deduplication Running...
              {:else}
                <Link2 class="h-4 w-4" />
                Run Deduplication
              {/if}
            </Button>
          </div>
        </div>
      </CardContent>
    </Card>

    <!-- Integrations -->
    <Card>
      <CardHeader>
        <CardTitle class="flex items-center gap-2">
          <Link2 class="h-5 w-5" />
          Integrations
        </CardTitle>
        <CardDescription>
          Connect Nocturne to chat platforms and other external services
        </CardDescription>
      </CardHeader>
      <CardContent>
        <a
          href="/settings/integrations/discord"
          class="flex items-center justify-between rounded-lg border p-4 hover:bg-accent transition-colors"
        >
          <div class="flex items-center gap-3">
            <div class="flex h-10 w-10 items-center justify-center rounded-md bg-muted">
              <MessageSquare class="h-5 w-5" />
            </div>
            <div>
              <p class="font-medium">Discord</p>
              <p class="text-sm text-muted-foreground">
                Link a Discord account to receive alerts and use the Nocturne bot
              </p>
            </div>
          </div>
          <ChevronRight class="h-4 w-4 text-muted-foreground" />
        </a>
      </CardContent>
    </Card>

    <!-- Connected Apps Section -->
    <ConnectedApps />

    <!-- API Tokens Section -->
    <ApiTokens />
  {/if}
</div>

<!-- Setup Instructions Dialog -->
<UploaderSetupDialog bind:open={showSetupDialog} {selectedUploader} />

<!-- Demo Data Management Dialog -->
<Dialog.Root bind:open={showDemoDataDialog}>
  <Dialog.Content class="max-w-md">
    <Dialog.Header>
      <Dialog.Title class="flex items-center gap-2">
        <Sparkles class="h-5 w-5 text-purple-500" />
        Demo Data
      </Dialog.Title>
      <Dialog.Description>
        Manage the simulated demo data in your Nocturne instance
      </Dialog.Description>
    </Dialog.Header>

    <div class="space-y-4 py-4">
      {#if demoDeleteResult}
        {#if demoDeleteResult.success}
          <div
            class="rounded-lg border border-green-200 dark:border-green-800 bg-green-50 dark:bg-green-950/20 p-4"
          >
            <div
              class="flex items-center gap-2 text-green-800 dark:text-green-200"
            >
              <CheckCircle class="h-5 w-5" />
              <span class="font-medium">Demo data cleared successfully</span>
            </div>
            <p class="text-sm text-green-700 dark:text-green-300 mt-1">
              Deleted {demoDeleteResult.totalDeleted?.toLocaleString() ?? 0} records
            </p>
          </div>
        {:else}
          <div
            class="rounded-lg border border-red-200 dark:border-red-800 bg-red-50 dark:bg-red-950/20 p-4"
          >
            <div class="flex items-center gap-2 text-red-800 dark:text-red-200">
              <AlertCircle class="h-5 w-5" />
              <span class="font-medium">Failed to delete demo data</span>
            </div>
            <p class="text-sm text-red-700 dark:text-red-300 mt-1">
              {demoDeleteResult.error}
            </p>
          </div>
        {/if}
      {:else}
        <div
          class="rounded-lg border border-purple-200 dark:border-purple-800 bg-purple-50 dark:bg-purple-950/20 p-4"
        >
          <p class="text-sm text-purple-800 dark:text-purple-200">
            <strong>This is demo data</strong>
            — synthetic glucose readings generated for testing and demonstration purposes.
          </p>
        </div>

        <div class="space-y-3">
          <p class="text-sm text-muted-foreground">
            You can safely delete all demo data. It's very easy to regenerate:
          </p>
          <ul
            class="text-sm text-muted-foreground list-disc list-inside space-y-1"
          >
            <li>Restart the demo service to regenerate data</li>
            <li>Only demo-generated data will be deleted</li>
            <li>Your real health data (if any) is not affected</li>
          </ul>
        </div>
      {/if}
    </div>

    <Dialog.Footer>
      <Button variant="outline" onclick={() => (showDemoDataDialog = false)}>
        Close
      </Button>
      {#if !demoDeleteResult?.success}
        <Button
          variant="destructive"
          onclick={deleteDemoData}
          disabled={isDeletingDemo}
          class="gap-2"
        >
          {#if isDeletingDemo}
            <Loader2 class="h-4 w-4 animate-spin" />
            Deleting...
          {:else}
            <Trash2 class="h-4 w-4" />
            Clear Demo Data
          {/if}
        </Button>
      {/if}
    </Dialog.Footer>
  </Dialog.Content>
</Dialog.Root>

<!-- Data Source Management Dialog -->
<Dialog.Root bind:open={showManageDataSourceDialog}>
  <Dialog.Content class="max-w-md">
    {#if selectedDataSource}
      {@const Icon = getCategoryIcon(selectedDataSource.category)}
      <Dialog.Header>
        <Dialog.Title class="flex items-center gap-2">
          <Icon class="h-5 w-5" />
          {selectedDataSource.name}
        </Dialog.Title>
        <Dialog.Description>
          {selectedDataSource.description ?? selectedDataSource.deviceId}
        </Dialog.Description>
      </Dialog.Header>

      <div class="space-y-4 py-4">
        <div class="grid grid-cols-2 gap-4 text-sm">
          <div>
            <span class="text-muted-foreground">Status</span>
            <div class="mt-1">
              <Badge
                variant={getStatusBadge(selectedDataSource.status).variant}
                class={getStatusBadge(selectedDataSource.status).class}
              >
                {getStatusBadge(selectedDataSource.status).text}
              </Badge>
            </div>
          </div>
          <div>
            <span class="text-muted-foreground">Last Seen</span>
            <p class="mt-1 font-medium">
              {formatLastSeen(selectedDataSource.lastSeen)}
            </p>
          </div>
          <div>
            <span class="text-muted-foreground">Records (24h)</span>
            <p class="mt-1 font-medium">
              {selectedDataSource.entriesLast24h?.toLocaleString() ?? 0}
            </p>
          </div>
          <div>
            <span class="text-muted-foreground">Total Records</span>
            <p class="mt-1 font-medium">
              {selectedDataSource.totalEntries?.toLocaleString() ?? 0}
            </p>
          </div>
        </div>

        <Separator />

        <div
          class="rounded-lg border border-amber-200 dark:border-amber-800 bg-amber-50 dark:bg-amber-950/20 p-4"
        >
          <div class="flex items-start gap-3">
            <AlertTriangle
              class="h-5 w-5 text-amber-600 dark:text-amber-400 shrink-0 mt-0.5"
            />
            <div>
              <p class="text-sm font-medium text-amber-800 dark:text-amber-200">
                Delete All Data from This Source
              </p>
              <p class="text-sm text-amber-700 dark:text-amber-300 mt-1">
                This will permanently delete all entries, treatments, and device
                status records from this data source.
              </p>
            </div>
          </div>
        </div>
      </div>

      <Dialog.Footer>
        <Button
          variant="outline"
          onclick={() => (showManageDataSourceDialog = false)}
        >
          Cancel
        </Button>
        <Button
          variant="outline"
          class="gap-2"
          onclick={() => { showDeleteConfirmDialog = true; deleteConfirmText = ""; }}
        >
          <Pencil class="h-4 w-4" />
          Delete Data...
        </Button>
      </Dialog.Footer>
    {/if}
  </Dialog.Content>
</Dialog.Root>

<!-- Delete Confirmation Dialog (Stern Warning) -->
<AlertDialog.Root bind:open={showDeleteConfirmDialog}>
  <AlertDialog.Content>
    <AlertDialog.Header>
      <AlertDialog.Title class="flex items-center gap-2 text-destructive">
        <AlertTriangle class="h-5 w-5" />
        Permanently Delete Data
      </AlertDialog.Title>
      <AlertDialog.Description class="space-y-4">
        {#if selectedDataSource}
          <div
            class="rounded-lg border border-red-200 dark:border-red-800 bg-red-50 dark:bg-red-950/20 p-4 mt-4"
          >
            <p class="text-sm font-semibold text-red-800 dark:text-red-200">
              ⚠️ THIS ACTION CANNOT BE UNDONE
            </p>
            <p class="text-sm text-red-700 dark:text-red-300 mt-2">
              You are about to permanently delete <strong>all data</strong>
              from
              <strong>{selectedDataSource.name}</strong>
              . This includes:
            </p>
            <ul
              class="text-sm text-red-700 dark:text-red-300 list-disc list-inside mt-2 space-y-1"
            >
              <li>
                All glucose records ({selectedDataSource.totalEntries?.toLocaleString() ??
                  0} records)
              </li>
              <li>All treatments entered by this device</li>
              <li>All device status records</li>
            </ul>
          </div>

          {#if deleteResult}
            {#if deleteResult.success}
              <div
                class="rounded-lg border border-green-200 dark:border-green-800 bg-green-50 dark:bg-green-950/20 p-4"
              >
                <div
                  class="flex items-center gap-2 text-green-800 dark:text-green-200"
                >
                  <CheckCircle class="h-5 w-5" />
                  <span class="font-medium">Data deleted successfully</span>
                </div>
                <p class="text-sm text-green-700 dark:text-green-300 mt-1">
                  Deleted {deleteResult.totalDeleted?.toLocaleString() ?? 0} records
                </p>
              </div>
            {:else}
              <div
                class="rounded-lg border border-red-200 dark:border-red-800 bg-red-50 dark:bg-red-950/20 p-4"
              >
                <div
                  class="flex items-center gap-2 text-red-800 dark:text-red-200"
                >
                  <AlertCircle class="h-5 w-5" />
                  <span class="font-medium">Failed to delete data</span>
                </div>
                <p class="text-sm text-red-700 dark:text-red-300 mt-1">
                  {deleteResult.error}
                </p>
              </div>
            {/if}
          {:else}
            <div class="space-y-2 mt-4">
              <label for="confirm-delete" class="text-sm font-medium">
                Type <strong>DELETE</strong>
                to confirm:
              </label>
              <input
                id="confirm-delete"
                type="text"
                bind:value={deleteConfirmText}
                class="w-full px-3 py-2 rounded-md border bg-background text-sm"
                placeholder="Type DELETE"
              />
            </div>
          {/if}
        {/if}
      </AlertDialog.Description>
    </AlertDialog.Header>
    <AlertDialog.Footer>
      <AlertDialog.Cancel onclick={() => (showDeleteConfirmDialog = false)}>
        Cancel
      </AlertDialog.Cancel>
      {#if !deleteResult?.success}
        <Button
          variant="destructive"
          onclick={deleteDataSource}
          disabled={isDeletingDataSource || deleteConfirmText !== "DELETE"}
          class="gap-2"
        >
          {#if isDeletingDataSource}
            <Loader2 class="h-4 w-4 animate-spin" />
            Deleting...
          {:else}
            <Trash2 class="h-4 w-4" />
            Delete All Data
          {/if}
        </Button>
      {/if}
    </AlertDialog.Footer>
  </AlertDialog.Content>
</AlertDialog.Root>

<ManualSyncDialog bind:open={showManualSyncDialog} {isManualSyncing} {manualSyncResult} />

<!-- Connector Details Dialog -->
<ConnectorDetailsDialog bind:open={showConnectorDialog} {selectedConnector} {selectedConnectorCapabilities} onSyncComplete={loadConnectorStatuses} />

<DeduplicationDialog bind:open={showDeduplicationDialog} bind:isDeduplicating />
