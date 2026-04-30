<script lang="ts">
	interface Props {
		value: number;
		large?: boolean;
	}

	const { value, large = false }: Props = $props();
</script>

<span
	class="inline-flex items-baseline gap-1 whitespace-nowrap rounded-full {large
		? 'px-3 py-1 text-sm'
		: 'px-2.5 py-0.5 text-xs'}"
	style="background: oklch(0.769 0.188 70.08 / 0.12); color: oklch(0.85 0.18 75); border: 1px solid oklch(0.769 0.188 70.08 / 0.22);"
>
	<span class="tabular-nums {large ? 'text-base font-bold' : 'text-sm font-bold'}">{value}</span>
	<span>g carbs</span>
</span>
