<template>
	<v-container class="flex-grow-1 d-flex flex-column ga-5">
		<v-form
			v-model="form.isValid"
			@submit.prevent="handleSearch"
			class="mt-4 d-flex ga-3 flex-column justify-center align-center">
			<v-text-field
				v-model="form.data.search"
				:disabled="starWarsCharacters.getPeopleList.length === 0"
				@input="debouncedSearch"
				hide-details
				width="100%"
				rounded="pill"
				variant="solo"
				max-width="700px"
				placeholder="Search">
				<template #prepend-inner>
					<v-icon>mdi-magnify</v-icon>
				</template>
				<template
					#append-inner
					@click="onClearSearch">
					<v-btn
						:disabled="!form.data.search"
						icon
						variant="text">
						<v-icon>mdi-close</v-icon>
					</v-btn>
				</template>
			</v-text-field>
		</v-form>
		<div class="flex-grow-1 relative w-100">
			<div class="absolute inset-0 overflow-scroll">
				<div class="card-list">
					<v-card
						v-for="character in peopleList"
						:key="character.url"
						@click="() => onOpenCharacterPage(character)"
						min-height="100px"
						rounded="lg">
						<v-card-text class="d-flex w-100">
							<div>
								<v-img
									:src="fallbackImgAddrs"
									height="100px"
									width="100px"
									rounded="circle"
									cover />
							</div>
							<div class="flex-grow-1 d-flex flex-column align-center pa-4">
								<span class="text-center font-weight-bold">
									{{ character.name }}
								</span>
							</div>
						</v-card-text>
					</v-card>
				</div>
			</div>
		</div>
	</v-container>
</template>

<script setup lang="ts">
import type { StarWarsApiResponse } from '@/api/models/starwars-api.models';
import router from '@/router';
import { useStarWarsPeople } from '@/stores/starwars-people.store';
import { debounce } from '@/utils/common.utils';
import { extractLastUrlSection } from '@/utils/url.utils';

const starWarsCharacters = useStarWarsPeople();

const peopleList = ref<StarWarsApiResponse.Person[]>([]);
const form = reactive({
	isValid: false,
	data: {
		search: null as string | null,
	},
});
const fallbackImgAddrs =
	'https://www.photomural.com/media/catalog/product/import/api-v1.1-file-public-files-pim-assets-97-ad-84-62-6284ad972eff292d45ce1a2e-images-47-73-f0-65-65f073472bc4ca5854c10e43-008-dvd2-star-wars-poster-classic-2-web.jpg';

onMounted(() => {
	starWarsCharacters.fetchPeopleList();
});

watch(starWarsCharacters, () => {
	if (starWarsCharacters.getPeopleList) {
		peopleList.value = starWarsCharacters.getPeopleList;
	}
});

const debouncedSearch = debounce(() => {
	handleSearch();
}, 500);

function onOpenCharacterPage(character: StarWarsApiResponse.Person) {
	const id = Number(extractLastUrlSection(character.url));
	if (isNaN(id)) throw new Error('id is not a number');

	router.push({
		name: 'Character',
		params: { id },
	});
}

function onClearSearch() {
	form.data.search = null;
}

function handleSearch() {
	debouncedSearch.cancel();

	peopleList.value = starWarsCharacters.getPeopleList.filter((character) =>
		character.name.toLowerCase().includes(form.data.search?.toLowerCase() || ''),
	);
}
</script>

<style scoped>
.card-list {
	display: grid;
	grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
	gap: 12px;
}
</style>

