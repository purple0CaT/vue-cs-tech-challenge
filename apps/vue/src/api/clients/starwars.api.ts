import { starWarsApiClient } from '.';
import type { StarWarsApiResponse } from '../models/starwars-api.models';

export namespace StarWarsApi {
	export function getCharacters(): Promise<StarWarsApiResponse.Person[]> {
		return starWarsApiClient.get<StarWarsApiResponse.Person[]>(`/people`);
	}

	export function getCharacter(id: string): Promise<StarWarsApiResponse.Person> {
		return starWarsApiClient.get<StarWarsApiResponse.Person>(`/people/${id}`);
	}

	// FIXME DTO
	export function createCharacter(data: any): Promise<StarWarsApiResponse.Person> {
		return starWarsApiClient.post<StarWarsApiResponse.Person>(`/people`, data);
	}
}

