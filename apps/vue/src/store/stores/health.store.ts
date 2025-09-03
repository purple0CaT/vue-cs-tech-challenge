import { DotnetApi } from '@/api/clients/dotnet.api';

export const useHealthStore = defineStore('health', {
	state: () => ({
		healthy: false,
	}),
	getters: {},
	actions: {
		checkHealth(): Promise<void> {
			return DotnetApi.getHealth()
				.then(() => {
					this.healthy = true;
				})
				.catch(() => {
					this.healthy = false;
				});
		},
	},
});

