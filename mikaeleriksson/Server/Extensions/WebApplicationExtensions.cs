
using Microsoft.AspNetCore.Mvc;
using mikaeleriksson.Server.Data;
using mikaeleriksson.Server.Services.Interfaces;
using mikaeleriksson.Shared.DTOs.Login;
using System.ComponentModel.DataAnnotations;

namespace mikaeleriksson.Server.Extensions;
public static class WebApplicationExtensions
{
    public static WebApplication MapAuthEndPoints(this WebApplication app)
    {
        app.MapPost("user/Register", RegisterHandler);
        app.MapPost("user/Login", LoginHandler);
        return app;
    }

    private static async Task<IResult> LoginHandler(IAuthService authService, UserLoginDto user)
    {
        var response = await authService.Login(user.Email,user.Password);

        return response.Success ? Results.Ok(response) : Results.BadRequest(response);
    }

    private static async Task<IResult> RegisterHandler(IAuthService authService, UserRegisterDto userDto)
    {
        var user = new UserModel() { Email= userDto.Email };

        var response = await authService.RegisterUserAsync(user,userDto.Password);

        return response.Success ? Results.Ok(response) : Results.BadRequest(response);

    }
}
