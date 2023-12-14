using Microsoft.AspNetCore.Mvc;
using PixGuard.Api.Application.Contracts;
using Domain.Entities;
using Domain.Enumerables;
using Domain.DTOs;
using Domain.Contracts;
using Domain.Services;
using System.Collections.Generic;
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
    
    public async Task<ActionResult<Guid>> Create([FromBody] CreatePixDto createPixDto)
    {
        var pixId = await _pixAppService.Add(createPixDto);
        return Ok(pixId);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PixDto>> GetById(Guid id)
    {
        var pixDto = await _pixAppService.GetById(id);

        if (pixDto == null)
        {
            return NotFound(); 
        }

        return Ok(pixDto);
    }

    [HttpGet("all")]
    public async Task<List<PixDto>> GetAll()
    {
        var pixList = await _pixAppService.GetAll();
        return pixList;
    }
}