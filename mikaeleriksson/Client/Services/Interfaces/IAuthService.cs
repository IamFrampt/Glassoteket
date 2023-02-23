using mikaeleriksson.Shared.DTOs.Login;

namespace mikaeleriksson.Client.Services.Interfaces;

public interface IAuthService
{
    Task<ServiceResponse<int>?> RegisterAsync(UserRegisterDto userDto);
    Task<ServiceResponse<string>?> LoginAsync(UserLoginDto userDto);
}
