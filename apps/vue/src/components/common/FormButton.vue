<template>
	<v-btn
		:color="isSuccess ? 'success' : isError ? 'error' : color"
		:disabled="disabled"
		:height="height"
		:loading="loading"
		:rounded="rounded"
		:type="type"
		:variant="variant"
		:width="width">
		<span v-if="isSuccess"><v-icon color="white">mdi-check</v-icon></span>
		<span v-else-if="isError"> <v-icon color="white">mdi-close</v-icon> </span>
		<span v-else>
			<v-icon
				v-if="icon"
				:icon="icon" />
			{{ label }}
		</span>
	</v-btn>
</template>

<script setup lang="ts">
const props = defineProps<{
	label: string;
	icon?: string;
	disabled?: boolean;
	loading?: boolean;
	type?: string;
	rounded?: string;
	variant?: 'flat' | 'text' | 'elevated' | 'tonal' | 'outlined' | 'plain' | undefined;
	color?: string;
	width?: string;
	height?: string;
	isSuccess?: boolean;
	isError?: boolean;
}>();
</script>

<style scoped></style>

