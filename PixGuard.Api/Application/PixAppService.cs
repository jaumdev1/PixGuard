using PixGuard.Api.Application.Contracts;
using Domain.Entities;
using Domain.DTOs;
using Domain.Contracts;
using Domain.Exceptions;
using PixGuard.Api.Application.Contracts.Mappers;

namespace PixGuard.Api.Application;

public class PixAppService:  IPixAppService
{
    private readonly IPixRepository _pixRepository;
    private readonly PixMapper _pixMapper;
    public PixAppService(IPixRepository pixRepository, PixMapper pixMapper)
    {
        _pixRepository = pixRepository;
        _pixMapper = pixMapper;
      
    }
    public async Task<PixDto> GetById(Guid id)
    {
        var pix = await _pixRepository.GetById(id);
        
        var pixDto =  _pixMapper.ToDto(pix);
       
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
    
    public async Task<List<PixDto>> GetByValue(string value)
    {
        var pixList = await _pixRepository.GetByValue(value);
        var pixsDto = pixList.Select(pix => _pixMapper.ToDto(pix)).ToList();
        return pixsDto;
    }
}