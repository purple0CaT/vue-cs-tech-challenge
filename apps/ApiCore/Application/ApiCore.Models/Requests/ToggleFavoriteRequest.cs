namespace ApiCore.Models.Requests;

public class ToggleFavoriteRequest {
	public string UserId { get; set; } = string.Empty;
	public string CharacterId { get; set; } = string.Empty;
}
