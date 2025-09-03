<template>
	<v-app class="d-flex flex-column">
		<router-view />

		<v-snackbar-queue v-model="queue">
			<template #text="{ item }">
				<div class="d-flex align-center pr-2">
					<v-icon
						v-if="item.icon"
						class="pr-3"
						:size="item.iconSize || 30">
						{{ item.icon }}
					</v-icon>
					<div class="w-100 text-center">{{ item.text }}</div>
				</div>
			</template>
		</v-snackbar-queue>
	</v-app>
</template>

<script lang="ts" setup>
import { storeToRefs } from 'pinia';
import { useToastMessagesStore } from './store/stores/toast-messages.store';

const toastStore = useToastMessagesStore();
const { queue } = storeToRefs(toastStore);
</script>

