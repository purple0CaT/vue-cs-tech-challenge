using ApiCore.Models;

namespace ApiCore.Contracts;

public interface ICharacterService {
	Task<IList<Character>> GetAll();
	Task<Character> GetOne(int id);
}
