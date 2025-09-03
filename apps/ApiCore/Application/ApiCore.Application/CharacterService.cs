using ApiCore.Clients.Contracts;
using ApiCore.Common.Models;
using ApiCore.Contracts;
using ApiCore.Models;

namespace ApiCore.Application;

public class CharacterService(IStarWarsApiClient starWarsApiClient) : ICharacterService {
	private readonly int _pageSize = 10;
	private readonly IStarWarsApiClient _starWarsApiClient = starWarsApiClient;
	public async Task<PagedResponse<Character>> GetCharacterList(int page) {
		var items = await _starWarsApiClient.GetCharacterList();

		var response = new PagedResponse<Character> {
			Items = items.Skip((page - 1) * _pageSize).Take(_pageSize),
			Page = page,
			PageSize = 10,
			TotalPages = (int)Math.Ceiling((double)items.Count / _pageSize),
			TotalCount = items.Count
		};
		return response;
	}

	public Task<Character> GetCharacter(int id) {
		return _starWarsApiClient.GetCharacter(id);
	}
}
