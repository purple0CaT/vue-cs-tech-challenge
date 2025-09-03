import type { IReview } from '@/store/models/review.model';

export type IReviewRequestDto = Omit<IReview, 'id' | 'createdAt'> & {
	characterId: string;
};

