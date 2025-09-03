<template>
	<v-card
		rounded="lg"
		@click="() => onOpenCharacterPage(person)">
		<v-card-text class="d-flex w-100 relative">
			<v-btn
				class="absolute"
				icon
				size="small"
				style="right: 0; top: 0"
				variant="text"
				@click.stop="() => onToggleFavorite(person)">
				<v-icon
					v-if="person.isFavorite"
					color="error"
					size="large">
					mdi-heart
				</v-icon>
				<v-icon
					v-else
					color="grey"
					size="large">
					mdi-heart-outline
				</v-icon>
			</v-btn>
			<div>
				<v-img
					cover
					height="100px"
					rounded="circle"
					:src="fallbackImgAddrs"
					width="100px" />
			</div>
			<div class="flex-grow-1 d-flex flex-column align-center">
				<span class="text-center font-weight-bold">
					{{ person.name }}
				</span>

				<div class="d-flex flex-column align-center justify-center ga-1 mt-auto">
					<v-rating
						class="ml-auto mt-auto"
						color="grey-lighten-1"
						active-color="primary"
						density="compact"
						hover
						:length="10"
						:model-value="getAvarageRating(extractLastUrlSection(person.url) || '')"
						readonly
						:size="18" />
					<span>
						{{ getReviewsByCharacter(extractLastUrlSection(person.url) || '').length }}
						<v-icon color="grey-lighten-1">mdi-message-text-outline</v-icon>
					</span>
				</div>
			</div>
		</v-card-text>
	</v-card>
</template>

<script setup lang="ts">
import router from '@/router';
import type { ICharacter } from '@/store/models/character.model';
import { useReviewsStore } from '@/store/stores/review.store';
import { useCharactersStore } from '@/store/stores/characters.store';
import { extractLastUrlSection } from '@/utils/url.utils';

const fallbackImgAddrs =
	'https://www.photomural.com/media/catalog/product/import/api-v1.1-file-public-files-pim-assets-97-ad-84-62-6284ad972eff292d45ce1a2e-images-47-73-f0-65-65f073472bc4ca5854c10e43-008-dvd2-star-wars-poster-classic-2-web.jpg';

const props = defineProps<{
	person: ICharacter;
}>();

const charactersStore = useCharactersStore();
const { getReviewsByCharacter, getAvarageRating } = storeToRefs(useReviewsStore());

function onOpenCharacterPage(character: ICharacter) {
	const id = Number(extractLastUrlSection(character.url));
	if (isNaN(id)) throw new Error('id is not a number');

	router.push({
		name: 'Character',
		params: { id },
	});
}

function onToggleFavorite(character: ICharacter) {
	const id = Number(extractLastUrlSection(character.url));
	if (isNaN(id)) throw new Error('id is not a number');
	charactersStore.toggleFavorite(id);
}
</script>

<style scoped></style>

