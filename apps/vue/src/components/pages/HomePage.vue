<template>
	<v-container class="flex-grow-1 d-flex flex-column ga-5">
		<SearchForm />
		<div
			v-if="isLoading"
			class="flex-grow-1 d-flex justify-center align-center">
			<v-progress-circular
				color="primary"
				indeterminate
				model-value="40" />
		</div>
		<div
			v-else
			class="flex-grow-1 relative w-100">
			<!-- extracted the classes to seperate div, due to card taking up all the available height-->
			<div class="absolute inset-0 overflow-y-auto">
				<div class="card-list">
					<CharacterListCard
						v-for="person in getCharacterList"
						:key="person.url"
						:person="person" />
				</div>
			</div>
		</div>
	</v-container>
</template>

<script setup lang="ts">
import { useCharactersStore } from '@/store/stores/characters.store';

const charactersStore = useCharactersStore();
const { getCharacterList, isLoading } = storeToRefs(charactersStore);

onMounted(() => {
	charactersStore.fetchPeopleList();
});
</script>

<style scoped>
.card-list {
	display: grid;
	grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
	gap: 12px;
}
</style>

