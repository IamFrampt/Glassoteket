using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace mikaeleriksson.Shared.DTOs.Login;

public class UserRegisterDto
{
    [EmailAddress, Required]
    public string Email { get; set; }
    [Required, StringLength(100, MinimumLength = 6)]
    public string Password { get; set; }

    [Compare("Password")]
    public string PasswordConfirm { get; set; }


}
