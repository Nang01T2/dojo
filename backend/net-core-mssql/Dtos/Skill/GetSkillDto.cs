using AutoMapper;
using net_core_mssql.Mappings;

namespace net_core_mssql.Dtos.Skill
{
    public class GetSkillDto: IMapFrom<net_core_mssql.Models.Skill>
    {
        public string Name { get; set; }
        public int Damage { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<net_core_mssql.Models.Skill, GetSkillDto>();
        }
    }
}