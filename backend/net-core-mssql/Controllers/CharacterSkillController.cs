using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using net_core_mssql.Dtos.CharacterSkill;
using net_core_mssql.Services;

namespace net_core_mssql.Controllers
{
  [Authorize]
  [ApiController]
  [Route("api/[controller]")]
  public class CharacterSkillController : ControllerBase
  {
    private readonly ICharacterSkillService characterSkillService;
    public CharacterSkillController(ICharacterSkillService characterSkillService)
    {
      this.characterSkillService = characterSkillService;

    }

    [HttpPost]
    public async Task<IActionResult> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
    {
      return Ok(await characterSkillService.AddCharacterSkill(newCharacterSkill));
    }
  }
}