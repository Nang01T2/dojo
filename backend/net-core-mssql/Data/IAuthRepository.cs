using System.Threading.Tasks;
using net_core_mssql.Models;
using net_core_mssql.Services;

namespace net_core_mssql.Data
{
  public interface IAuthRepository
  {
    Task<ServiceResponse<int>> Register(User user, string password);
    Task<ServiceResponse<string>> Login(string username, string password);
    Task<bool> UserExists(string username);
  }
}