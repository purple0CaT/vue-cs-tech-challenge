import axios, { type AxiosInstance } from 'axios';

export class ApiClient {
	private client: AxiosInstance;

	constructor(baseURL: string, config?: any) {
		this.client = axios.create({
			baseURL,
			timeout: 10000,
			...config,
		});

		// TODO we can add here interceptors if needed (auth, logs etc.)
	}

	async get<T>(url: string, config?: any): Promise<T> {
		const response = await this.client.get(url, config);
		return response.data;
	}

	async post<T>(url: string, data?: any, config?: any): Promise<T> {
		const response = await this.client.post(url, data, config);
		return response.data;
	}

	async put<T>(url: string, data?: any, config?: any): Promise<T> {
		const response = await this.client.put(url, data, config);
		return response.data;
	}

	async delete<T>(url: string, config?: any): Promise<T> {
		const response = await this.client.delete(url, config);
		return response.data;
	}
}

export const starWarsApiClient = new ApiClient('https://swapi.info/api', { timeout: 5000 });

