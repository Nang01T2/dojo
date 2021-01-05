using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using net_core_mssql.Dtos.Weapon;
using net_core_mssql.Services;

namespace net_core_mssql.Controllers
{
  [Authorize]
  [ApiController]
  [Route("api/[controller]")]
  public class WeaponController : ControllerBase
  {
    private readonly IWeaponService weaponService;
    public WeaponController(IWeaponService weaponService)
    {
      this.weaponService = weaponService;

    }

    [HttpPost]
    public async Task<IActionResult> AddWeapon(AddWeaponDto newWeapon)
    {
      return Ok(await weaponService.AddWeapon(newWeapon));
    }
  }
}