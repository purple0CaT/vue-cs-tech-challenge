using ApiCore.Models;

namespace ApiCore.Clients.Contracts;

public interface IStarWarsApiClient {
	public Task<Character> GetOne(int id);
	public Task<IList<Character>> GetAll();
}
