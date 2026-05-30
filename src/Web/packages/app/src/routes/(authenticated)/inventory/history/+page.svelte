<script lang="ts">
  import { goto } from "$app/navigation";
  import { Button } from "$lib/components/ui/button";
  import { Badge } from "$lib/components/ui/badge";
  import * as Card from "$lib/components/ui/card";
  import * as inventoryRemote from "$api/generated/inventories.generated.remote";
  import {
    InventoryStorageState,
    InventoryTransactionType,
    type InventoryExpiringBatchDto,
    type InventoryTransactionWithItemDto,
  } from "$api";
  import { ArrowUpRight, CalendarClock, History } from "lucide-svelte";

  // Expiring batches — always load all (within 30-day default)
  const expiringQuery = inventoryRemote.getExpiringBatches(undefined);
  const expiring = $derived<InventoryExpiringBatchDto[]>(expiringQuery.current ?? []);

  // Global transactions — filterable by type
  type Filter = "all" | InventoryTransactionType;
  let activeFilter = $state<Filter>("all");

  const transactionsQuery = $derived(
    inventoryRemote.getAllTransactions(
      activeFilter === "all" ? undefined : { type: activeFilter as InventoryTransactionType },
    ),
  );
  const transactions = $derived<InventoryTransactionWithItemDto[]>(
    transactionsQuery.current ?? [],
  );

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

  const filters: { label: string; value: Filter }[] = [
    { label: "All", value: "all" },
    { label: "Expired", value: InventoryTransactionType.Expired },
    { label: "Manual use", value: InventoryTransactionType.ManualConsume },
    { label: "Auto use", value: InventoryTransactionType.AutoConsume },
    { label: "Restock", value: InventoryTransactionType.Restock },
    { label: "Adjustment", value: InventoryTransactionType.Adjustment },
    { label: "Reversal", value: InventoryTransactionType.Reversal },
  ];

  const EXPIRING_SOON_MS = 7 * 24 * 60 * 60 * 1000;
  function isExpiringSoon(value: Date | string | null | undefined): boolean {
    if (!value) return false;
    const ms = new Date(value).getTime() - Date.now();
    return ms > 0 && ms < EXPIRING_SOON_MS;
  }

  function isExpired(value: Date | string | null | undefined): boolean {
    if (!value) return false;
    return new Date(value).getTime() < Date.now();
  }

  function formatDate(value: Date | string | null | undefined): string {
    if (!value) return "—";
    return new Date(value).toLocaleDateString(undefined, {
      month: "short",
      day: "numeric",
      year: "numeric",
    });
  }

  function displayNumber(value: number | undefined): string {
    return (value ?? 0).toLocaleString(undefined, { maximumFractionDigits: 2 });
  }
</script>

<svelte:head>
  <title>Inventory History - Nocturne</title>
</svelte:head>

<div class="container mx-auto max-w-7xl space-y-8 p-4 md:p-6">
  <div>
    <h1 class="text-3xl font-bold tracking-tight">Inventory History</h1>
    <p class="text-muted-foreground">Upcoming expirations and a global transaction ledger across all items.</p>
  </div>

  <!-- Upcoming expirations -->
  <section class="space-y-4">
    <div class="flex items-center gap-2">
      <CalendarClock class="h-5 w-5 text-muted-foreground" />
      <h2 class="text-xl font-semibold">Upcoming expirations</h2>
      {#if expiring.length > 0}
        <Badge variant="outline">{expiring.length}</Badge>
      {/if}
    </div>

    {#if expiringQuery.current === undefined}
      <div class="h-24 animate-pulse rounded-md bg-muted"></div>
    {:else if expiring.length === 0}
      <Card.Root>
        <Card.Content class="flex items-center justify-center py-10 text-muted-foreground text-sm">
          No batches expiring within the next 30 days.
        </Card.Content>
      </Card.Root>
    {:else}
      <div class="overflow-x-auto rounded-md border">
        <table class="w-full min-w-160 text-sm">
          <thead class="bg-muted/50 text-left">
            <tr>
              <th class="p-3">Item</th>
              <th class="p-3">Remaining</th>
              <th class="p-3">Expires</th>
              <th class="p-3">Lot</th>
              <th class="p-3">Storage</th>
              <th class="p-3"></th>
            </tr>
          </thead>
          <tbody>
            {#each expiring as batch}
              <tr class="border-t">
                <td class="p-3 font-medium">{batch.itemName}</td>
                <td class="p-3">{displayNumber(batch.remainingQuantity)}</td>
                <td class="p-3">
                  <span>{formatDate(batch.expiresAt)}</span>
                  {#if isExpired(batch.expiresAt)}
                    <Badge variant="destructive" class="ml-2">Expired</Badge>
                  {:else if isExpiringSoon(batch.expiresAt)}
                    <Badge variant="outline" class="ml-2 border-orange-400 text-orange-600">Soon</Badge>
                  {/if}
                </td>
                <td class="p-3 text-muted-foreground">{batch.lotNumber ?? "—"}</td>
                <td class="p-3 text-muted-foreground">{storageLabels[batch.storageState ?? InventoryStorageState.Normal]}</td>
                <td class="p-3 text-right">
                  <Button
                    size="sm"
                    variant="ghost"
                    onclick={() => goto(`/inventory/${batch.inventoryItemId}`)}
                    class="gap-1.5"
                  >
                    <ArrowUpRight class="h-3.5 w-3.5" />
                    Details
                  </Button>
                </td>
              </tr>
            {/each}
          </tbody>
        </table>
      </div>
    {/if}
  </section>

  <!-- Global ledger -->
  <section class="space-y-4">
    <div class="flex items-center gap-2">
      <History class="h-5 w-5 text-muted-foreground" />
      <h2 class="text-xl font-semibold">Transaction ledger</h2>
    </div>

    <!-- Filter chips -->
    <div class="flex flex-wrap gap-2">
      {#each filters as f}
        <button
          onclick={() => (activeFilter = f.value)}
          class="rounded-full border px-3 py-1 text-sm transition-colors {activeFilter === f.value
            ? 'border-primary bg-primary text-primary-foreground'
            : 'border-border bg-background hover:bg-muted'}"
        >
          {f.label}
        </button>
      {/each}
    </div>

    {#if transactionsQuery.current === undefined}
      <div class="h-32 animate-pulse rounded-md bg-muted"></div>
    {:else if transactions.length === 0}
      <Card.Root>
        <Card.Content class="flex items-center justify-center py-10 text-muted-foreground text-sm">
          No transactions found.
        </Card.Content>
      </Card.Root>
    {:else}
      <div class="overflow-x-auto rounded-md border">
        <table class="w-full min-w-180 text-sm">
          <thead class="bg-muted/50 text-left">
            <tr>
              <th class="p-3">Date</th>
              <th class="p-3">Item</th>
              <th class="p-3">Type</th>
              <th class="p-3">Change</th>
              <th class="p-3">After</th>
              <th class="p-3">Reason</th>
              <th class="p-3"></th>
            </tr>
          </thead>
          <tbody>
            {#each transactions as tx}
              <tr class="border-t">
                <td class="p-3 text-muted-foreground">{formatDate(tx.createdAt)}</td>
                <td class="p-3 font-medium">{tx.itemName}</td>
                <td class="p-3">{transactionLabels[tx.type ?? InventoryTransactionType.Adjustment]}</td>
                <td class="p-3">{displayNumber(tx.quantityDelta)}</td>
                <td class="p-3">{displayNumber(tx.quantityAfter)}</td>
                <td class="p-3 text-muted-foreground">{tx.reason ?? ""}</td>
                <td class="p-3 text-right">
                  <Button
                    size="sm"
                    variant="ghost"
                    onclick={() => goto(`/inventory/${tx.inventoryItemId}`)}
                    class="gap-1.5"
                  >
                    <ArrowUpRight class="h-3.5 w-3.5" />
                    Item
                  </Button>
                </td>
              </tr>
            {/each}
          </tbody>
        </table>
      </div>
    {/if}
  </section>
</div>
