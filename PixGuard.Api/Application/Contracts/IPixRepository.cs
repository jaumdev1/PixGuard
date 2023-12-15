using Domain.Entities;
namespace PixGuard.Api.Application.Contracts;

public interface IPixRepository: IRepository<Pix>
{
    Task<List<Pix>> GetByValue(string value);
   
}