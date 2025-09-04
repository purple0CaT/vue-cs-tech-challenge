using ApiCore.Models;

namespace ApiCore.Repositories;

public interface IUserRepository {
	Task<User?> GetByIdAsync(string id);
	Task StoreUserAsync(User user);
	Task UpdateUserAsync(User user);
	Task DeleteUserAsync(string id);
}
