
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
	public async Task<ActionResult<PagedResponse<Character>>> GetCharacterList(int page = 1) {
		try {
			var response = await _characterService.GetCharacterList(page);
			return Ok(response);
		} catch (Exception ex) {
			return StatusCode(500, new { error = ex.Message });
		}
	}

	[HttpGet("{id:int}")]
	public async Task<ActionResult<Character>> GetCharacter(int id) {
		try {
			var response = await _characterService.GetCharacter(id);
			return Ok(response);
		} catch (Exception ex) {
			return StatusCode(500, new { error = ex.Message });
		}
	}
}