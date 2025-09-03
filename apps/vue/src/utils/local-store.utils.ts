export function getLocalStoreValue<T>(key: string): T | null {
	const store = localStorage.getItem(key);

	if (store) {
		return JSON.parse(store) as T;
	}
	return null;
}

