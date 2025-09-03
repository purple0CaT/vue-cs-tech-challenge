using ApiCore.Models;

namespace ApiCore.Contracts;

public interface ICharacterService {
	Task<IList<Character>> GetAll(int page);
	Task<Character> GetOne(int id);
}
