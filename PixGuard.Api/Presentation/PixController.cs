using Microsoft.AspNetCore.Mvc;
using PixGuard.Api.Application.Contracts;
using Domain.Entities;
using Domain.Enumerables;
namespace PixGuard.Api.Presentation;

[ApiController]
[Route("[controller]")]
public class PixController : ControllerBase
{
    private readonly IRepository<Pix> _repository;

    public PixController(IRepository<Pix> repository)
    {
        _repository = repository;
    }
    [HttpPost]
    [Route("/pix")]
    public async Task<ActionResult> Create(Pix pix)
    {
    
        await _repository.Add(pix);
        return CreatedAtAction(nameof(GetById), new { id = pix.Id }, pix);
    }

    [HttpGet]
    [Route("/pix/{id}")]
    public async Task<ActionResult<Pix>> GetById(int id)
    {
        var pix = await _repository.GetById(id);
        if (pix == null)
        {
            return NotFound();
        }
        return pix;
    }
}