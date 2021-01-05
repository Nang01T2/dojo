using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using net_core_mssql.Data;
using net_core_mssql.Dtos.Character;
using net_core_mssql.Dtos.Weapon;
using net_core_mssql.Models;

namespace net_core_mssql.Services
{
  public class WeaponService : IWeaponService
  {
    private readonly DataContext context;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly IMapper mapper;
    private int GetUserId() => int.Parse(this.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
    public WeaponService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
      this.mapper = mapper;
      this.httpContextAccessor = httpContextAccessor;
      this.context = context;
    }
    public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon)
    {
      ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();
      try
      {
        Character character = await context.Characters
                                .FirstOrDefaultAsync(c => c.Id == newWeapon.CharacterId && c.User.Id == GetUserId());
        if (character == null)
        {
          response.Success = false;
          response.Message = "Character not found.";
        } else {
            Weapon weapon = new Weapon
            {
              Name = newWeapon.Name,
              Damage = newWeapon.Damage,
              Character = character
            };
            await context.Weapons.AddAsync(weapon);
            await context.SaveChangesAsync();
            
            response.Data = mapper.Map<GetCharacterDto>(character);
        }
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