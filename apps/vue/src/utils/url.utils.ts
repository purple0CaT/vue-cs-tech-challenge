export function extractQueryParam(url: string, param: string): string | null {
	return new URL(url).searchParams.get(param);
}

export function extractLastUrlSection(url: string): string | null {
	return url.split('/').pop() ?? null;
}

