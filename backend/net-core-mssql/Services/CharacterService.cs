using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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

    public CharacterService(IMapper mapper, DataContext context)
    {
      this.context = context;
      this.mapper = mapper;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
    {
      ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
      Character character = mapper.Map<Character>(newCharacter);

      await context.Characters.AddAsync(character);
      await context.SaveChangesAsync();
      serviceResponse.Data = (context.Characters.Select(c => mapper.Map<GetCharacterDto>(c))).ToList();
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
    {
      ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
      try
      {
        Character character = await context.Characters.FirstAsync(c => c.Id == id);
        context.Characters.Remove(character);
        await context.SaveChangesAsync();
        serviceResponse.Data = (context.Characters.Select(c => mapper.Map<GetCharacterDto>(c))).ToList();
      }
      catch (Exception ex)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters(int userId)
    {
      ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
      List<Character> dbCharacters = await context.Characters.Where(c => c.User.Id == userId).ToListAsync();
      serviceResponse.Data = dbCharacters.Select(c => mapper.Map<GetCharacterDto>(c)).ToList();
      return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
    {
      ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
      Character dbCharacter = await context.Characters.FirstOrDefaultAsync(c => c.Id == id);
      serviceResponse.Data = mapper.Map<GetCharacterDto>(dbCharacter);
      return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
    {
      ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
      try
      {
        Character character = await context.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
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
      catch (Exception ex)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }


      return serviceResponse;
    }
  }
}