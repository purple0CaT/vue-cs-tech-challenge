import { dotnetApiClient } from '.';

export namespace DotnetApi {
	export function getHealth(): Promise<string> {
		return dotnetApiClient.get<string>('/health');
	}
}

