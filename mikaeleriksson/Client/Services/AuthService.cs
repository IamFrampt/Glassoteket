
using mikaeleriksson.Client.Services.Interfaces;
using mikaeleriksson.Shared.DTOs.Login;
using System.Net.Http.Json;

namespace mikaeleriksson.Client.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _client;

    public AuthService(HttpClient client)
    {
        _client = client;
    }



    public async Task<ServiceResponse<string>?> LoginAsync(UserLoginDto userDto)
    {
        var response = await _client.PostAsJsonAsync("user/Login", userDto);
        return await response.Content.ReadFromJsonAsync<ServiceResponse<string>>();
    }

    public async Task<ServiceResponse<int>?> RegisterAsync(UserRegisterDto userDto)
    {
        var response = await _client.PostAsJsonAsync("user/Register",userDto);
        return await response.Content.ReadFromJsonAsync<ServiceResponse<int>>();
    }
}
