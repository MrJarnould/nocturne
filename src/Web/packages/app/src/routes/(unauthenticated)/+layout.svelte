<script lang="ts">
  let { children } = $props();
</script>

<div class="min-h-screen flex flex-col bg-background">
  <!-- Header -->
  <header class="border-b border-border/40">
    <div class="container mx-auto px-4 py-4 flex justify-center">
      <div class="flex items-center gap-2.5">
        <div class="w-8 h-8 rounded-lg bg-primary/20 flex items-center justify-center">
          <span class="text-primary font-bold">N</span>
        </div>
        <span class="text-lg font-semibold">Nocturne</span>
      </div>
    </div>
  </header>

  <!-- Content -->
  <main class="flex-1">
    {@render children()}
  </main>
</div>
