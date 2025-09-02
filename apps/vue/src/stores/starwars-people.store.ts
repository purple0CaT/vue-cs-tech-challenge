import { StarWarsApi } from '@/api/clients/starwars.api';
import type { StarWarsApiResponse } from '@/api/models/starwars-api.models';
import { defineStore } from 'pinia';

interface IStarWarsPeopleState {
	loading: boolean;
	error: string | null;
	peopleList: StarWarsApiResponse.Person[];
	selected: StarWarsApiResponse.Person | null;
}

export const useStarWarsPeople = defineStore('star-wars-people', {
	state: (): IStarWarsPeopleState => ({
		// since the api doesn't provide pagination, we would just load all the characters
		// otherwise I would implement infinite scroll / pagination logic
		loading: false,
		peopleList: [],
		selected: null,
		error: null,
	}),
	getters: {
		getPeopleList: (state): StarWarsApiResponse.Person[] => state.peopleList ?? [],
		getSelected: (state): StarWarsApiResponse.Person | null => state.selected ?? null,
	},
	actions: {
		fetchPeopleList(): Promise<void> {
			this.loading = true;
			return StarWarsApi.getCharacters()
				.then((response) => {
					this.peopleList = response;
				})
				.catch((error) => {
					this.error = error.message ?? 'Unknown error';
				})
				.finally(() => {
					this.loading = false;
				});
		},
		fetchPeopleDetail(id: string): Promise<void> {
			this.loading = true;
			return StarWarsApi.getCharacter(id)
				.then((response) => {
					this.selected = response;
				})
				.catch((error) => {
					this.error = error.message ?? 'Unknown error';
				})
				.finally(() => {
					this.loading = false;
				});
		},
		reset(): void {
			this.peopleList = [];
			this.selected = null;
			this.error = null;
			this.loading = false;
		},
	},
});

