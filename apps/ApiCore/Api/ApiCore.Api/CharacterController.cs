
using ApiCore.Contracts;

using Microsoft.AspNetCore.Mvc;

namespace ApiCore.Api;

[ApiController]
[Route("api/characters")]
public class SWCharacterController(ICharacterService characterService) : ControllerBase {
	private readonly ICharacterService _characterService = characterService;


	[HttpGet]
	public async Task<ActionResult<object>> GetAll() {
		try {
			var response = await _characterService.GetAll();
			return Ok(response);
		} catch (Exception ex) {
			return StatusCode(500, new { error = ex.Message });
		}
	}

	[HttpGet("{id:int}")]
	public async Task<ActionResult<object>> GetOne(int id) {
		try {
			var response = await _characterService.GetOne(id);
			return Ok(response);
		} catch (Exception ex) {
			return StatusCode(500, new { error = ex.Message });
		}
	}
}