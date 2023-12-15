
using Domain.DTOs;
using Domain.Entities;  
namespace PixGuard.Api.Application.Contracts.Mappers;
public class AssessmentMapper
{
    public AssessmentDto ToDto(Assessment entity)
    {
        return new AssessmentDto
        {
            Id = entity.Id,
            PixId = entity.PixId,
            UserId = entity.UserId,
            Comments = entity.Comments,
            Rate = entity.Rate,
            Number = entity.Number
        };
    }

    public Assessment ToEntity(CreateAssessmentDto dto)
    {
        return new Assessment
        {
            PixId = dto.PixId,
            UserId = dto.UserId,
            Comments = dto.Comments,
            Rate = dto.Rate,
            Number = dto.Number
        };
    }
}