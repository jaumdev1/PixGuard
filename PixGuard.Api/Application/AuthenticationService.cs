using System.Security.Claims;
using Domain.Contracts;
using Domain.Entities;
using Domain.DTOs;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Org.BouncyCastle.Crypto.Generators;
using PixGuard.Api.Application.Contracts;
namespace PixGuard.Api.Application;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    
    public AuthenticationService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    
    public async Task<string> Authenticate(AuthDto authDto)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        
        var user = await _userRepository.GetByEmail(authDto.Email);

       if (user == null || !BCrypt.Net.BCrypt.Verify(authDto.Password, user.Password))
        {
            return null;
        }

        var identity = new ClaimsIdentity();
        identity.AddClaim(new Claim(ClaimTypes.Email, authDto.Email));
        
        var SecretKey = configuration.GetSection("AppSettings")["SecretKey"];
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
        
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "PixGuard",
            audience: "PixApiAuthentication",
            claims: identity.Claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}