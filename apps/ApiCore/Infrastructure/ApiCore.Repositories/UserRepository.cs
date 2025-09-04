using ApiCore.Models;

using Microsoft.Extensions.Caching.Memory;

namespace ApiCore.Repositories;

public class UserRepository(IMemoryCache memoryCache) : IUserRepository {
	private readonly IMemoryCache _memoryCache = memoryCache;
	private readonly TimeSpan _cacheExpiration = TimeSpan.FromHours(24);
	private const string USER_CACHE_KEY_PREFIX = "user_";

	public Task<User?> GetByIdAsync(string id) {
		var cacheKey = $"{USER_CACHE_KEY_PREFIX}{id}";

		if (_memoryCache.TryGetValue(cacheKey, out User? cachedUser)) {
			return Task.FromResult(cachedUser);
		}

		return Task.FromResult<User?>(null);
	}

	public Task StoreUserAsync(User user) {
		var cacheKey = $"{USER_CACHE_KEY_PREFIX}{user.Id}";
		_memoryCache.Set(cacheKey, user, _cacheExpiration);
		return Task.CompletedTask;
	}

	public Task UpdateUserAsync(User user) {
		var cacheKey = $"{USER_CACHE_KEY_PREFIX}{user.Id}";
		_memoryCache.Set(cacheKey, user, _cacheExpiration);
		return Task.CompletedTask;
	}

	public Task DeleteUserAsync(string id) {
		var cacheKey = $"{USER_CACHE_KEY_PREFIX}{id}";
		_memoryCache.Remove(cacheKey);
		return Task.CompletedTask;
	}
}
