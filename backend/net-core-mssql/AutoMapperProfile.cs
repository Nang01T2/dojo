using System.Linq;
using AutoMapper;
using net_core_mssql.Dtos.Character;
using net_core_mssql.Dtos.Skill;
using net_core_mssql.Dtos.Weapon;
using net_core_mssql.Models;

namespace net_core_mssql
{
  public class AutoMapperProfile: Profile 
  {
    public AutoMapperProfile()
    {
        CreateMap<Character, GetCharacterDto>()
          .ForMember(dto => dto.Skills, c => c.MapFrom(c => c.CharacterSkills.Select(cs => cs.Skill)));
        CreateMap<AddCharacterDto, Character>();
        CreateMap<Weapon, GetWeaponDto>();
        CreateMap<Skill, GetSkillDto>();
    }
  }
}