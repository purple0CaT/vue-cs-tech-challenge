using ApiCore.Contracts;
using ApiCore.Models;
using ApiCore.Models.Requests;

using Microsoft.AspNetCore.Mvc;

namespace ApiCore.Api;

[ApiController]
[Route("api/users")]
public class UserController(IUserService userService) : ControllerBase {
	private readonly IUserService _userService = userService;

	[HttpPost("toggle-favorite")]
	public async Task<ActionResult<bool>> ToggleFavorite([FromBody] ToggleFavoriteRequest request) {
		try {
			if (string.IsNullOrWhiteSpace(request.UserId) || string.IsNullOrWhiteSpace(request.CharacterId)) {
				return BadRequest(new { error = "UserId and CharacterId are required" });
			}

			var isFavorite = await _userService.ToggleFavoriteCharacterAsync(request.UserId, request.CharacterId);

			return Ok(isFavorite);
		} catch (Exception ex) {
			return StatusCode(500, ex.Message);
		}
	}

	[HttpGet("{userId}")]
	// the user id should be extracted from the token
	public async Task<ActionResult<User>> GetUser(string userId) {
		try {
			if (string.IsNullOrWhiteSpace(userId)) {
				return BadRequest(new { error = "UserId is required" });
			}

			var user = await _userService.GetUserAsync(userId);

			if (user == null) {
				return NotFound(new { error = "User not found" });
			}

			return Ok(user);
		} catch (Exception ex) {
			return StatusCode(500, new { error = ex.Message });
		}
	}
}
