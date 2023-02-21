

using mikaeleriksson.Server.Data;
using mikaeleriksson.Shared;

namespace mikaeleriksson.Server.Services.Interfaces;

public interface IAuthService
{
    Task<ServiceResponse<int>> RegisterUserAsync(UserModel user, string password);
    Task<bool> CheckUserExistsAsync(string email);
    Task<ServiceResponse<string>> Login(string email, string password);
}
