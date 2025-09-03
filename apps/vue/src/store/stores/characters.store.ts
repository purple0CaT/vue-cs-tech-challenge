import { StarWarsApi } from '@/api/clients/starwars.api';
import type { StarWarsApiResponse } from '@/api/models/starwars-api.models';
import { extractLastUrlSection } from '@/utils/url.utils';
import { defineStore } from 'pinia';
import type { ICharacter } from '../models/character.model';

interface ICharactersState {
	loading: boolean;
	error: string | null;
	characterList: StarWarsApiResponse.Character[];
	characterDetails: StarWarsApiResponse.Character | null;
	userFavorites: number[];
	searchQuery: string | null;
}

export const useCharactersStore = defineStore('characters', {
	state: (): ICharactersState => ({
		// since the api doesn't provide pagination, we would just load all the characters
		// otherwise I would implement infinite scroll / pagination logic
		loading: false,
		characterList: [],
		characterDetails: null,
		error: null,
		userFavorites: [],
		searchQuery: null,
	}),
	getters: {
		isLoading: (state): boolean => state.loading,
		getCharacterList: (state): ICharacter[] => {
			const list = state.characterList
				.map((char) => ({
					...char,
					isFavorite: state.userFavorites.includes(
						Number(extractLastUrlSection(char.url)),
					),
				}))
				.sort((a, b) => {
					if (a.isFavorite) return -1;
					if (b.isFavorite) return 1;
					return 0;
				});
			if (state.searchQuery) {
				return list.filter((char) =>
					char.name.toLowerCase().includes(state.searchQuery!.toLowerCase()),
				);
			}
			return list;
		},
	},
	actions: {
		async fetchPeopleList(): Promise<void> {
			this.loading = true;
			return await StarWarsApi.getCharacters()
				.then((response) => {
					this.characterList = response;
				})
				.catch((error) => {
					this.error = error.message ?? 'Unknown error';
				})
				.finally(() => {
					this.loading = false;
				});
		},
		async fetchPeopleDetail(id: number): Promise<void> {
			this.characterDetails = null;
			this.loading = true;
			return await StarWarsApi.getCharacter(id)
				.then((response) => {
					this.characterDetails = response;
				})
				.catch((error) => {
					this.error = error.message ?? 'Unknown error';
				})
				.finally(() => {
					this.loading = false;
				});
		},
		reset(): void {
			this.characterList = [];
			this.characterDetails = null;
			this.error = null;
			this.loading = false;
		},
		toggleFavorite(characterId: number): void {
			const isFavorite = this.userFavorites.find((id) => id === characterId);
			if (isFavorite) {
				this.userFavorites = this.userFavorites.filter((id) => id !== characterId);
			} else {
				this.userFavorites.push(characterId);
			}
		},
		setSearchQuery(query: string | null): void {
			this.searchQuery = query;
		},
	},
	persist: {
		storage: localStorage,
		pick: ['userFavorites'],
	},
});

