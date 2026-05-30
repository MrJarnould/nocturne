<script lang="ts">
  import { page } from "$app/state";
  import { goto } from "$app/navigation";
  import { Button } from "$lib/components/ui/button";
  import { Badge } from "$lib/components/ui/badge";
  import * as Card from "$lib/components/ui/card";
  import * as Command from "$lib/components/ui/command";
  import * as Dialog from "$lib/components/ui/dialog";
  import { Input } from "$lib/components/ui/input";
  import { Label } from "$lib/components/ui/label";
  import * as Popover from "$lib/components/ui/popover";
  import { Textarea } from "$lib/components/ui/textarea";
  import * as inventoryRemote from "$api/generated/inventories.generated.remote";
  import {
    DeviceEventType,
    InventoryAutoConsumeSource,
    InventoryCategory,
    InventoryKind,
    InventoryStorageState,
    InventoryTransactionType,
    type InventoryBatchDto,
    type InventoryItemDetailDto,
    type InventoryItemDto,
  } from "$api";
  import { DEVICE_EVENT_TYPE_LABELS, DEVICE_EVENT_TYPES } from "$lib/constants/device-event-types";
  import {
    Archive,
    ArrowLeft,
    Check,
    ChevronsUpDown,
    Loader2,
    PackagePlus,
    RotateCcw,
    Settings2,
    Trash2,
  } from "lucide-svelte";
  import { cn } from "$lib/utils";

  const itemId = $derived(page.params.id as string);
  const itemQuery = $derived(inventoryRemote.getItem(itemId));
  const item = $derived<InventoryItemDetailDto | undefined>(itemQuery.current ?? undefined);

  const itemsQuery = inventoryRemote.getItems(undefined);
  const insulinItems = $derived<InventoryItemDto[]>(
    (itemsQuery.current ?? []).filter((i) => i.kind === InventoryKind.Insulin && !i.isArchived),
  );

  let busyAction = $state<string | null>(null);

  // Edit item dialog state
  let editDialogOpen = $state(false);
  let itemName = $state("");
  let itemCategory = $state<InventoryCategory>(InventoryCategory.Cgm);
  let itemKind = $state<InventoryKind>(InventoryKind.Custom);
  let itemUnit = $state("each");
  let itemThreshold = $state(1);
  let itemTarget = $state<number | undefined>(undefined);
  let itemAutoSource = $state<InventoryAutoConsumeSource>(InventoryAutoConsumeSource.None);
  let itemDeviceEvents = $state<DeviceEventType[]>([]);
  let eventPickerOpen = $state(false);
  let itemLinkedInsulinId = $state<string | undefined>(undefined);
  let itemLinkedUnitsPerUse = $state<number | undefined>(undefined);
  const showLinkedInsulin = $derived(itemKind === InventoryKind.Pod || itemKind === InventoryKind.Reservoir);

  // Restock dialog state
  let restockDialogOpen = $state(false);
  let batchQuantity = $state(1);
  let batchReceived = $state("");
  let batchExpires = $state("");
  let batchLot = $state("");
  let batchStorage = $state<InventoryStorageState>(InventoryStorageState.Normal);
  let batchNotes = $state("");

  // Use dialog state
  let consumeDialogOpen = $state(false);
  let consumeQuantity = $state(1);
  let consumeReason = $state("Manual use");
  let consumeNotes = $state("");

  // Adjust dialog state
  let adjustDialogOpen = $state(false);
  let selectedBatch = $state<InventoryBatchDto | null>(null);
  let adjustQuantity = $state(0);
  let adjustReason = $state("Stock correction");
  let adjustNotes = $state("");

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

  const transactionLabels: Record<InventoryTransactionType, string> = {
    [InventoryTransactionType.Restock]: "Restock",
    [InventoryTransactionType.ManualConsume]: "Manual use",
    [InventoryTransactionType.AutoConsume]: "Automatic use",
    [InventoryTransactionType.Adjustment]: "Adjustment",
    [InventoryTransactionType.Reversal]: "Reversal",
    [InventoryTransactionType.Expired]: "Expired",
  };

  function openEdit() {
    if (!item) return;
    itemName = item.name ?? "";
    itemCategory = item.category ?? InventoryCategory.Other;
    itemKind = item.kind ?? InventoryKind.Custom;
    itemUnit = item.unitLabel ?? "each";
    itemThreshold = item.lowStockThreshold ?? 1;
    itemTarget = item.targetStock ?? undefined;
    itemAutoSource = item.autoConsumeSource ?? InventoryAutoConsumeSource.None;
    itemDeviceEvents = (item.deviceEventTypes ?? []) as DeviceEventType[];
    itemLinkedInsulinId = item.linkedInsulinItemId ?? undefined;
    itemLinkedUnitsPerUse = item.linkedInsulinUnitsPerUse ?? undefined;
    editDialogOpen = true;
  }

  function openRestock() {
    if (!item) return;
    batchQuantity = item.suggestedRestockQuantity || 1;
    batchReceived = dateInputValue(new Date());
    batchExpires = "";
    batchLot = "";
    batchStorage = InventoryStorageState.Normal;
    batchNotes = "";
    restockDialogOpen = true;
  }

  function openConsume() {
    consumeQuantity = 1;
    consumeReason = "Manual use";
    consumeNotes = "";
    consumeDialogOpen = true;
  }

  function openAdjust(batch: InventoryBatchDto) {
    selectedBatch = batch;
    adjustQuantity = batch.remainingQuantity ?? 0;
    adjustReason = "Stock correction";
    adjustNotes = "";
    adjustDialogOpen = true;
  }

  async function submitEdit() {
    if (!item?.id) return;
    busyAction = "updateItem";
    try {
      await inventoryRemote.updateItem({
        id: item.id,
        request: {
          name: itemName,
          category: itemCategory,
          kind: itemKind,
          unitLabel: itemUnit,
          lowStockThreshold: itemThreshold,
          targetStock: itemTarget,
          autoConsumeEnabled: itemAutoSource !== InventoryAutoConsumeSource.None,
          autoConsumeSource: itemAutoSource,
          deviceEventTypes: itemDeviceEvents,
          linkedInsulinItemId: showLinkedInsulin ? itemLinkedInsulinId : undefined,
          linkedInsulinUnitsPerUse: showLinkedInsulin ? itemLinkedUnitsPerUse : undefined,
        },
      });
      editDialogOpen = false;
    } finally {
      busyAction = null;
    }
  }

  async function archiveItem() {
    if (!item?.id) return;
    busyAction = "archive";
    try {
      await inventoryRemote.archiveItem(item.id);
      editDialogOpen = false;
      goto("/inventory/overview");
    } finally {
      busyAction = null;
    }
  }

  async function submitRestock() {
    if (!item?.id) return;
    busyAction = "addBatch";
    try {
      await inventoryRemote.addBatch({
        itemId: item.id,
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
    } finally {
      busyAction = null;
    }
  }

  async function submitConsume() {
    if (!item?.id) return;
    busyAction = "consume";
    try {
      await inventoryRemote.consume({
        itemId: item.id,
        request: {
          quantity: consumeQuantity,
          reason: consumeReason || undefined,
          notes: consumeNotes || undefined,
        },
      });
      consumeDialogOpen = false;
    } finally {
      busyAction = null;
    }
  }

  async function submitAdjust() {
    if (!item?.id || !selectedBatch?.id) return;
    busyAction = "adjust";
    try {
      await inventoryRemote.adjustBatch({
        batchId: selectedBatch.id,
        request: {
          remainingQuantity: adjustQuantity,
          reason: adjustReason || undefined,
          notes: adjustNotes || undefined,
        },
      });
      adjustDialogOpen = false;
    } finally {
      busyAction = null;
    }
  }

  async function expireBatch(batch: InventoryBatchDto) {
    if (!item?.id || !batch.id) return;
    busyAction = `expire-${batch.id}`;
    try {
      await inventoryRemote.transferBatchToExpired({
        batchId: batch.id,
        request: { notes: "Marked expired from inventory settings" },
      });
    } finally {
      busyAction = null;
    }
  }

  function displayNumber(value: number | undefined): string {
    return (value ?? 0).toLocaleString(undefined, { maximumFractionDigits: 2 });
  }

  function formatDate(value: Date | string | null | undefined): string {
    if (!value) return "None";
    return new Date(value).toLocaleDateString(undefined, { month: "short", day: "numeric", year: "numeric" });
  }

  function dateInputValue(value: Date | string | undefined): string {
    if (!value) return "";
    return new Date(value).toISOString().slice(0, 10);
  }

  const EXPIRING_SOON_MS = 30 * 24 * 60 * 60 * 1000;
  function isExpiringSoon(value: Date | string | null | undefined): boolean {
    if (!value) return false;
    const ms = new Date(value).getTime() - Date.now();
    return ms > 0 && ms < EXPIRING_SOON_MS;
  }

  function toggleDeviceEvent(type: DeviceEventType) {
    if (itemDeviceEvents.includes(type)) {
      itemDeviceEvents = itemDeviceEvents.filter((t) => t !== type);
    } else {
      itemDeviceEvents = [...itemDeviceEvents, type];
    }
  }
</script>

<svelte:head>
  <title>{item?.name ?? "Item"} - Inventory - Settings - Nocturne</title>
</svelte:head>

<div class="container mx-auto max-w-7xl space-y-6 p-4 md:p-6">
  <!-- Header -->
  <div class="flex items-start justify-between gap-3">
    <div class="flex items-center gap-3">
      <Button variant="ghost" size="icon" onclick={() => goto("/inventory/overview")} title="Back to inventory">
        <ArrowLeft class="h-5 w-5" />
      </Button>
      <div>
        {#if item}
          <h1 class="text-3xl font-bold tracking-tight">{item.name}</h1>
          <p class="text-muted-foreground">{kindLabels[item.kind ?? InventoryKind.Custom]}</p>
        {:else}
          <div class="h-9 w-48 animate-pulse rounded-md bg-muted"></div>
          <div class="mt-1 h-5 w-32 animate-pulse rounded-md bg-muted"></div>
        {/if}
      </div>
    </div>
    <Button variant="ghost" size="icon" onclick={openEdit} title="Edit item settings" disabled={!item}>
      <Settings2 class="h-5 w-5" />
    </Button>
  </div>

  {#if item}
    <!-- Batches section -->
    <section class="space-y-4">
      <div class="grid gap-3 sm:grid-cols-4">
        <Card.Root><Card.Content class="p-4"><p class="text-sm text-muted-foreground">Available</p><p class="text-2xl font-semibold">{displayNumber(item.usableStock)}</p></Card.Content></Card.Root>
        <Card.Root><Card.Content class="p-4"><p class="text-sm text-muted-foreground">Expired</p><p class="text-2xl font-semibold">{displayNumber(item.expiredStock)}</p></Card.Content></Card.Root>
        <Card.Root><Card.Content class="p-4"><p class="text-sm text-muted-foreground">Threshold</p><p class="text-2xl font-semibold">{displayNumber(item.lowStockThreshold)}</p></Card.Content></Card.Root>
        <Card.Root><Card.Content class="p-4"><p class="text-sm text-muted-foreground">Restock</p><p class="text-2xl font-semibold">{displayNumber(item.suggestedRestockQuantity)}</p></Card.Content></Card.Root>
      </div>

      <div class="flex gap-2">
        <Button size="sm" onclick={openRestock} class="gap-2" title="Add a newly-received batch">
          <PackagePlus class="h-4 w-4" />
          Restock
        </Button>
        <Button size="sm" variant="outline" onclick={openConsume} class="gap-2" title="Manually log consumption (FEFO)">
          <RotateCcw class="h-4 w-4" />
          Use
        </Button>
      </div>

      <div class="overflow-x-auto rounded-md border">
        <table class="w-full min-w-160 text-sm">
          <thead class="bg-muted/50 text-left">
            <tr>
              <th class="p-3">Remaining</th>
              <th class="p-3">Received</th>
              <th class="p-3">Expiry</th>
              <th class="p-3">Lot</th>
              <th class="p-3">Storage</th>
              <th class="p-3"></th>
            </tr>
          </thead>
          <tbody>
            {#each item.batches ?? [] as batch}
              <tr class="border-t">
                <td class="p-3 font-medium">{displayNumber(batch.remainingQuantity)} / {displayNumber(batch.receivedQuantity)}</td>
                <td class="p-3">{formatDate(batch.receivedAt)}</td>
                <td class="p-3">
                  <span>{formatDate(batch.expiresAt)}</span>
                  {#if batch.isExpired}<Badge variant="destructive" class="ml-2">Expired</Badge>{:else if isExpiringSoon(batch.expiresAt)}<Badge variant="outline" class="ml-2">Soon</Badge>{/if}
                </td>
                <td class="p-3">{batch.lotNumber ?? ""}</td>
                <td class="p-3">{storageLabels[batch.storageState ?? InventoryStorageState.Normal]}</td>
                <td class="p-3 text-right">
                  <Button size="sm" variant="ghost" onclick={() => openAdjust(batch)} class="gap-2"
                    title="Override this batch's remaining quantity. Writes an Adjustment row to the ledger.">
                    <Settings2 class="h-4 w-4" />
                    Adjust
                  </Button>
                  <Button size="sm" variant="ghost" onclick={() => expireBatch(batch)} disabled={busyAction === `expire-${batch.id}`}
                    title="Transition this batch out of usable stock.">
                    <Trash2 class="h-4 w-4" />
                    Expire
                  </Button>
                </td>
              </tr>
            {/each}
            {#if (item.batches ?? []).length === 0}
              <tr><td colspan="6" class="p-6 text-center text-muted-foreground">No batches yet — restock to add one.</td></tr>
            {/if}
          </tbody>
        </table>
      </div>
    </section>

    <!-- Ledger section -->
    <section class="space-y-3">
      <h2 class="text-lg font-semibold">Ledger</h2>
      <div class="overflow-x-auto rounded-md border">
        <table class="w-full min-w-180 text-sm">
          <thead class="bg-muted/50 text-left">
            <tr>
              <th class="p-3">Date</th>
              <th class="p-3">Type</th>
              <th class="p-3">Change</th>
              <th class="p-3">After</th>
              <th class="p-3">Reason</th>
              <th class="p-3">Source</th>
            </tr>
          </thead>
          <tbody>
            {#each item.transactions ?? [] as transaction}
              <tr class="border-t">
                <td class="p-3">{formatDate(transaction.createdAt)}</td>
                <td class="p-3">{transactionLabels[transaction.type ?? InventoryTransactionType.Adjustment]}</td>
                <td class="p-3">{displayNumber(transaction.quantityDelta)}</td>
                <td class="p-3">{displayNumber(transaction.quantityAfter)}</td>
                <td class="p-3">{transaction.reason ?? ""}</td>
                <td class="p-3">{transaction.sourceType ?? ""}</td>
              </tr>
            {/each}
            {#if (item.transactions ?? []).length === 0}
              <tr><td colspan="6" class="p-6 text-center text-muted-foreground">No transactions yet.</td></tr>
            {/if}
          </tbody>
        </table>
      </div>
    </section>
  {/if}
</div>

<!-- Edit settings dialog -->
<Dialog.Root bind:open={editDialogOpen}>
  <Dialog.Content class="sm:max-w-2xl">
    <Dialog.Header>
      <Dialog.Title>Edit {item?.name}</Dialog.Title>
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
        <Label>Device event mappings</Label>
        <Popover.Root bind:open={eventPickerOpen}>
          <Popover.Trigger class="w-full">
            {#snippet child({ props })}
              <Button
                variant="outline"
                class="w-full justify-between font-normal"
                {...props}
                role="combobox"
                aria-expanded={eventPickerOpen}
              >
                <span class={itemDeviceEvents.length === 0 ? "text-muted-foreground" : ""}>
                  {itemDeviceEvents.length === 0
                    ? "Select event types…"
                    : itemDeviceEvents.map((t) => DEVICE_EVENT_TYPE_LABELS[t]).join(", ")}
                </span>
                <ChevronsUpDown class="ml-2 h-4 w-4 shrink-0 opacity-50" />
              </Button>
            {/snippet}
          </Popover.Trigger>
          <Popover.Content class="w-(--bits-popover-anchor-width) p-0">
            <Command.Root>
              <Command.Input placeholder="Search event types…" />
              <Command.List>
                <Command.Empty>No event types found.</Command.Empty>
                <Command.Group>
                  {#each DEVICE_EVENT_TYPES as eventType}
                    <Command.Item
                      value={eventType}
                      onSelect={() => toggleDeviceEvent(eventType)}
                    >
                      <Check class={cn("mr-2 h-4 w-4", !itemDeviceEvents.includes(eventType) && "text-transparent")} />
                      {DEVICE_EVENT_TYPE_LABELS[eventType]}
                    </Command.Item>
                  {/each}
                </Command.Group>
              </Command.List>
            </Command.Root>
          </Popover.Content>
        </Popover.Root>
      </div>
      {#if showLinkedInsulin}
        <div class="space-y-2 md:col-span-2 rounded-md border border-dashed p-3">
          <p class="text-xs text-muted-foreground">
            For pump users: each change of this {itemKind === InventoryKind.Pod ? "pod" : "reservoir"} also drains the linked insulin bottle.
          </p>
          <div class="grid gap-3 md:grid-cols-2">
            <div class="space-y-2">
              <Label for="item-linked-insulin">Linked insulin</Label>
              <select id="item-linked-insulin" bind:value={itemLinkedInsulinId} class="h-10 w-full rounded-md border bg-background px-3 text-sm">
                <option value={undefined}>None — track {itemKind === InventoryKind.Pod ? "pods" : "reservoirs"} only</option>
                {#each insulinItems as insulin}
                  <option value={insulin.id}>{insulin.name}</option>
                {/each}
              </select>
            </div>
            <div class="space-y-2">
              <Label for="item-linked-units">Units per change</Label>
              <Input id="item-linked-units" type="number" min="0" step="1" bind:value={itemLinkedUnitsPerUse}
                placeholder={itemKind === InventoryKind.Pod ? "200" : "300"} />
            </div>
          </div>
        </div>
      {/if}
    </div>
    <Dialog.Footer class="gap-2">
      <Button variant="destructive" onclick={archiveItem} disabled={busyAction === "archive"} class="mr-auto gap-2"
        title="Hide this item from the inventory list. Ledger history is preserved.">
        <Archive class="h-4 w-4" />
        Archive
      </Button>
      <Button variant="outline" onclick={() => (editDialogOpen = false)} disabled={busyAction === "updateItem"}>Cancel</Button>
      <Button onclick={submitEdit} disabled={!itemName || busyAction === "updateItem"} class="gap-2">
        {#if busyAction === "updateItem"}
          <Loader2 class="h-4 w-4 animate-spin" />
        {/if}
        Save changes
      </Button>
    </Dialog.Footer>
  </Dialog.Content>
</Dialog.Root>

<!-- Restock dialog -->
<Dialog.Root bind:open={restockDialogOpen}>
  <Dialog.Content class="max-w-lg">
    <Dialog.Header>
      <Dialog.Title>Restock {item?.name}</Dialog.Title>
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
      <Button onclick={submitRestock} disabled={busyAction === "addBatch" || batchQuantity <= 0} class="gap-2">
        {#if busyAction === "addBatch"}
          <Loader2 class="h-4 w-4 animate-spin" />
        {/if}
        Add batch
      </Button>
    </Dialog.Footer>
  </Dialog.Content>
</Dialog.Root>

<!-- Use dialog -->
<Dialog.Root bind:open={consumeDialogOpen}>
  <Dialog.Content class="max-w-md">
    <Dialog.Header>
      <Dialog.Title>Use {item?.name}</Dialog.Title>
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

<!-- Adjust batch dialog -->
<Dialog.Root bind:open={adjustDialogOpen}>
  <Dialog.Content class="max-w-md">
    <Dialog.Header>
      <Dialog.Title>Adjust batch</Dialog.Title>
      <Dialog.Description>Correct the remaining stock and keep an audit note.</Dialog.Description>
    </Dialog.Header>
    <div class="space-y-4 py-4">
      <div class="space-y-2">
        <Label for="adjust-qty">New remaining quantity</Label>
        <Input id="adjust-qty" type="number" min="0" step="0.01" bind:value={adjustQuantity} />
      </div>
      <div class="space-y-2">
        <Label for="adjust-reason">Reason</Label>
        <Input id="adjust-reason" bind:value={adjustReason} />
      </div>
      <div class="space-y-2">
        <Label for="adjust-notes">Notes</Label>
        <Textarea id="adjust-notes" rows={3} bind:value={adjustNotes} />
      </div>
    </div>
    <Dialog.Footer>
      <Button variant="outline" onclick={() => (adjustDialogOpen = false)}>Cancel</Button>
      <Button onclick={submitAdjust} disabled={busyAction === "adjust" || adjustQuantity < 0}>Save adjustment</Button>
    </Dialog.Footer>
  </Dialog.Content>
</Dialog.Root>
