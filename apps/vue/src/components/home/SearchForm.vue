<template>
	<v-form
		v-model="form.isValid"
		class="mt-4 d-flex ga-3 flex-column justify-center align-center"
		@submit.prevent="handleSearch">
		<v-text-field
			v-model="form.data.search"
			:disabled="getCharacterList.length === 0"
			@input="debouncedSearch"
			@click:clear="() => onClearSearch()"
			hide-details
			max-width="700px"
			placeholder="Search"
			rounded="pill"
			variant="solo"
			width="100%"
			clearable>
			<template #prepend-inner>
				<v-icon>mdi-magnify</v-icon>
			</template>
		</v-text-field>
	</v-form>
</template>

<script setup lang="ts">
import { useCharactersStore } from '@/store/stores/characters.store';
import { debounce } from '@/utils/common.utils';

const charactersStore = useCharactersStore();
const { getCharacterList } = storeToRefs(charactersStore);

const form = reactive({
	isValid: false,
	data: {
		search: null as string | null,
	},
});

const debouncedSearch = debounce(() => {
	handleSearch();
}, 500);

function onClearSearch() {
	form.data.search = null;
	charactersStore.setSearchQuery(null);
}

function handleSearch() {
	debouncedSearch.cancel();
	charactersStore.setSearchQuery(form.data.search);
}
</script>

<style scoped></style>

