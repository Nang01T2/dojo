using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using net_core_mssql.Dtos.Skill;
using net_core_mssql.Dtos.Weapon;
using net_core_mssql.Mappings;
using net_core_mssql.Models;

namespace net_core_mssql.Dtos.Character
{
    public class GetCharacterDto: IMapFrom<net_core_mssql.Models.Character>
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Frodo";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public RpgClass Class { get; set; } = RpgClass.Knight;
        public GetWeaponDto Weapon { get; set; }
        public List<GetSkillDto> Skills { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<net_core_mssql.Models.Character, GetCharacterDto>()
                .ForMember(dto => dto.Skills, c => c.MapFrom(c => c.CharacterSkills.Select(cs => cs.Skill)));
        }
    }
}