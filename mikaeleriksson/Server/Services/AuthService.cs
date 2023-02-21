
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using mikaeleriksson.Server.Data;
using mikaeleriksson.Server.Services.Interfaces;
using mikaeleriksson.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace mikaeleriksson.Server.Services;

public class AuthService : IAuthService
{
    private readonly FavoriteDbContext _peopleContext;
    private readonly IConfiguration _config;

    public AuthService(FavoriteDbContext peopleContext, IConfiguration config)
    {
        _peopleContext = peopleContext;
        _config = config;

    }

    public async Task<bool> CheckUserExistsAsync(string email)
    {
        return await _peopleContext.Users
            .AnyAsync(user => user.Email
            .ToLower()
            .Equals(email.ToLower()));
    }

    public async Task<ServiceResponse<string>> Login(string email, string password)
    {
        var response = new ServiceResponse<string>();


        if(!await CheckUserExistsAsync(email))
        {
            response.Success = false;
            response.Message = "Wrong user name or password! You BOZO";
            return response;
        }

        var user = await _peopleContext.Users.FirstAsync(u => u.Email == email);

        if(!VerifyPasswordHash(password,user.PasswordHash,user.PasswordSalt))
        {
            response.Success = false;
            response.Message = "Wrong user name or password! You BOZO";
            return response;
        }

        response.Success = true;
        response.Message = "Du är Cool";
        response.Data = CreateToken(user);

        return response;
    }

    private string CreateToken(UserModel user)
    {
        var claims = new List<Claim>()
        {
            new (ClaimTypes.NameIdentifier,user.Id.ToString()),
            new (ClaimTypes.Name,user.Email)

        };

        var secret = _config.GetSection("AppSettings:Token").Value;
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secret));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
        
            claims : claims,
            expires : DateTime.UtcNow.AddMinutes(15),
            signingCredentials : creds

        );

    var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;

    }

    public async Task<ServiceResponse<int>> RegisterUserAsync(UserModel user, string password)
    {
        if (await CheckUserExistsAsync(user.Email))
        {
            return new ServiceResponse<int>()
            {
                Success = false,
                Message = "User Already Exists"
            };
        }

        CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        await _peopleContext.Users.AddAsync(user);
        await _peopleContext.SaveChangesAsync();
        return new ServiceResponse<int>()
        {
            Data = user.Id,
            Success = true,
            Message = "Registration Successful!"
        };

    }
    private bool VerifyPasswordHash(string password, byte[] userPasswordHash, byte[] userPasswordSalt)
    {
        using (var hmac = new HMACSHA512(userPasswordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(userPasswordHash);
        }
    }


    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {

        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}
