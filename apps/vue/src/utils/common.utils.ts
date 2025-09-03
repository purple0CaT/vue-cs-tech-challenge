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

export function displayDate(date: Date) {
	return new Date(date).toLocaleDateString();
}

