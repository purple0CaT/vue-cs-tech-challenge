import type { StarWarsApiResponse } from '@/api/models/starwars-api.models';

export type ICharacter = StarWarsApiResponse.Character & {
	isFavorite: boolean;
};

