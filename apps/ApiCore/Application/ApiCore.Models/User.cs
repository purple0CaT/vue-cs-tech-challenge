namespace ApiCore.Models;

public class User {
	public string Id { get; set; } = string.Empty;
	public string[] FavoriteCharacters { get; set; } = [];
}
