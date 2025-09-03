using ApiCore.Common.Models;
using ApiCore.Models;

namespace ApiCore.Contracts;

public interface ICharacterService {
	Task<PagedResponse<Character>> GetCharacterList(int page);
	Task<Character> GetCharacter(int id);
}
