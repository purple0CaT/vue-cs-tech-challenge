using ApiCore.Models;

namespace ApiCore.Clients.Contracts;

public interface IStarWarsApiClient {
	public Task<IList<Character>> GetCharacterList();
	public Task<Character> GetCharacter(int id);
}
