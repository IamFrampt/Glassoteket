﻿using mikaeleriksson.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using mikaeleriksson.Shared.DTOs.Login;

namespace mikaeleriksson.Client.Pages.Login;

partial class RegisterUser : ComponentBase
{
    [Inject]
    private IAuthService AuthService { get; set; }

    UserRegisterDto _user = new UserRegisterDto();

    string _message = string.Empty;
    string _messageCssClass = string.Empty;

    async Task HandleSubmit()
    {
        var result = await AuthService.RegisterAsync(_user);
        _message = result.Message;

        _messageCssClass = result.Success ? "text-success" : "text-danger";
    }
}