using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using net_core_mssql.Dtos.Character;
using net_core_mssql.Models;
using net_core_mssql.Services;

namespace net_core_mssql.Controllers
{
  [Authorize]
  [ApiController]
  [Route("api/[controller]")]
  public class CharacterController : ControllerBase
  {
    private readonly ICharacterService characterService;

    public CharacterController(ICharacterService characterService)
    {
      this.characterService = characterService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
      return Ok(await characterService.GetCharacterById(id));
    }

    [HttpPost]
    //[AllowAnonymous]
    public async Task<IActionResult> AddCharacter(AddCharacterDto newCharacter)
    {
      return Ok(await characterService.AddCharacter(newCharacter));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCharacter(UpdateCharacterDto updatedCharacter)
    {
      ServiceResponse<GetCharacterDto> response = await characterService.UpdateCharacter(updatedCharacter);
      if (response.Data == null)
      {
        return NotFound(response);
      }

      return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      ServiceResponse<List<GetCharacterDto>> response = await characterService.DeleteCharacter(id);
      if (response.Data == null)
      {
        return NotFound(response);
      }

      return Ok(response);
    }

    [HttpGet("GetAll")]
    //[AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
      // int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
      // return Ok(await characterService.GetAllCharacters(userId));
      return Ok(await characterService.GetAllCharacters());
    }
  }
}