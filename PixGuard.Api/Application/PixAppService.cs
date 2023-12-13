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

    public async Task<List<PixDto>> GetAll()
    {
        var pixList = await _pixRepository.GetAll();

        if (pixList == null)
        {
            throw new Exception("Could not  get all PixDto objects from PixRepository");
        }
        
        var pixsDto = pixList.Select(pix => _pixMapper.ToDto(pix)).ToList();
        return pixsDto;
    }

    public async Task<Guid> Add(CreatePixDto createDto)
    {
        
            var pix = _pixMapper.ToEntity(createDto);

            await _pixRepository.Add(pix);

            return pix.Id;
      
       
    }
    
  
}