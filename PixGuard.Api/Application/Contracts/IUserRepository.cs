using Domain.DTOs;
using Domain.Entities;
namespace PixGuard.Api.Application.Contracts;

public interface IUserRepository: IRepository<User>
{
    Task<User> GetByEmail(string email);
   
}