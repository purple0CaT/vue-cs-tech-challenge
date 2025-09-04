using ApiCore.Clients.Contracts;
using ApiCore.Common.Extensions;
using ApiCore.Common.Models;
using ApiCore.Contracts;
using ApiCore.Models;

namespace ApiCore.Application;

public class CharacterService(IStarWarsApiClient starWarsApiClient, IUserService userService) : ICharacterService {
	private readonly int _pageSize = 10;
	private readonly IStarWarsApiClient _starWarsApiClient = starWarsApiClient.NotNull(nameof(starWarsApiClient));
	private readonly IUserService _userService = userService.NotNull(nameof(userService));
	public async Task<PagedResponse<Character>> GetCharacterList(int page, string? userId = null) {
		var items = await _starWarsApiClient.GetCharacterList();

		if (!string.IsNullOrWhiteSpace(userId)) {
			var user = await _userService.GetUserAsync(userId);
			var favoriteCharacterIds = user?.FavoriteCharacters ?? [];

			foreach (var character in items) {
				character.IsFavorite = favoriteCharacterIds.Contains(character.Id);
			}

			items = items.OrderByDescending(c => c.IsFavorite).ToList();
		}

		var response = new PagedResponse<Character> {
			Items = items.Skip((page - 1) * _pageSize).Take(_pageSize),
			Page = page,
			PageSize = 10,
			TotalPages = (int)Math.Ceiling((double)items.Count / _pageSize),
			TotalCount = items.Count
		};
		return response;
	}

	public async Task<Character> GetCharacter(int id, string? userId = null) {
		var character = await _starWarsApiClient.GetCharacter(id);

		if (!string.IsNullOrWhiteSpace(userId)) {
			var user = await _userService.GetUserAsync(userId);
			var favoriteCharacterIds = user?.FavoriteCharacters ?? [];
			character.IsFavorite = favoriteCharacterIds.Contains(character.Url);
		}

		return character;
	}
}
