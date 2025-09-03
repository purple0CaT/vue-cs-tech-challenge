<template>
	<v-card rounded="xl">
		<v-card-text class="d-flex flex-column ga-4 pa-6">
			<div>
				<div class="d-flex justify-start mt-auto text-h5">Rating overview</div>
				<v-rating
					active-color="primary"
					density="compact"
					hover
					:length="10"
					:model-value="characterId ? getAvarageRating(characterId) : 0"
					readonly
					:size="30" />
			</div>

			<v-divider />

			<h5 class="d-flex justify-start mt-4 text-h5 text-grey-darken-1">Reviews</h5>
			<section class="d-flex flex-column-reverse flex-md-row ga-4">
				<div
					v-if="reviews.length === 0"
					class="left-column">
					<span class="text-grey-darken-1"> No reviews yet </span>
				</div>
				<div
					v-else
					class="flex-grow-1 d-flex flex-column ga-4 left-column">
					<div
						v-for="review in reviews"
						:key="review.id"
						class="d-flex flex-column bg-grey-lighten-5 ga-4 pa-2 rounded-lg">
						<div class="d-flex justify-space-between align-center">
							<span
								class="text-body-1 font-weight-bold text-capitalize text-grey-darken-1">
								{{ review.name }}
							</span>
							<span class="text-body-2 text-grey-darken-1">
								{{ formatDate(review.createdAt) }}
							</span>
						</div>
						<v-rating
							active-color="primary"
							density="compact"
							hover
							:length="10"
							:model-value="review.rating"
							:readonly="true"
							:size="24" />
						<span>
							<strong class="text-grey-darken-2">Viewed:</strong>
							{{ formatDate(review.watchedAt) }}
						</span>
						<span class="text-body-1">
							{{ review.comment }}
						</span>
					</div>
				</div>
				<div class="flex-md-grow-1">
					<ReviewForm :characterId="characterId" />
				</div>
			</section>
		</v-card-text>
	</v-card>
</template>

<script setup lang="ts">
import { useReviewsStore } from '@/store/stores/review.store';
import { formatDate } from '@/utils/common.utils';

const props = defineProps<{
	characterId: string;
}>();

const reviewsStore = useReviewsStore();
const { getAvarageRating, getReviewsByCharacter } = storeToRefs(reviewsStore);

const reviews = computed(() => {
	if (!props.characterId) return [];
	return getReviewsByCharacter.value(props.characterId);
});
</script>

<style lang="scss" scoped>
.left-column {
	width: 100%;
	max-width: 600px;
}
</style>

