using System;
using System.Linq;
using AutoMapper;
using net_core_mssql.Dtos.Character;
using net_core_mssql.Dtos.Skill;
using net_core_mssql.Dtos.Weapon;
using net_core_mssql.Models;

namespace net_core_mssql
{
  public class AutoMapperProfile : Profile
  {
    public AutoMapperProfile()
    {
      CreateMap<AddCharacterDto, Character>();
    }
  }
}