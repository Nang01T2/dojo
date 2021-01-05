using System.Threading.Tasks;
using net_core_mssql.Dtos.Character;
using net_core_mssql.Dtos.CharacterSkill;

namespace net_core_mssql.Services
{
    public interface ICharacterSkillService
    {
         Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill);
    }
}