using ApiCore.Contracts;
using ApiCore.Models;
using ApiCore.Repositories;

namespace ApiCore.Application;

public class UserService(IUserRepository userRepository) : IUserService {
	private readonly IUserRepository _userRepository = userRepository;

	public async Task<bool> ToggleFavoriteCharacterAsync(string userId, string characterId) {
		var user = await _userRepository.GetByIdAsync(userId);

		if (user == null) {
			user = new User {
				Id = userId,
				FavoriteCharacters = [characterId]
			};
			await _userRepository.StoreUserAsync(user);
			return true;
		}

		var isCurrentlyFavorite = user.FavoriteCharacters.Contains(characterId);

		if (isCurrentlyFavorite) {
			user.FavoriteCharacters = user.FavoriteCharacters
				.Where(id => id != characterId)
				.ToArray();
		} else {
			user.FavoriteCharacters = user.FavoriteCharacters
				.Concat([characterId])
				.ToArray();
		}

		await _userRepository.UpdateUserAsync(user);

		return !isCurrentlyFavorite;
	}

	public async Task<User?> GetUserAsync(string userId) {
		return await _userRepository.GetByIdAsync(userId);
	}
}
