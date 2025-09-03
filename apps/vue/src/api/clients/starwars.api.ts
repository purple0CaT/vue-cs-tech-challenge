import type { StarWarsApiResponse } from '../models/starwars-api.models';
import type { IReview } from '@/store/models/review.model';
import { starWarsApiClient } from '.';

export namespace StarWarsApi {
	export function getCharacters(): Promise<StarWarsApiResponse.Character[]> {
		return starWarsApiClient.get<StarWarsApiResponse.Character[]>(`/people`);
	}

	export function getCharacter(id: number): Promise<StarWarsApiResponse.Character> {
		return starWarsApiClient.get<StarWarsApiResponse.Character>(`/people/${id}`);
	}

	export function createReview(data: any): Promise<IReview> {
		return new Promise((resolve, reject) => {
			setTimeout(() => {
				reject('Error');
			}, 500);
		});
	}
}

