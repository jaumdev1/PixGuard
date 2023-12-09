using Microsoft.AspNetCore.Mvc;

namespace PixGuard.Api.Presentation;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    [HttpGet]
    [Route("/user")]
    public async Task<string> Index()
    {
        string message = "online";
        return await Task.FromResult(message);
    }
}