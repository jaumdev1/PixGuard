using Microsoft.AspNetCore.Mvc;
using PixGuard.Api.Application.Contracts;
using Domain.Entities;
using Domain.Enumerables;
using Domain.DTOs;
using Domain.Contracts;
using Domain.Services;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace PixGuard.Api.Presentation;

[ApiController]
[Route("[controller]")]
public class AssessmentController : ControllerBase
{
    private readonly IAppService<AssessmentDto, CreateAssessmentDto> _assessmentAppService;

    public AssessmentController(IAppService<AssessmentDto, CreateAssessmentDto> assessmentAppService)
    {
        _assessmentAppService = assessmentAppService;
    }
    
    
    [HttpPatch]
    
    public async Task<ActionResult<Guid>> Update([FromBody] AssessmentDto assessmentDto)
    {
       
        return Ok();
    }

    [HttpPost]
    
    public async Task<ActionResult<Guid>> Create([FromBody] CreateAssessmentDto createAssessmentDTO)
    {
        var assessmentId = await _assessmentAppService.Add(createAssessmentDTO);
        return Ok(assessmentId);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<AssessmentDto>> GetById(Guid id)
    {
        var assessmentDto = await _assessmentAppService.GetById(id);

        if (assessmentDto == null)
        {
            return NotFound(); 
        }

        return Ok(assessmentDto);
    }

    [HttpGet]
    [Authorize]
    public async Task<List<AssessmentDto>> GetAll()
    {
        var assessmentList = await _assessmentAppService.GetAll();
        return assessmentList;
    }
}