using mikaeleriksson.Server.Data.Model;
using mikaeleriksson.Shared.DTOs.Login;

namespace mikaeleriksson.Server.Services.Interfaces;

public interface IAuthService
{
    Task<ServiceResponse<int>> RegisterUserAsync(UserModel user, string password);
    Task<bool> CheckUserExistsAsync(string email);
    Task<ServiceResponse<string>> Login(string email, string password);
}
