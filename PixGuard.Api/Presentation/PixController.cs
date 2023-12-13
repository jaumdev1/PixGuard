using Microsoft.AspNetCore.Mvc;
using PixGuard.Api.Application.Contracts;
using Domain.Entities;
using Domain.Enumerables;
using Domain.DTOs;
using Domain.Contracts;
using Domain.Services;

namespace PixGuard.Api.Presentation;

[ApiController]
[Route("[controller]")]
public class PixController : ControllerBase
{
    private readonly IAppService<PixDto, CreatePixDto> _pixAppService;

    public PixController(IAppService<PixDto, CreatePixDto> pixAppService)
    {
        _pixAppService = pixAppService;
    }
    
    
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreatePixDto createPixDto)
    {
        _pixAppService.Add(createPixDto);
        return Ok("Pix created successfully");
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PixDto>> GetById(Guid id)
    {
        var pixDto = _pixAppService.GetById(id);

        if (pixDto == null)
        {
            return NotFound(); 
        }

        return Ok(pixDto);
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<PixDto>>> GetAll()
    {
        var pixList = _pixAppService.GetAll();
        return Ok(pixList);
    }
}