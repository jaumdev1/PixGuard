using Microsoft.AspNetCore.Mvc;
using PixGuard.Api.Application.Contracts;
using Domain.Entities;


namespace PixGuard.Api.Presentation;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IRepository<User> _repository;

    public UserController(IRepository<User> repository)
    {
        _repository = repository;
    }
    [HttpPost]
    [Route("/user")]
    public async Task<ActionResult>Create(User user)
    {
        await _repository.Add(user);
        return CreatedAtAction(nameof(GetById),new { id = user.Id }, user);
    }
    [HttpGet]
    [Route("/user/{id}")]
    public async Task<ActionResult<User>> GetById(Guid id)
    {
        var user = await _repository.GetById(id);
        if (user == null)
        {
            return NotFound();
        }
        return user;
    }
}