using ApiCore.Common.Models;
using ApiCore.Models;

namespace ApiCore.Contracts;

public interface ICharacterService {
	Task<PagedResponse<Character>> GetCharacterList(int page, string? userId = null);
	Task<Character> GetCharacter(int id, string? userId = null);
}
