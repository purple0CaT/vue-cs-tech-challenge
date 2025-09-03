import type { IReview } from '../models/review.model';
import type { IReviewRequestDto } from '@/models/requests/review.request';
import { nanoid } from 'nanoid';
import { defineStore } from 'pinia';
import { StarWarsApi } from '@/api/clients/starwars.api';
import { useToastMessagesStore } from './toast-messages.store';

interface IReviewState {
	working: boolean;
	success: boolean;
	error: string | null;
	reviews: Record<string, IReview[]>;
}

export const useReviewsStore = defineStore('reviews', {
	state: (): IReviewState => ({
		reviews: {},
		success: false,
		working: false,
		error: null,
	}),
	getters: {
		isWorking: (state): boolean => state.working,
		isSuccess: (state): boolean => state.success,
		getError: (state): string | null => state.error,
		getAvarageRating: (state) => {
			return (characterId: string): number =>
				(state.reviews[characterId]?.reduce((acc, review) => acc + review.rating, 0) ?? 0) /
				(state.reviews[characterId]?.length ?? 0);
		},
		getReviewsByCharacter: (state) => {
			return (characterId: string): IReview[] => state.reviews[characterId] ?? [];
		},
	},
	actions: {
		async createReview(
			commentDto: IReviewRequestDto,
			shouldSimulate: boolean,
		): Promise<boolean> {
			this.error = null;
			if (!this.reviews[commentDto.characterId]) {
				this.reviews[commentDto.characterId] = [];
			}
			this.working = true;

			const request = shouldSimulate
				? simulateCreateReview(commentDto)
				: StarWarsApi.createReview(commentDto);

			return request
				.then((resp) => {
					this.success = true;
					this.reviews[commentDto.characterId].unshift(resp);
					setTimeout(() => {
						this.success = false;
					}, 1500);
					return true;
				})
				.catch((error) => {
					const toastMessagesStore = useToastMessagesStore();
					this.error = error.message ?? 'Unknown error';
					toastMessagesStore.showError(this.error!);

					setTimeout(() => {
						this.error = null;
					}, 3000);
					return false;
				})
				.finally(() => {
					this.working = false;
				});
		},
	},
	persist: {
		storage: localStorage,
	},
});

function simulateCreateReview(commentDto: IReviewRequestDto): Promise<IReview> {
	return new Promise((resolve, reject) => {
		setTimeout(() => {
			const resp: IReview = {
				name: commentDto.name,
				comment: commentDto.comment,
				watchedAt: commentDto.watchedAt,
				rating: commentDto.rating,
				characterId: commentDto.characterId,
				createdAt: new Date(),
				id: nanoid(),
			};
			resolve(resp);
		}, 1500);
	});
}

