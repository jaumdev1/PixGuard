using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Domain.Contracts;

namespace UserGuard.Api.Presentation;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IAppService<UserDto, CreateUserDto> _userAppService;

    public UserController(IAppService<UserDto, CreateUserDto> userAppService)
    {
        _userAppService = userAppService;
    }
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateUserDto createUserDto)
    {
        var userId = await _userAppService.Add(createUserDto);
        return Ok(userId);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetById(Guid id)
    {
        var userDto = await _userAppService.GetById(id);

        if (userDto == null)
        {
            return NotFound(); 
        }

        return Ok(userDto);
    }

    [HttpGet("all")]
    public async Task<List<UserDto>> GetAll()
    {
        var userList = await _userAppService.GetAll();
        return userList;
    }
}