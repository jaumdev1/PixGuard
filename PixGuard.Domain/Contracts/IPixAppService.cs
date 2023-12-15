using Domain.DTOs;
namespace Domain.Contracts;

public interface IPixAppService :IAppService<PixDto, CreatePixDto>
{
    Task<List<PixDto>> GetByValue(string value);

}