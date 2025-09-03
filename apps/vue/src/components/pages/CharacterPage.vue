<template>
	<v-container class="flex-grow-1 d-flex flex-column ga-5">
		<div
			v-if="isLoading"
			class="flex-grow-1 d-flex justify-center align-center">
			<v-progress-circular
				color="primary"
				indeterminate
				model-value="40" />
		</div>
		<div class="d-flex justify-end">
			<v-btn
				color="primary"
				rounded="pill"
				variant="outlined"
				width="100px"
				@click="onBack">
				Back
			</v-btn>
		</div>

		<section
			v-if="!isLoading && characterDetails"
			class="flex-grow-1 d-flex relative">
			<div class="absolute inset-0 overflow-y-auto d-flex flex-column ga-5 py-1">
				<CharacterDetails :details="characterDetails" />
				<!-- reviews section -->
				<Review
					v-if="characterId"
					:characterId="characterId" />
			</div>
		</section>
	</v-container>
</template>

<script lang="ts" setup>
import router from '@/router';
import { useCharactersStore } from '@/store/stores/characters.store';

const route = useRoute();
const charactersStore = useCharactersStore();
const { isLoading } = storeToRefs(charactersStore);
const { characterDetails } = storeToRefs(charactersStore);

const characterId = computed(() => (typeof route.params.id === 'string' ? route.params.id : null));

function onBack() {
	router.push({ name: 'Home' });
}

onMounted(() => {
	const id = Number(route.params.id);
	if (isNaN(id)) throw new Error('id is not a number');
	charactersStore.fetchPeopleDetail(id);
});
</script>

