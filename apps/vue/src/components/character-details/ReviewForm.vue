<template>
	<v-form
		ref="formRef"
		v-model="form.isValid"
		class="d-flex flex-column ga-4 w-100 bg-grey-lighten-4 pa-4 rounded-xl"
		@submit.prevent="onSubmit">
		<div class="d-flex flex-column ga-4">
			<v-rating
				v-model="form.data.rating"
				active-color="primary"
				density="compact"
				hover
				:length="10"
				:readonly="isWorking"
				:rules="[rules.required]"
				:size="28" />
			<v-text-field
				v-model="form.data.name"
				hide-details
				label="Name"
				:readonly="isWorking"
				rounded="xl"
				:rules="[rules.required]"
				variant="solo"
				width="100%" />
			<v-textarea
				v-model="form.data.comment"
				hide-details
				label="Review"
				:readonly="isWorking"
				rounded="xl"
				rows="4"
				:rules="[rules.required]"
				variant="solo"
				width="100%" />
			<v-menu
				v-model="menu"
				:close-on-content-click="false"
				transition="scale-transition"
				offset-y
				min-width="auto">
				<template #activator="{ props }">
					<v-text-field
						v-bind="props"
						:model-value="form.data.watchedAt ? formatDate(form.data.watchedAt) : ''"
						:rules="[rules.required]"
						variant="solo"
						width="100%"
						label="Select date"
						rounded="xl"
						append-inner-icon="mdi-calendar"
						readonly
						clearable>
						<template #append-inner>
							<v-btn
								rounded="pill"
								variant="plain"
								@click.stop="onSetToday">
								Today
							</v-btn>
						</template>
					</v-text-field>
				</template>

				<v-date-picker
					v-model="form.data.watchedAt"
					:max="formatDate(new Date(), 'iso')"
					@update:model-value="onSaveDate" />
			</v-menu>
		</div>
		<div class="w-100 d-flex justify-space-between align-center">
			<v-switch
				v-model="shouldSimulate"
				color="primary"
				hide-details
				label="Simulate API call"
				style="opacity: 0.5" />
			<FormButton
				color="primary"
				:disabled="!form.isValid"
				:is-error="!!getError"
				:is-success="isSuccess"
				label="Submit"
				:loading="isWorking"
				rounded="pill"
				type="submit"
				width="100px" />
		</div>
	</v-form>
</template>

<script setup lang="ts">
import type { IReviewRequestDto } from '@/models/requests/review.request';
import { useReviewsStore } from '@/store/stores/review.store';
import { formatDate } from '@/utils/common.utils';
import { rules } from '@/utils/validator-rules';

const props = defineProps<{
	characterId: string;
}>();

const reviewsStore = useReviewsStore();
const { isWorking, isSuccess, getError } = storeToRefs(reviewsStore);

const shouldSimulate = ref(false);
const formRef = ref();
const menu = ref(false);

const form = reactive({
	isValid: false,
	data: {
		rating: 0,
		name: null as string | null,
		comment: null as string | null,
		watchedAt: null as Date | null,
	},
});

async function onSubmit() {
	if (!props.characterId) throw new Error('characterId is null');

	if (!form.data.name || !form.data.comment || !form.data.watchedAt)
		throw new Error('Name, Comment, Date watched are required');

	const requestDto: IReviewRequestDto = {
		name: form.data.name,
		comment: form.data.comment,
		rating: form.data.rating,
		watchedAt: form.data.watchedAt,
		characterId: props.characterId,
	};
	const response = await reviewsStore.createReview(requestDto, shouldSimulate.value);
	if (response) {
		form.data.rating = 0;
		formRef.value?.reset();
		formRef.value?.resetValidation();
	}
}

function onSaveDate(value: Date | null) {
	if (!value) {
		form.data.watchedAt = null;
		return;
	}

	form.data.watchedAt = new Date(value).toISOString().split('T')[0] as any;
}

function onSetToday() {
	form.data.watchedAt = new Date().toISOString().split('T')[0] as any;
}
</script>

<style scoped></style>

