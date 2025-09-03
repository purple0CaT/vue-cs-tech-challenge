using ApiCore.Clients.Contracts;
using ApiCore.Contracts;
using ApiCore.Models;

namespace ApiCore.Application;

public class CharacterService(IStarWarsApiClient starWarsApiClient) : ICharacterService {
	private readonly IStarWarsApiClient _starWarsApiClient = starWarsApiClient;
	public async Task<IList<Character>> GetAll(int page) {
		return await _starWarsApiClient.GetAll(page);
	}

	public Task<Character> GetOne(int id) {
		return _starWarsApiClient.GetOne(id);
	}
}
