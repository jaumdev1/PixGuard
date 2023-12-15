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

        return Ok(new { token });
    }
    
    
}