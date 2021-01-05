using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using net_core_mssql.Data;
using net_core_mssql.Dtos.Character;
using net_core_mssql.Models;

namespace net_core_mssql.Services
{
  public class CharacterService : ICharacterService
  {
    private readonly IMapper mapper;
    private readonly DataContext context;
    private readonly IHttpContextAccessor httpContextAccessor;
    private int GetUserId() => int.Parse(this.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

    public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
    {
      this.httpContextAccessor = httpContextAccessor;
      this.context = context;
      this.mapper = mapper;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
    {
      ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
      Character character = mapper.Map<Character>(newCharacter);
      character.User = await context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

      await context.Characters.AddAsync(character);
      await context.SaveChangesAsync();

      serviceResponse.Data = (context.Characters.Include(c => c.Weapon).Where(c => c.User.Id == GetUserId()).Select(c => mapper.Map<GetCharacterDto>(c))).ToList();
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
    {
      ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
      try
      {
        Character character = 
            await context.Characters.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
        if (character != null)
        {
            context.Characters.Remove(character);
            await context.SaveChangesAsync();
            serviceResponse.Data = (context.Characters.Where(c => c.User.Id == GetUserId())
                .Select(c => mapper.Map<GetCharacterDto>(c))).ToList();
        }
        else
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Character not found.";
        }
      }
      catch (Exception ex)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
    {
      ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
      List<Character> dbCharacters = await context.Characters
        .Include(c => c.Weapon)
        .Where(c => c.User.Id == GetUserId()).ToListAsync();
      serviceResponse.Data = dbCharacters.Select(c => mapper.Map<GetCharacterDto>(c)).ToList();
      return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
    {
      ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
      Character dbCharacter = await context.Characters
                  .Include(c => c.Weapon)
                  .FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
      serviceResponse.Data = mapper.Map<GetCharacterDto>(dbCharacter);
      return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
    {
      ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
      try
      {
        // So keep that in mind, if you want to access related objects. You might have to include them first.
        Character character = await context.Characters
          .Include(c => c.User)
          .Include(c => c.Weapon)
          .FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
        if (character != null && character.User.Id == GetUserId())
        {
            character.Name = updatedCharacter.Name;
            character.Class = updatedCharacter.Class;
            character.Defense = updatedCharacter.Defense;
            character.HitPoints = updatedCharacter.HitPoints;
            character.Intelligence = updatedCharacter.Intelligence;
            character.Strength = updatedCharacter.Strength;

            context.Characters.Update(character);
            await context.SaveChangesAsync();

            serviceResponse.Data = mapper.Map<GetCharacterDto>(character);
        }
        else
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Character not found.";
        }
      }
      catch (Exception ex)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }


      return serviceResponse;
    }
  }
}