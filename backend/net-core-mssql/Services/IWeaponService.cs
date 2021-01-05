using System.Threading.Tasks;
using net_core_mssql.Dtos.Character;
using net_core_mssql.Dtos.Weapon;

namespace net_core_mssql.Services
{
    public interface IWeaponService
    {
         Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon);
    }
}