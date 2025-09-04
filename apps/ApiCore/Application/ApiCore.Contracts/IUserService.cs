using ApiCore.Models;

namespace ApiCore.Contracts;

public interface IUserService
{
    Task<bool> ToggleFavoriteCharacterAsync(string userId, string characterId);
    Task<User?> GetUserAsync(string userId);
}
