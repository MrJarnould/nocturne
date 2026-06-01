<script lang="ts">
  import { Button } from "$lib/components/ui/button";
  import { Input } from "$lib/components/ui/input";
  import { Label } from "$lib/components/ui/label";
  import { Textarea } from "$lib/components/ui/textarea";
  import { Badge } from "$lib/components/ui/badge";
  import * as Dialog from "$lib/components/ui/dialog";
  import * as AlertDialog from "$lib/components/ui/alert-dialog";
  import { Activity, Pill, Plug, Loader2, Pencil, Trash2 } from "lucide-svelte";
  import {
    type ConsumableInstance,
    ConsumableKind,
    ConsumableInstanceEndReason,
  } from "$api";
  import * as remote from "$api/generated/consumableInstances.generated.remote";

  const active = remote.getActive();
  const recent = remote.getRecentClosed();

  // Phase 1 only opens wear sessions for these three kinds; rendering by these
  // matches what the DeviceEvent hook can actually produce.
  const KINDS_IN_USE = [
    ConsumableKind.CgmSensor,
    ConsumableKind.InfusionSet,
    ConsumableKind.Pod,
  ] as const;

  const KIND_LABELS: Record<ConsumableKind, string> = {
    [ConsumableKind.CgmSensor]: "CGM sensor",
    [ConsumableKind.CgmTransmitter]: "CGM transmitter",
    [ConsumableKind.Pod]: "Pod",
    [ConsumableKind.Reservoir]: "Reservoir",
    [ConsumableKind.InfusionSet]: "Infusion set",
    [ConsumableKind.Cannula]: "Cannula",
  };

  const KIND_ICONS: Record<ConsumableKind, typeof Activity> = {
    [ConsumableKind.CgmSensor]: Activity,
    [ConsumableKind.CgmTransmitter]: Activity,
    [ConsumableKind.Pod]: Pill,
    [ConsumableKind.Reservoir]: Pill,
    [ConsumableKind.InfusionSet]: Plug,
    [ConsumableKind.Cannula]: Plug,
  };

  const END_REASON_LABELS: Record<ConsumableInstanceEndReason, string> = {
    [ConsumableInstanceEndReason.Planned]: "Planned change",
    [ConsumableInstanceEndReason.Occlusion]: "Occlusion",
    [ConsumableInstanceEndReason.Failure]: "Failure",
    [ConsumableInstanceEndReason.FellOff]: "Fell off",
    [ConsumableInstanceEndReason.Unknown]: "Unknown",
  };

  const activeByKind = $derived.by(() => {
    const result = new Map<ConsumableKind, ConsumableInstance>();
    for (const inst of active.current ?? []) {
      if (inst.kind !== undefined) result.set(inst.kind, inst);
    }
    return result;
  });

  function formatDateTime(value: Date | string | undefined): string {
    if (!value) return "—";
    const d = new Date(value);
    return d.toLocaleString(undefined, {
      year: "numeric",
      month: "short",
      day: "numeric",
      hour: "numeric",
      minute: "2-digit",
    });
  }

  function hoursBetween(start: Date | string | undefined, end: Date | string | undefined): number | null {
    if (!start) return null;
    const startMs = new Date(start).getTime();
    const endMs = end ? new Date(end).getTime() : Date.now();
    return Math.max(0, (endMs - startMs) / (1000 * 60 * 60));
  }

  function formatDuration(start: Date | string | undefined, end: Date | string | undefined): string {
    const hours = hoursBetween(start, end);
    if (hours === null) return "—";
    if (hours < 24) return `${hours.toFixed(1)} h`;
    const days = hours / 24;
    return `${days.toFixed(1)} d`;
  }

  // ── Edit dialog ───────────────────────────────────────────────────

  let editTarget = $state<ConsumableInstance | null>(null);
  let editOpen = $state(false);
  let editEndingNow = $state(false);

  function openEdit(inst: ConsumableInstance) {
    editTarget = inst;
    editEndingNow = false;
    editOpen = true;
  }

  function openEnd(inst: ConsumableInstance) {
    editTarget = inst;
    editEndingNow = true;
    editOpen = true;
  }

  // ── Delete confirm ────────────────────────────────────────────────

  let deleteTarget = $state<ConsumableInstance | null>(null);
  let deletePending = $state(false);

  async function confirmDelete() {
    if (!deleteTarget?.id) return;
    deletePending = true;
    try {
      await remote.remove(deleteTarget.id);
    } finally {
      deletePending = false;
      deleteTarget = null;
    }
  }
</script>

<div class="space-y-6">
  <!-- Active wear sessions -->
  <div class="grid gap-3 @md:grid-cols-2 @lg:grid-cols-3">
    {#each KINDS_IN_USE as kind}
      {@const inst = activeByKind.get(kind)}
      {@const Icon = KIND_ICONS[kind]}
      <div class="rounded-lg border bg-card p-4 space-y-2">
        <div class="flex items-center gap-2">
          <Icon class="h-4 w-4 text-muted-foreground" />
          <h4 class="font-medium">{KIND_LABELS[kind]}</h4>
        </div>
        {#if inst}
          <div class="text-sm space-y-1">
            <div>
              <span class="text-muted-foreground">Started:</span>
              {formatDateTime(inst.startedAt)}
            </div>
            <div>
              <span class="text-muted-foreground">Worn:</span>
              {formatDuration(inst.startedAt, undefined)}
              {#if inst.snapshotWearDays != null}
                <span class="text-muted-foreground">/ {inst.snapshotWearDays} d</span>
              {/if}
            </div>
            {#if inst.insertionSite}
              <div>
                <span class="text-muted-foreground">Site:</span>
                {inst.insertionSite}
              </div>
            {/if}
          </div>
          <div class="flex gap-2 pt-1">
            <Button size="sm" variant="outline" onclick={() => openEdit(inst)}>
              <Pencil class="mr-1 h-3 w-3" /> Edit
            </Button>
            <Button size="sm" variant="outline" onclick={() => openEnd(inst)}>
              End now
            </Button>
          </div>
        {:else}
          <p class="text-sm text-muted-foreground">No active session.</p>
        {/if}
      </div>
    {/each}
  </div>

  <!-- Recently closed -->
  <div>
    <h4 class="font-medium mb-2">Recent wear sessions</h4>
    {#if (recent.current ?? []).length === 0}
      <p class="text-sm text-muted-foreground">No closed sessions yet.</p>
    {:else}
      <div class="rounded-lg border divide-y">
        {#each recent.current ?? [] as inst}
          {@const kind = inst.kind}
          <div class="p-3 flex items-start justify-between gap-3">
            <div class="text-sm space-y-1 min-w-0">
              <div class="flex items-center gap-2">
                <span class="font-medium">{kind ? KIND_LABELS[kind] : "Unknown"}</span>
                {#if inst.endReason}
                  <Badge variant="outline">{END_REASON_LABELS[inst.endReason]}</Badge>
                {/if}
              </div>
              <div class="text-muted-foreground">
                {formatDateTime(inst.startedAt)} → {formatDateTime(inst.endedAt)}
                <span class="ml-1">({formatDuration(inst.startedAt, inst.endedAt)})</span>
              </div>
              {#if inst.notes}
                <div class="truncate">{inst.notes}</div>
              {/if}
            </div>
            <div class="flex gap-1 shrink-0">
              <Button size="sm" variant="ghost" onclick={() => openEdit(inst)}>
                <Pencil class="h-3 w-3" />
              </Button>
              <Button size="sm" variant="ghost" onclick={() => (deleteTarget = inst)}>
                <Trash2 class="h-3 w-3" />
              </Button>
            </div>
          </div>
        {/each}
      </div>
    {/if}
  </div>
</div>

<!-- Edit / End-now dialog ────────────────────────────────────────── -->
<Dialog.Root bind:open={editOpen}>
  <Dialog.Content class="sm:max-w-md">
    <Dialog.Header>
      <Dialog.Title>{editEndingNow ? "End wear session" : "Edit wear session"}</Dialog.Title>
      <Dialog.Description>
        {#if editTarget?.kind}
          {KIND_LABELS[editTarget.kind]} — started {formatDateTime(editTarget.startedAt)}
        {/if}
      </Dialog.Description>
    </Dialog.Header>

    {#if editTarget}
      <form
        {...remote.update.enhance(async ({ submit }) => {
          const result = await submit();
          if (result) editOpen = false;
        })}
        class="space-y-4"
      >
        <input type="hidden" name="id" value={editTarget.id} />

        <div class="space-y-2">
          <Label for="insertionSite">Insertion site</Label>
          <Input
            id="insertionSite"
            name="request.insertionSite"
            value={editTarget.insertionSite ?? ""}
            placeholder="e.g. upper-arm-left"
          />
        </div>

        <div class="space-y-2">
          <Label for="serialNumber">Serial number</Label>
          <Input
            id="serialNumber"
            name="request.serialNumber"
            value={editTarget.serialNumber ?? ""}
          />
        </div>

        {#if editEndingNow || editTarget.endedAt}
          <div class="space-y-2">
            <Label for="endedAt">Ended at</Label>
            <Input
              id="endedAt"
              name="request.endedAt"
              type="datetime-local"
              value={editTarget.endedAt
                ? new Date(editTarget.endedAt).toISOString().slice(0, 16)
                : new Date().toISOString().slice(0, 16)}
            />
          </div>

          <div class="space-y-2">
            <Label for="endReason">End reason</Label>
            <select
              id="endReason"
              name="request.endReason"
              class="w-full rounded-md border bg-background px-3 py-2 text-sm"
              value={editTarget.endReason ?? ConsumableInstanceEndReason.Planned}
            >
              {#each Object.values(ConsumableInstanceEndReason) as reason}
                <option value={reason}>{END_REASON_LABELS[reason]}</option>
              {/each}
            </select>
          </div>
        {/if}

        <div class="space-y-2">
          <Label for="notes">Notes</Label>
          <Textarea
            id="notes"
            name="request.notes"
            value={editTarget.notes ?? ""}
            rows={3}
          />
        </div>

        <Dialog.Footer>
          <Button type="button" variant="outline" onclick={() => (editOpen = false)}>
            Cancel
          </Button>
          <Button type="submit" disabled={!!remote.update.pending}>
            {#if remote.update.pending}
              <Loader2 class="mr-2 h-4 w-4 animate-spin" />
            {/if}
            Save
          </Button>
        </Dialog.Footer>
      </form>
    {/if}
  </Dialog.Content>
</Dialog.Root>

<!-- Delete confirm ──────────────────────────────────────────────── -->
<AlertDialog.Root open={!!deleteTarget} onOpenChange={(o) => !o && (deleteTarget = null)}>
  <AlertDialog.Content>
    <AlertDialog.Header>
      <AlertDialog.Title>Delete wear session?</AlertDialog.Title>
      <AlertDialog.Description>
        This removes the record from your history. The underlying device event is unaffected.
      </AlertDialog.Description>
    </AlertDialog.Header>
    <AlertDialog.Footer>
      <AlertDialog.Cancel disabled={deletePending}>Cancel</AlertDialog.Cancel>
      <AlertDialog.Action disabled={deletePending} onclick={confirmDelete}>
        {#if deletePending}<Loader2 class="mr-2 h-4 w-4 animate-spin" />{/if}
        Delete
      </AlertDialog.Action>
    </AlertDialog.Footer>
  </AlertDialog.Content>
</AlertDialog.Root>
