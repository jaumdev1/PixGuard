namespace PixGuard.Api.Application.Contracts.Mappers;
using Domain.DTOs;
using Domain.Entities;  
public class PixMapper
{
    public PixDto ToDto(Pix entity)
    {
        return new PixDto
        {
            Id = entity.Id,
            KeyType = entity.KeyType,
            KeyValue = entity.KeyValue,
            UserId = entity.UserId
        };
    }

    public Pix ToEntity(CreatePixDto dto)
    {
        return new Pix
        {
            KeyType = dto.KeyType,
            KeyValue = dto.KeyValue,
            UserId = dto.UserId
        };
    }
}
