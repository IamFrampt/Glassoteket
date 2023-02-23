using System.ComponentModel.DataAnnotations;

namespace mikaeleriksson.Shared.DTOs.Login;

public class UserLoginDto
{
    [EmailAddress, Required]
    public string Email { get; set; }
    [Required(ErrorMessage = "Password can not be empty!")]
    public string Password { get; set; }

}
