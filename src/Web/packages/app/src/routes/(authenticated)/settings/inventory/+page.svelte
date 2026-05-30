<script lang="ts">
  import { goto } from "$app/navigation";
  import { Button } from "$lib/components/ui/button";
  import { Badge } from "$lib/components/ui/badge";
  import * as Card from "$lib/components/ui/card";
  import * as Dialog from "$lib/components/ui/dialog";
  import { Input } from "$lib/components/ui/input";
  import { Label } from "$lib/components/ui/label";
  import { Textarea } from "$lib/components/ui/textarea";
  import * as inventoryRemote from "$api/generated/inventories.generated.remote";
  import {
    InventoryAutoConsumeSource,
    InventoryCatalogCategory,
    InventoryCategory,
    InventoryKind,
    InventoryStorageState,
    TherapyMode,
    type InventoryCatalogEntry,
    type InventoryItemDetailDto,
    type InventoryItemDto,
  } from "$api";
  import {
    ArrowUpRight,
    Archive,
    ClipboardList,
    Loader2,
    PackagePlus,
    PackageSearch,
    Plus,
    RefreshCw,
    RotateCcw,
    Trash2,
  } from "lucide-svelte";

  const itemsQuery = inventoryRemote.getItems(undefined);

  const items = $derived<InventoryItemDto[]>(itemsQuery.current ?? []);
  const hasItems = $derived(items.length > 0);

  let itemDialogOpen = $state(false);
  let restockDialogOpen = $state(false);
  let consumeDialogOpen = $state(false);
  let editingItem = $state<InventoryItemDto | null>(null);
  let selectedItem = $state<InventoryItemDetailDto | null>(null);
  let busyAction = $state<string | null>(null);

  let itemName = $state("");
  let itemCategory = $state<InventoryCategory>(InventoryCategory.Cgm);
  let itemKind = $state<InventoryKind>(InventoryKind.Custom);
  let itemUnit = $state("each");
  let itemThreshold = $state(1);
  let itemTarget = $state<number | undefined>(undefined);
  let itemAutoConsume = $state(false);
  let itemAutoSource = $state<InventoryAutoConsumeSource>(InventoryAutoConsumeSource.None);
  let itemDeviceEvents = $state("");
  let itemLinkedInsulinId = $state<string | undefined>(undefined);
  let itemLinkedUnitsPerUse = $state<number | undefined>(undefined);

  // Multi-step seed wizard state.
  type WizardStep = "mode" | "cgm" | "pump" | "rapid" | "basal" | "confirm";
  let wizardOpen = $state(false);
  let wizardStep = $state<WizardStep>("mode");
  let wizardMode = $state<TherapyMode>(TherapyMode.Pump);
  let wizardCatalog = $state<InventoryCatalogEntry[]>([]);
  let wizardLoadingCatalog = $state(false);
  let wizardCgmKeys = $state<string[]>([]);
  let wizardPumpKey = $state<string | null>(null);
  let wizardRapidKey = $state<string | null>(null);
  let wizardBasalKey = $state<string | null>(null);

  const therapyModeLabels: Record<TherapyMode, string> = {
    [TherapyMode.Mdi]: "MDI (injections only)",
    [TherapyMode.Pump]: "Insulin pump",
  };

  let consumeQuantity = $state(1);
  let consumeReason = $state("Manual use");
  let consumeNotes = $state("");

  let batchQuantity = $state(1);
  let batchReceived = $state("");
  let batchExpires = $state("");
  let batchLot = $state("");
  let batchStorage = $state<InventoryStorageState>(InventoryStorageState.Normal);
  let batchNotes = $state("");

  const categoryLabels: Record<InventoryCategory, string> = {
    [InventoryCategory.Cgm]: "CGM",
    [InventoryCategory.Pump]: "Pump",
    [InventoryCategory.Insulin]: "Insulin",
    [InventoryCategory.Testing]: "Testing",
    [InventoryCategory.Emergency]: "Emergency",
    [InventoryCategory.Other]: "Other",
  };

  const kindLabels: Record<InventoryKind, string> = {
    [InventoryKind.CgmSensor]: "CGM sensor",
    [InventoryKind.CgmTransmitter]: "CGM transmitter",
    [InventoryKind.Pod]: "Pump pod",
    [InventoryKind.InfusionSet]: "Infusion set",
    [InventoryKind.Cannula]: "Cannula",
    [InventoryKind.Reservoir]: "Reservoir",
    [InventoryKind.PumpBattery]: "Pump battery",
    [InventoryKind.Insulin]: "Insulin",
    [InventoryKind.TestStrip]: "Test strip",
    [InventoryKind.Lancet]: "Lancet",
    [InventoryKind.AlcoholSwab]: "Alcohol swab",
    [InventoryKind.ControlSolution]: "Control solution",
    [InventoryKind.Glucagon]: "Glucagon",
    [InventoryKind.FastCarbs]: "Fast carbs",
    [InventoryKind.KetoneStrip]: "Ketone strip",
    [InventoryKind.Custom]: "Custom",
  };

  const autoSourceLabels: Record<InventoryAutoConsumeSource, string> = {
    [InventoryAutoConsumeSource.None]: "None",
    [InventoryAutoConsumeSource.DeviceEvent]: "Device events",
    [InventoryAutoConsumeSource.Bolus]: "Bolus insulin",
    [InventoryAutoConsumeSource.BasalInjection]: "Basal injections",
  };

  const storageLabels: Record<InventoryStorageState, string> = {
    [InventoryStorageState.Normal]: "Normal",
    [InventoryStorageState.Refrigerated]: "Refrigerated",
    [InventoryStorageState.Opened]: "Opened",
    [InventoryStorageState.Frozen]: "Frozen",
    [InventoryStorageState.HeatExposed]: "Heat exposed",
    [InventoryStorageState.Discarded]: "Discarded",
  };

  const groupedItems = $derived.by(() => {
    const groups = new Map<InventoryCategory, InventoryItemDto[]>();
    for (const item of items) {
      const category = item.category ?? InventoryCategory.Other;
      groups.set(category, [...(groups.get(category) ?? []), item]);
    }
    return Object.values(InventoryCategory)
      .map((category) => ({ category, items: groups.get(category) ?? [] }))
      .filter((group) => group.items.length > 0);
  });

  function openCreateItem() {
    editingItem = null;
    itemName = "";
    itemCategory = InventoryCategory.Cgm;
    itemKind = InventoryKind.Custom;
    itemUnit = "each";
    itemThreshold = 1;
    itemTarget = undefined;
    itemAutoConsume = false;
    itemAutoSource = InventoryAutoConsumeSource.None;
    itemDeviceEvents = "";
    itemLinkedInsulinId = undefined;
    itemLinkedUnitsPerUse = undefined;
    itemDialogOpen = true;
  }

  // Items eligible to be linked from a Pod/Reservoir as the insulin source.
  const insulinItems = $derived(items.filter((i) => i.kind === InventoryKind.Insulin && !i.isArchived));
  const showLinkedInsulin = $derived(itemKind === InventoryKind.Pod || itemKind === InventoryKind.Reservoir);

  async function reloadSelected() {
    await itemsQuery.refresh();
    if (selectedItem?.id) selectedItem = await inventoryRemote.getItem(selectedItem.id).run();
  }

  function openRestock(item: InventoryItemDto | InventoryItemDetailDto) {
    selectedItem = item as InventoryItemDetailDto;
    batchQuantity = item.suggestedRestockQuantity || 1;
    batchReceived = dateInputValue(new Date());
    batchExpires = "";
    batchLot = "";
    batchStorage = InventoryStorageState.Normal;
    batchNotes = "";
    restockDialogOpen = true;
  }

  async function submitItem() {
    busyAction = editingItem ? "updateItem" : "createItem";
    try {
      const request = {
        name: itemName,
        category: itemCategory,
        kind: itemKind,
        unitLabel: itemUnit,
        lowStockThreshold: itemThreshold,
        targetStock: itemTarget,
        autoConsumeEnabled: itemAutoConsume || itemAutoSource !== InventoryAutoConsumeSource.None,
        autoConsumeSource: itemAutoSource,
        deviceEventTypes: eventTypes(),
        linkedInsulinItemId: showLinkedInsulin ? itemLinkedInsulinId : undefined,
        linkedInsulinUnitsPerUse: showLinkedInsulin ? itemLinkedUnitsPerUse : undefined,
      };
      if (editingItem?.id) {
        await inventoryRemote.updateItem({ id: editingItem.id, request });
      } else {
        await inventoryRemote.createItem(request);
      }
      await itemsQuery.refresh();
      if (selectedItem?.id) selectedItem = await inventoryRemote.getItem(selectedItem.id).run();
      itemDialogOpen = false;
    } finally {
      busyAction = null;
    }
  }

  async function submitAddBatch() {
    if (!selectedItem?.id) return;
    busyAction = "addBatch";
    try {
      await inventoryRemote.addBatch({
        itemId: selectedItem.id,
        request: {
          quantity: batchQuantity,
          receivedAt: batchReceived ? new Date(batchReceived).toISOString() : undefined,
          expiresAt: batchExpires ? new Date(batchExpires).toISOString() : undefined,
          lotNumber: batchLot || undefined,
          storageState: batchStorage,
          notes: batchNotes || undefined,
        } as any,
      });
      restockDialogOpen = false;
      await reloadSelected();
    } finally {
      busyAction = null;
    }
  }

  function openConsume(item: InventoryItemDto | InventoryItemDetailDto) {
    selectedItem = item as InventoryItemDetailDto;
    consumeQuantity = 1;
    consumeReason = "Manual use";
    consumeNotes = "";
    consumeDialogOpen = true;
  }

  async function openWizard() {
    wizardStep = "mode";
    wizardMode = TherapyMode.Pump;
    wizardCgmKeys = [];
    wizardPumpKey = null;
    wizardRapidKey = null;
    wizardBasalKey = null;
    wizardCatalog = [];
    wizardOpen = true;
    await loadWizardCatalog(wizardMode);
  }

  async function loadWizardCatalog(mode: TherapyMode) {
    wizardLoadingCatalog = true;
    try {
      const result = await inventoryRemote.getInventoryCatalog({ mode }).run();
      wizardCatalog = result ?? [];
    } finally {
      wizardLoadingCatalog = false;
    }
  }

  // Catalog entries grouped by category, for wizard step rendering.
  const wizardCgmEntries = $derived(wizardCatalog.filter((e) => e.category === InventoryCatalogCategory.Cgm));
  const wizardPumpEntries = $derived(wizardCatalog.filter((e) => e.category === InventoryCatalogCategory.Pump));
  const wizardRapidEntries = $derived(wizardCatalog.filter((e) => e.category === InventoryCatalogCategory.RapidInsulin));
  const wizardBasalEntries = $derived(wizardCatalog.filter((e) => e.category === InventoryCatalogCategory.BasalInsulin));

  // CGM and pump entries grouped by brand for rendering.
  function groupByBrand(entries: InventoryCatalogEntry[]) {
    const groups = new Map<string, InventoryCatalogEntry[]>();
    for (const entry of entries) {
      const brand = entry.brand ?? "Other";
      groups.set(brand, [...(groups.get(brand) ?? []), entry]);
    }
    return Array.from(groups.entries()).map(([brand, items]) => ({ brand, items }));
  }
  const wizardCgmGroups = $derived(groupByBrand(wizardCgmEntries));
  const wizardPumpGroups = $derived(groupByBrand(wizardPumpEntries));

  // Next-step button enablement per step.
  const wizardCanAdvance = $derived.by(() => {
    switch (wizardStep) {
      case "mode": return true; // mode always picked (radio default)
      case "cgm": return wizardCgmKeys.length > 0;
      case "pump": return wizardPumpKey !== null;
      case "rapid": return wizardRapidKey !== null;
      case "basal": return wizardBasalKey !== null;
      case "confirm": return true;
    }
  });

  function wizardNextStep() {
    switch (wizardStep) {
      case "mode":
        wizardStep = "cgm";
        break;
      case "cgm":
        wizardStep = wizardMode === TherapyMode.Pump ? "pump" : "rapid";
        break;
      case "pump":
        wizardStep = "rapid";
        break;
      case "rapid":
        wizardStep = wizardMode === TherapyMode.Mdi ? "basal" : "confirm";
        break;
      case "basal":
        wizardStep = "confirm";
        break;
    }
  }

  function wizardPrevStep() {
    switch (wizardStep) {
      case "cgm":
        wizardStep = "mode";
        break;
      case "pump":
        wizardStep = "cgm";
        break;
      case "rapid":
        wizardStep = wizardMode === TherapyMode.Pump ? "pump" : "cgm";
        break;
      case "basal":
        wizardStep = "rapid";
        break;
      case "confirm":
        wizardStep = wizardMode === TherapyMode.Mdi ? "basal" : "rapid";
        break;
    }
  }

  async function wizardSwitchMode(mode: TherapyMode) {
    if (mode === wizardMode) return;
    wizardMode = mode;
    wizardPumpKey = null;
    wizardBasalKey = null;
    await loadWizardCatalog(mode);
  }

  function toggleCgmSelection(key: string) {
    const i = wizardCgmKeys.indexOf(key);
    if (i >= 0) {
      wizardCgmKeys = wizardCgmKeys.filter((k) => k !== key);
    } else {
      wizardCgmKeys = [...wizardCgmKeys, key];
    }
  }

  async function wizardSubmit() {
    busyAction = "seed";
    try {
      await inventoryRemote.seedFromSelection({
        therapyMode: wizardMode,
        cgmKeys: wizardCgmKeys,
        pumpKey: wizardPumpKey ?? undefined,
        rapidInsulinKey: wizardRapidKey ?? undefined,
        basalInsulinKey: wizardBasalKey ?? undefined,
      });
      await itemsQuery.refresh();
      wizardOpen = false;
    } finally {
      busyAction = null;
    }
  }

  function findCatalogEntry(key: string | null): InventoryCatalogEntry | undefined {
    if (!key) return undefined;
    return wizardCatalog.find((e) => e.key === key);
  }

  // Confirm-step lookups — kept here because Svelte 5 only allows `{@const}` as
  // a direct child of block tags ({#if}/{#each}/etc.), not nested in <div>s.
  const wizardPumpEntry = $derived(findCatalogEntry(wizardPumpKey));
  const wizardRapidEntry = $derived(findCatalogEntry(wizardRapidKey));
  const wizardBasalEntry = $derived(findCatalogEntry(wizardBasalKey));

  // Returns the DeviceEvent type to log for items whose "primary" inventory
  // action is changing a worn device (pod/reservoir/CGM sensor). null for
  // items without that semantic (insulin, strips, lancets, etc.).
  function changeEventTypeFor(kind: InventoryKind | undefined): string | null {
    switch (kind) {
      case InventoryKind.Pod: return "PodChange";
      case InventoryKind.Reservoir: return "ReservoirChange";
      // Emit SensorStart (not SensorChange) — matches Trio/xDrip+ semantics for
      // new physical sensor sessions, and aligns with the catalog auto-consume
      // subscription which deliberately excludes SensorChange to avoid AAPS
      // double-counting on Sensor Start + Sensor Change pairs.
      case InventoryKind.CgmSensor: return "SensorStart";
      default: return null;
    }
  }

  function changeLabelFor(kind: InventoryKind | undefined): string {
    switch (kind) {
      case InventoryKind.Pod: return "Change pod";
      case InventoryKind.Reservoir: return "Change reservoir";
      case InventoryKind.CgmSensor: return "Change sensor";
      default: return "Change";
    }
  }

  async function markItemChanged(item: InventoryItemDto) {
    if (!item.id) return;
    const eventType = changeEventTypeFor(item.kind);
    if (!eventType) return;
    busyAction = `change-${item.id}`;
    try {
      // Post a device event through the existing v4 endpoint; the inventory
      // hook in DeviceEventController picks it up and auto-consumes this
      // item (and any linked insulin bottle).
      const response = await fetch("/api/v4/observations/device-events", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        credentials: "include",
        body: JSON.stringify({
          timestamp: new Date().toISOString(),
          eventType,
          notes: `Logged from inventory: ${item.name}`,
        }),
      });
      if (!response.ok) {
        console.error(`Failed to log ${eventType}:`, response.status);
        return;
      }
      await itemsQuery.refresh();
    } finally {
      busyAction = null;
    }
  }

  async function archiveCurrentItem() {
    if (!editingItem?.id) return;
    busyAction = "archive";
    try {
      await inventoryRemote.archiveItem(editingItem.id);
      await itemsQuery.refresh();
      itemDialogOpen = false;
    } finally {
      busyAction = null;
    }
  }

  async function submitConsume() {
    if (!selectedItem?.id) return;
    busyAction = "consume";
    try {
      await inventoryRemote.consume({
        itemId: selectedItem.id,
        request: {
          quantity: consumeQuantity,
          reason: consumeReason || undefined,
          notes: consumeNotes || undefined,
        },
      });
      consumeDialogOpen = false;
      await reloadSelected();
    } finally {
      busyAction = null;
    }
  }

  function displayNumber(value: number | undefined): string {
    return (value ?? 0).toLocaleString(undefined, { maximumFractionDigits: 2 });
  }

  function formatDate(value: Date | string | undefined): string {
    if (!value) return "None";
    return new Date(value).toLocaleDateString(undefined, { month: "short", day: "numeric", year: "numeric" });
  }

  function dateInputValue(value: Date | string | undefined): string {
    if (!value) return "";
    return new Date(value).toISOString().slice(0, 10);
  }

  function formatRunOut(item: InventoryItemDto): string {
    if (!item.estimatedRunOutAt) return "—";
    if ((item.usableStock ?? 0) <= 0) return "Out of stock";
    const target = new Date(item.estimatedRunOutAt).getTime();
    const days = Math.round((target - Date.now()) / (24 * 60 * 60 * 1000));
    if (days <= 0) return "Today";
    if (days === 1) return "Tomorrow";
    if (days < 30) return `in ${days} days`;
    return formatDate(item.estimatedRunOutAt);
  }

  function runOutTooltip(item: InventoryItemDto): string {
    switch (item.runOutProjectionSource) {
      case "WearTime":
        return item.wearDays ? `Based on ${item.wearDays}-day wear time` : "Based on wear time";
      case "HistoricalRate":
        return "Based on average daily use over the last 14 days";
      case "LinkedItem":
        return "Based on the linked pump pod/reservoir wear-time";
      default:
        return "Not enough data to project a run-out date";
    }
  }

  function eventTypes(): string[] {
    return itemDeviceEvents.split(",").map((value) => value.trim()).filter(Boolean);
  }
</script>

<svelte:head>
  <title>Inventory - Settings - Nocturne</title>
</svelte:head>

<div class="container mx-auto max-w-7xl space-y-6 p-4 md:p-6">
  <div class="flex flex-col gap-3 sm:flex-row sm:items-center sm:justify-between">
    <div>
      <h1 class="text-3xl font-bold tracking-tight">Inventory</h1>
      <p class="text-muted-foreground">Track supply stock, batches, expirations, and automatic consumption.</p>
    </div>
    <div class="flex flex-wrap gap-2">
      <Button variant="outline" onclick={openWizard} disabled={busyAction === "seed"} class="gap-2"
        title="Open the multi-step wizard to seed common diabetes supplies based on your therapy mode, CGM, pump, and insulin">
        {#if busyAction === "seed"}
          <Loader2 class="h-4 w-4 animate-spin" />
        {:else}
          <ClipboardList class="h-4 w-4" />
        {/if}
        Enable suggested list
      </Button>
      <Button onclick={openCreateItem} class="gap-2"
        title="Create a custom inventory item not covered by the suggested catalog">
        <Plus class="h-4 w-4" />
        Add item
      </Button>
    </div>
  </div>

  {#if !hasItems}
    <Card.Root>
      <Card.Content class="flex flex-col items-center gap-4 py-14 text-center">
        <PackageSearch class="h-12 w-12 text-muted-foreground" />
        <div class="space-y-1">
          <h2 class="text-lg font-semibold">No inventory items yet</h2>
          <p class="max-w-xl text-sm text-muted-foreground">
            Start with the suggested diabetes supply list, then edit thresholds and restock batches as needed.
          </p>
        </div>
        <Button onclick={openWizard} disabled={busyAction === "seed"} class="gap-2">
          <ClipboardList class="h-4 w-4" />
          Enable suggested supply list
        </Button>
      </Card.Content>
    </Card.Root>
  {:else}
    <div class="space-y-6">
      {#each groupedItems as group}
        <section class="space-y-3">
          <div class="flex items-center gap-2">
            <h2 class="text-lg font-semibold">{categoryLabels[group.category]}</h2>
            <Badge variant="outline">{group.items.length}</Badge>
          </div>
          <div class="grid grid-cols-1 gap-3 lg:grid-cols-2 xl:grid-cols-3">
            {#each group.items as item}
              <Card.Root class="overflow-hidden">
                <Card.Header class="space-y-3">
                  <div class="flex items-start justify-between gap-3">
                    <div class="min-w-0 flex-1">
                      <div class="flex items-start justify-between gap-2">
                        <div class="min-w-0">
                          <Card.Title class="truncate text-base">{item.name}</Card.Title>
                          <p class="text-sm text-muted-foreground">{kindLabels[item.kind ?? InventoryKind.Custom]}</p>
                        </div>
                        <Button
                          size="icon"
                          variant="ghost"
                          class="h-7 w-7 shrink-0 text-muted-foreground"
                          onclick={() => item.id && goto(`/settings/inventory/${item.id}`)}
                          title="Open full detail view"
                        >
                          <ArrowUpRight class="h-4 w-4" />
                        </Button>
                      </div>
                    </div>
                  </div>
                  <div class="flex flex-wrap gap-1">
                    {#if item.isLow}
                      <Badge variant="destructive">Low</Badge>
                    {/if}
                    {#if (item.expiredStock ?? 0) > 0}
                      <Badge variant="secondary">Expired</Badge>
                    {/if}
                    {#if (item.expiringSoonStock ?? 0) > 0}
                      <Badge variant="outline">Expiring soon</Badge>
                    {/if}
                  </div>
                </Card.Header>
                <Card.Content class="space-y-4">
                  <div class="grid grid-cols-2 gap-3 text-sm sm:grid-cols-4">
                    <div>
                      <p class="text-muted-foreground">In stock</p>
                      <p class="text-xl font-semibold">{displayNumber(item.usableStock)} <span class="text-sm font-normal">{item.unitLabel}</span></p>
                    </div>
                    <div>
                      <p class="text-muted-foreground">Threshold</p>
                      <p class="text-xl font-semibold">{displayNumber(item.lowStockThreshold)}</p>
                    </div>
                    <div title={runOutTooltip(item)}>
                      <p class="text-muted-foreground">Runs out</p>
                      <p class="font-medium">{formatRunOut(item)}</p>
                    </div>
                    <div>
                      <p class="text-muted-foreground">Next expiry</p>
                      <p class="font-medium">{formatDate(item.lowestExpiry)}</p>
                    </div>
                  </div>
                  <div class="flex flex-wrap gap-2">
                    {#if changeEventTypeFor(item.kind)}
                      <Button
                        size="sm"
                        onclick={() => markItemChanged(item)}
                        disabled={busyAction === `change-${item.id}`}
                        class="gap-2"
                        title="Log a real-world device change. Decrements this item by 1 and (for pods/reservoirs) drains the linked insulin bottle by the per-change unit amount. Also creates a DeviceEvent that shows up on charts and reports."
                      >
                        {#if busyAction === `change-${item.id}`}
                          <Loader2 class="h-4 w-4 animate-spin" />
                        {:else}
                          <RefreshCw class="h-4 w-4" />
                        {/if}
                        {changeLabelFor(item.kind)}
                      </Button>
                      <Button size="sm" variant="outline" onclick={() => openRestock(item)} class="gap-2"
                        title="Add a newly-received batch with quantity, expiry, lot number, and storage state">
                        <PackagePlus class="h-4 w-4" />
                        Restock
                      </Button>
                    {:else}
                      <Button size="sm" variant="outline" onclick={() => openRestock(item)} class="gap-2"
                        title="Add a newly-received batch with quantity, expiry, lot number, and storage state">
                        <PackagePlus class="h-4 w-4" />
                        Restock
                      </Button>
                      <Button size="sm" variant="outline" onclick={() => openConsume(item)} class="gap-2"
                        title="Manually log consumption — quantity drains the oldest non-expired batch first (FEFO)">
                        <RotateCcw class="h-4 w-4" />
                        Use
                      </Button>
                    {/if}
                    {#if changeEventTypeFor(item.kind)}
                      <Button size="sm" variant="ghost" onclick={() => openConsume(item)} class="ml-auto gap-2 text-muted-foreground" title="Log a unit removed from stock without wearing it (e.g. damaged, discarded)">
                        <Trash2 class="h-4 w-4" />
                        Log waste
                      </Button>
                    {/if}
                  </div>
                  {#if (item.kind === InventoryKind.Pod || item.kind === InventoryKind.Reservoir) && item.linkedInsulinItemId && item.linkedInsulinUnitsPerUse}
                    <p class="text-xs text-muted-foreground">
                      Changing also consumes <span class="font-medium">{displayNumber(item.linkedInsulinUnitsPerUse)} u</span> from the linked insulin bottle.
                    </p>
                  {/if}
                </Card.Content>
              </Card.Root>
            {/each}
          </div>
        </section>
      {/each}
    </div>
  {/if}
</div>

<Dialog.Root bind:open={itemDialogOpen}>
  <Dialog.Content class="sm:max-w-2xl">
    <Dialog.Header>
      <Dialog.Title>{editingItem ? "Edit inventory item" : "Add inventory item"}</Dialog.Title>
      <Dialog.Description>Set stock thresholds, category, and automatic consumption rules.</Dialog.Description>
    </Dialog.Header>
    <div class="grid gap-4 py-4 md:grid-cols-2">
      <div class="space-y-2 md:col-span-2">
        <Label for="item-name">Name</Label>
        <Input id="item-name" bind:value={itemName} />
      </div>
      <div class="space-y-2">
        <Label for="item-category">Category</Label>
        <select id="item-category" bind:value={itemCategory} class="h-10 w-full rounded-md border bg-background px-3 text-sm">
          {#each Object.values(InventoryCategory) as category}
            <option value={category}>{categoryLabels[category]}</option>
          {/each}
        </select>
      </div>
      <div class="space-y-2">
        <Label for="item-kind">Kind</Label>
        <select id="item-kind" bind:value={itemKind} class="h-10 w-full rounded-md border bg-background px-3 text-sm">
          {#each Object.values(InventoryKind) as kind}
            <option value={kind}>{kindLabels[kind]}</option>
          {/each}
        </select>
      </div>
      <div class="space-y-2">
        <Label for="item-unit">Unit</Label>
        <Input id="item-unit" bind:value={itemUnit} />
      </div>
      <div class="space-y-2">
        <Label for="item-threshold">Low stock threshold</Label>
        <Input id="item-threshold" type="number" min="0" step="0.01" bind:value={itemThreshold} />
      </div>
      <div class="space-y-2">
        <Label for="item-target">Target stock</Label>
        <Input id="item-target" type="number" min="0" step="0.01" bind:value={itemTarget} />
      </div>
      <div class="space-y-2">
        <Label for="item-auto-source">Auto-consume source</Label>
        <select id="item-auto-source" bind:value={itemAutoSource} class="h-10 w-full rounded-md border bg-background px-3 text-sm">
          {#each Object.values(InventoryAutoConsumeSource) as source}
            <option value={source}>{autoSourceLabels[source]}</option>
          {/each}
        </select>
      </div>
      <div class="space-y-2 md:col-span-2">
        <Label for="item-events">Device event mappings</Label>
        <Input id="item-events" bind:value={itemDeviceEvents} placeholder="SensorStart, SensorChange" />
      </div>

      {#if showLinkedInsulin}
        <div class="space-y-2 md:col-span-2 rounded-md border border-dashed p-3">
          <p class="text-xs text-muted-foreground">
            For pump users: each change of this {itemKind === InventoryKind.Pod ? "pod" : "reservoir"} also drains the linked insulin bottle. Capacity defaults to 200u (pod) / 300u (reservoir).
          </p>
          <div class="grid gap-3 md:grid-cols-2">
            <div class="space-y-2">
              <Label for="item-linked-insulin">Linked insulin</Label>
              <select
                id="item-linked-insulin"
                bind:value={itemLinkedInsulinId}
                class="h-10 w-full rounded-md border bg-background px-3 text-sm"
              >
                <option value={undefined}>None — track {itemKind === InventoryKind.Pod ? "pods" : "reservoirs"} only</option>
                {#each insulinItems as insulin}
                  <option value={insulin.id}>{insulin.name}</option>
                {/each}
              </select>
            </div>
            <div class="space-y-2">
              <Label for="item-linked-units">Units per change</Label>
              <Input
                id="item-linked-units"
                type="number"
                min="0"
                step="1"
                bind:value={itemLinkedUnitsPerUse}
                placeholder={itemKind === InventoryKind.Pod ? "200" : "300"}
              />
            </div>
          </div>
        </div>
      {/if}
    </div>
    <Dialog.Footer class="gap-2">
      {#if editingItem}
        <Button variant="destructive" onclick={archiveCurrentItem} disabled={busyAction === "archive"} class="mr-auto gap-2"
          title="Hide this item from the inventory list. The ledger history is preserved; you can un-archive via the API later.">
          <Archive class="h-4 w-4" />
          Archive
        </Button>
      {/if}
      <Button variant="outline" onclick={() => (itemDialogOpen = false)} disabled={busyAction === "createItem" || busyAction === "updateItem"}>Cancel</Button>
      <Button onclick={submitItem} disabled={!itemName || busyAction === "createItem" || busyAction === "updateItem"} class="gap-2">
        {#if busyAction === "createItem" || busyAction === "updateItem"}
          <Loader2 class="h-4 w-4 animate-spin" />
        {/if}
        {editingItem ? "Save changes" : "Create item"}
      </Button>
    </Dialog.Footer>
  </Dialog.Content>
</Dialog.Root>

<Dialog.Root bind:open={restockDialogOpen}>
  <Dialog.Content class="max-w-lg">
    <Dialog.Header>
      <Dialog.Title>Restock {selectedItem?.name}</Dialog.Title>
      <Dialog.Description>Add a received batch with expiry and storage details.</Dialog.Description>
    </Dialog.Header>
    <div class="grid gap-4 py-4 md:grid-cols-2">
      <div class="space-y-2">
        <Label for="batch-qty">Quantity</Label>
        <Input id="batch-qty" type="number" min="0.01" step="0.01" bind:value={batchQuantity} />
      </div>
      <div class="space-y-2">
        <Label for="batch-received">Received</Label>
        <Input id="batch-received" type="date" bind:value={batchReceived} />
      </div>
      <div class="space-y-2">
        <Label for="batch-expiry">Expiry</Label>
        <Input id="batch-expiry" type="date" bind:value={batchExpires} />
      </div>
      <div class="space-y-2">
        <Label for="batch-lot">Lot number</Label>
        <Input id="batch-lot" bind:value={batchLot} />
      </div>
      <div class="space-y-2 md:col-span-2">
        <Label for="batch-storage">Storage</Label>
        <select id="batch-storage" bind:value={batchStorage} class="h-10 w-full rounded-md border bg-background px-3 text-sm">
          {#each Object.values(InventoryStorageState) as state}
            <option value={state}>{storageLabels[state]}</option>
          {/each}
        </select>
      </div>
      <div class="space-y-2 md:col-span-2">
        <Label for="batch-notes">Notes</Label>
        <Textarea id="batch-notes" rows={3} bind:value={batchNotes} />
      </div>
    </div>
    <Dialog.Footer>
      <Button variant="outline" onclick={() => (restockDialogOpen = false)} disabled={busyAction === "addBatch"}>Cancel</Button>
      <Button onclick={submitAddBatch} disabled={busyAction === "addBatch" || batchQuantity <= 0} class="gap-2">
        {#if busyAction === "addBatch"}
          <Loader2 class="h-4 w-4 animate-spin" />
        {/if}
        Add batch
      </Button>
    </Dialog.Footer>
  </Dialog.Content>
</Dialog.Root>

<Dialog.Root bind:open={consumeDialogOpen}>
  <Dialog.Content class="max-w-md">
    <Dialog.Header>
      <Dialog.Title>Use {selectedItem?.name}</Dialog.Title>
      <Dialog.Description>Manual use follows FEFO unless a batch is selected by an automatic rule.</Dialog.Description>
    </Dialog.Header>
    <div class="space-y-4 py-4">
      <div class="space-y-2">
        <Label for="consume-qty">Quantity</Label>
        <Input id="consume-qty" type="number" min="0.01" step="0.01" bind:value={consumeQuantity} />
      </div>
      <div class="space-y-2">
        <Label for="consume-reason">Reason</Label>
        <Input id="consume-reason" bind:value={consumeReason} />
      </div>
      <div class="space-y-2">
        <Label for="consume-notes">Notes</Label>
        <Textarea id="consume-notes" rows={3} bind:value={consumeNotes} />
      </div>
    </div>
    <Dialog.Footer>
      <Button variant="outline" onclick={() => (consumeDialogOpen = false)}>Cancel</Button>
      <Button onclick={submitConsume} disabled={busyAction === "consume" || consumeQuantity <= 0}>Record use</Button>
    </Dialog.Footer>
  </Dialog.Content>
</Dialog.Root>

<Dialog.Root bind:open={wizardOpen}>
  <Dialog.Content class="max-h-[85vh] sm:max-w-2xl overflow-y-auto">
    <Dialog.Header>
      <Dialog.Title>
        {#if wizardStep === "mode"}Choose your therapy mode
        {:else if wizardStep === "cgm"}Pick your CGM
        {:else if wizardStep === "pump"}Pick your insulin pump
        {:else if wizardStep === "rapid"}Pick your rapid-acting insulin
        {:else if wizardStep === "basal"}Pick your long-acting insulin
        {:else}Review and seed{/if}
      </Dialog.Title>
      <Dialog.Description>
        {#if wizardStep === "mode"}
          Pump users drain insulin via reservoir/pod changes; MDI users drain via every bolus and basal injection.
        {:else if wizardStep === "cgm"}
          Select every CGM you currently use. Choose more than one if you wear redundant sensors.
        {:else if wizardStep === "pump"}
          Pick the pump you use. Hardware (pods, cartridges, infusion sets) will be seeded with auto-consume rules tied to device events.
        {:else if wizardStep === "rapid"}
          Pick your rapid-acting (mealtime) insulin.
        {:else if wizardStep === "basal"}
          Pick your long-acting (basal) insulin.
        {:else}
          Confirm your selection. Generic supplies (strips, lancets, swabs, glucagon, fast carbs, ketone strips) are seeded automatically.
        {/if}
      </Dialog.Description>
    </Dialog.Header>

    <div class="py-4">
      {#if wizardLoadingCatalog}
        <div class="flex items-center justify-center py-12 text-muted-foreground">
          <Loader2 class="mr-2 h-5 w-5 animate-spin" />
          Loading catalog...
        </div>
      {:else if wizardStep === "mode"}
        <div class="space-y-3">
          {#each Object.values(TherapyMode) as mode}
            <label class="flex cursor-pointer items-start gap-3 rounded-md border p-3 hover:bg-accent">
              <input
                type="radio"
                name="wizardTherapyMode"
                value={mode}
                checked={wizardMode === mode}
                onchange={() => wizardSwitchMode(mode)}
                class="mt-1"
              />
              <div class="space-y-1">
                <p class="font-medium">{therapyModeLabels[mode]}</p>
                <p class="text-xs text-muted-foreground">
                  {#if mode === TherapyMode.Pump}
                    Boluses come from the pump reservoir. Bottle drains on pod/reservoir changes.
                  {:else}
                    Every bolus and basal injection draws from the bottle directly.
                  {/if}
                </p>
              </div>
            </label>
          {/each}
        </div>
      {:else if wizardStep === "cgm"}
        <div class="space-y-4">
          {#each wizardCgmGroups as group}
            <div class="space-y-2">
              <h3 class="text-sm font-semibold text-muted-foreground">{group.brand}</h3>
              <div class="space-y-2">
                {#each group.items as entry}
                  <label class="flex cursor-pointer items-start gap-3 rounded-md border p-3 hover:bg-accent">
                    <input
                      type="checkbox"
                      checked={wizardCgmKeys.includes(entry.key ?? "")}
                      onchange={() => toggleCgmSelection(entry.key ?? "")}
                      class="mt-1"
                    />
                    <div class="min-w-0 flex-1 space-y-1">
                      <div class="flex flex-wrap items-center gap-2">
                        <p class="font-medium">{entry.name}</p>
                        {#if entry.isOtc}
                          <Badge variant="outline">OTC</Badge>
                        {/if}
                        {#if entry.isDiscontinued}
                          <Badge variant="secondary">Discontinued</Badge>
                        {/if}
                      </div>
                      {#if entry.notes}
                        <p class="text-xs text-muted-foreground">{entry.notes}</p>
                      {/if}
                    </div>
                  </label>
                {/each}
              </div>
            </div>
          {/each}
        </div>
      {:else if wizardStep === "pump"}
        <div class="space-y-4">
          {#each wizardPumpGroups as group}
            <div class="space-y-2">
              <h3 class="text-sm font-semibold text-muted-foreground">{group.brand}</h3>
              <div class="space-y-2">
                {#each group.items as entry}
                  <label class="flex cursor-pointer items-start gap-3 rounded-md border p-3 hover:bg-accent">
                    <input
                      type="radio"
                      name="wizardPump"
                      checked={wizardPumpKey === entry.key}
                      onchange={() => (wizardPumpKey = entry.key ?? null)}
                      class="mt-1"
                    />
                    <div class="min-w-0 flex-1 space-y-1">
                      <p class="font-medium">{entry.name}</p>
                      {#if entry.notes}
                        <p class="text-xs text-muted-foreground">{entry.notes}</p>
                      {/if}
                    </div>
                  </label>
                {/each}
              </div>
            </div>
          {/each}
        </div>
      {:else if wizardStep === "rapid"}
        <div class="space-y-2">
          {#each wizardRapidEntries as entry}
            <label class="flex cursor-pointer items-start gap-3 rounded-md border p-3 hover:bg-accent">
              <input
                type="radio"
                name="wizardRapid"
                checked={wizardRapidKey === entry.key}
                onchange={() => (wizardRapidKey = entry.key ?? null)}
                class="mt-1"
              />
              <div class="min-w-0 flex-1 space-y-1">
                <div class="flex flex-wrap items-center gap-2">
                  <p class="font-medium">{entry.name}</p>
                  <span class="text-xs text-muted-foreground">{entry.brand}</span>
                </div>
                {#if entry.notes}
                  <p class="text-xs text-muted-foreground">{entry.notes}</p>
                {/if}
              </div>
            </label>
          {/each}
        </div>
      {:else if wizardStep === "basal"}
        <div class="space-y-2">
          {#each wizardBasalEntries as entry}
            <label class="flex cursor-pointer items-start gap-3 rounded-md border p-3 hover:bg-accent">
              <input
                type="radio"
                name="wizardBasal"
                checked={wizardBasalKey === entry.key}
                onchange={() => (wizardBasalKey = entry.key ?? null)}
                class="mt-1"
              />
              <div class="min-w-0 flex-1 space-y-1">
                <div class="flex flex-wrap items-center gap-2">
                  <p class="font-medium">{entry.name}</p>
                  <span class="text-xs text-muted-foreground">{entry.brand}</span>
                </div>
                {#if entry.notes}
                  <p class="text-xs text-muted-foreground">{entry.notes}</p>
                {/if}
              </div>
            </label>
          {/each}
        </div>
      {:else if wizardStep === "confirm"}
        <div class="space-y-4 text-sm">
          <div class="grid gap-1">
            <span class="text-xs uppercase tracking-wide text-muted-foreground">Therapy</span>
            <span class="font-medium">{therapyModeLabels[wizardMode]}</span>
          </div>
          <div class="grid gap-1">
            <span class="text-xs uppercase tracking-wide text-muted-foreground">CGM</span>
            {#if wizardCgmKeys.length === 0}
              <span class="text-muted-foreground">None selected</span>
            {:else}
              <ul class="list-disc space-y-1 pl-5">
                {#each wizardCgmKeys as key}
                  {@const entry = findCatalogEntry(key)}
                  {#if entry}
                    <li>{entry.brand} — {entry.name}</li>
                  {/if}
                {/each}
              </ul>
            {/if}
          </div>
          {#if wizardMode === TherapyMode.Pump}
            <div class="grid gap-1">
              <span class="text-xs uppercase tracking-wide text-muted-foreground">Pump</span>
              <span class="font-medium">{wizardPumpEntry ? `${wizardPumpEntry.brand} — ${wizardPumpEntry.name}` : "None selected"}</span>
            </div>
          {/if}
          <div class="grid gap-1">
            <span class="text-xs uppercase tracking-wide text-muted-foreground">Rapid-acting insulin</span>
            <span class="font-medium">{wizardRapidEntry ? `${wizardRapidEntry.brand} — ${wizardRapidEntry.name}` : "None selected"}</span>
          </div>
          {#if wizardMode === TherapyMode.Mdi}
            <div class="grid gap-1">
              <span class="text-xs uppercase tracking-wide text-muted-foreground">Long-acting insulin</span>
              <span class="font-medium">{wizardBasalEntry ? `${wizardBasalEntry.brand} — ${wizardBasalEntry.name}` : "None selected"}</span>
            </div>
          {/if}
          <p class="rounded-md border bg-muted/40 p-3 text-xs text-muted-foreground">
            Generic supplies (test strips, lancets, alcohol swabs, control solution, glucagon, fast carbs, ketone strips) will be seeded automatically. Already-existing items are skipped.
          </p>
        </div>
      {/if}
    </div>

    <Dialog.Footer class="flex-row justify-between sm:justify-between">
      <div>
        {#if wizardStep !== "mode"}
          <Button variant="outline" onclick={wizardPrevStep} disabled={busyAction === "seed"}>Back</Button>
        {/if}
      </div>
      <div class="flex gap-2">
        <Button variant="ghost" onclick={() => (wizardOpen = false)} disabled={busyAction === "seed"}>Cancel</Button>
        {#if wizardStep === "confirm"}
          <Button onclick={wizardSubmit} disabled={busyAction === "seed"} class="gap-2">
            {#if busyAction === "seed"}
              <Loader2 class="h-4 w-4 animate-spin" />
            {/if}
            Seed inventory
          </Button>
        {:else}
          <Button onclick={wizardNextStep} disabled={!wizardCanAdvance || wizardLoadingCatalog}>Next</Button>
        {/if}
      </div>
    </Dialog.Footer>
  </Dialog.Content>
</Dialog.Root>
