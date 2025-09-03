export function debounce<T extends (...args: any[]) => any>(
	callback: T,
	wait: number,
): ((...args: Parameters<T>) => void) & { cancel: () => void } {
	let timeoutId: ReturnType<typeof setTimeout>;

	const debouncedFunction = (...args: Parameters<T>) => {
		clearTimeout(timeoutId);
		timeoutId = setTimeout(() => {
			callback.apply(null, args);
		}, wait);
	};

	debouncedFunction.cancel = () => {
		clearTimeout(timeoutId);
	};

	return debouncedFunction;
}

export function formatDate(date: Date, type: 'local' | 'iso' = 'local'): string {
	if (type === 'local') return new Date(date).toLocaleDateString();
	return new Date(date).toISOString().split('T')[0];
}

