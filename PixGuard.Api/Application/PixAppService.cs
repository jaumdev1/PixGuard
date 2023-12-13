using PixGuard.Api.Application.Contracts;
using Domain.Entities;
using Domain.DTOs;
using Domain.Contracts;
using PixGuard.Api.Application.Contracts.Mappers;

namespace PixGuard.Api.Application;

public class PixAppService:  IAppService<PixDto, CreatePixDto>
{
    private readonly IRepository<Pix> _pixRepository;
    private readonly PixMapper _pixMapper;
    public PixAppService(IRepository<Pix> pixRepository, PixMapper pixMapper)
    {
        _pixRepository = pixRepository;
        _pixMapper = pixMapper;
      
    }
    public async Task<PixDto> GetById(Guid id)
    {
        var pix = await _pixRepository.GetById(id);
        
        _pixMapper.ToDto(pix);
        
        var pixDto =  _pixMapper.ToDto(pix);
        if (pix == null)
        {
            return null;
        }
      
        return pixDto;
    }


    public  async Task<IEnumerable<PixDto>> GetAll()
    {
        var pix = await _pixRepository.GetAll();
        
        var pixsDto = new List<PixDto>();
        if (pix == null)
        {
            return null;
        }
        return pixsDto;
    }

    public async Task<bool> Add(CreatePixDto createDto)
    {
        var pix = await _pixRepository.GetAll();
        if (pix == null)
        {
            return false;
        }

        return true;
    }
    
  
}