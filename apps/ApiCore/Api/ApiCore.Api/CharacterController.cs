
using ApiCore.Common.Models;
using ApiCore.Contracts;
using ApiCore.Models;

using Microsoft.AspNetCore.Mvc;

namespace ApiCore.Api;

[ApiController]
[Route("api/characters")]
public class CharacterController(ICharacterService characterService) : ControllerBase {
	private readonly ICharacterService _characterService = characterService;


	[HttpGet]
	//TODO the user id should be extracted from the token
	public async Task<ActionResult<PagedResponse<Character>>> GetCharacterList(
		[FromQuery] int page = 1,
		[FromQuery] string? userId = null) {
		try {
			var response = await _characterService.GetCharacterList(page, userId);
			return Ok(response);
		} catch (Exception ex) {
			return StatusCode(500, new { error = ex.Message });
		}
	}

	[HttpGet("{id:int}")]
	public async Task<ActionResult<Character>> GetCharacter(
		[FromRoute] int id,
		[FromQuery] string? userId = null) {
		try {
			var response = await _characterService.GetCharacter(id, userId);
			return Ok(response);
		} catch (Exception ex) {
			return StatusCode(500, new { error = ex.Message });
		}
	}
}