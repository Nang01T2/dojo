using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using net_core_mssql.Data;
using net_core_mssql.Dtos.Character;
using net_core_mssql.Dtos.CharacterSkill;
using net_core_mssql.Models;

namespace net_core_mssql.Services
{
  public class CharacterSkillService : ICharacterSkillService
  {
    private readonly IMapper mapper;
    private readonly DataContext context;
    private readonly IHttpContextAccessor httpContextAccessor;
    private int GetUserId() => int.Parse(this.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
    public CharacterSkillService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
    {
      this.httpContextAccessor = httpContextAccessor;
      this.context = context;
      this.mapper = mapper;

    }
    public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
    {
      ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();
      try
      {
        Character character = await context.Characters
          .Include(c => c.Weapon)
          .Include(c => c.CharacterSkills).ThenInclude(cs => cs.Skill)
          .FirstOrDefaultAsync(c => c.Id == newCharacterSkill.CharacterId && c.User.Id == GetUserId());

        if (character == null)
        {
          response.Success = false;
          response.Message = "Character not found.";
          return response;
        }

        Skill skill = await context.Skills
            .FirstOrDefaultAsync(s => s.Id == newCharacterSkill.SkillId);
        if (skill == null)
        {
          response.Success = false;
          response.Message = "Skill not found.";
          return response;
        }

        CharacterSkill characterSkill = new CharacterSkill
        {
          Character = character,
          Skill = skill
        };

        await context.CharacterSkills.AddAsync(characterSkill);
        await context.SaveChangesAsync();

        response.Data = mapper.Map<GetCharacterDto>(character);
      }
      catch (Exception ex)
      {
        response.Success = false;
        response.Message = ex.Message;
      }
      return response;
    }
  }
}