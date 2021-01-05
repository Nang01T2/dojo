using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using net_core_mssql.Data;
using net_core_mssql.Dtos.User;
using net_core_mssql.Models;
using net_core_mssql.Services;

namespace net_core_mssql.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AuthController : ControllerBase
  {
    private readonly IAuthRepository authRepo;
    public AuthController(IAuthRepository authRepository)
    {
      this.authRepo = authRepository;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(UserRegisterDto request)
    {
      ServiceResponse<int> response = await authRepo.Register(
      new User { Username = request.Username }, request.Password);
      if (!response.Success)
      {
        return BadRequest(response);
      }
      return Ok(response);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(UserLoginDto request)
    {
      ServiceResponse<string> response = await authRepo.Login(
          request.Username, request.Password);
      if (!response.Success)
      {
        return BadRequest(response);
      }
      return Ok(response);
    }
  }
}