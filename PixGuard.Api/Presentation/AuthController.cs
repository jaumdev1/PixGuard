using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
namespace PixGuard.Api.Presentation;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost]
    public async Task<ActionResult<string>> Auth([FromBody] AuthDto authDto)
    {
        var token = await _authenticationService.Authenticate(authDto);

        if (token == null)
            return Unauthorized();
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,  
            Secure = true,
            SameSite = SameSiteMode.Strict,
            MaxAge = TimeSpan.FromDays(1)  
        };
        Response.Headers.Add("Authorization", $"Bearer {token}");
        Response.Cookies.Append("Token", token, cookieOptions);
        return Ok(new { token });
    }
    
    
}