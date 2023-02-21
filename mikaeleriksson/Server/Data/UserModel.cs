namespace mikaeleriksson.Server.Data;

public class UserModel
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public DateTime DateOfCreation { get; set; } = DateTime.UtcNow;
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
}
