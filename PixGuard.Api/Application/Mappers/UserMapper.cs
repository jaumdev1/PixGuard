namespace PixGuard.Api.Application.Contracts.Mappers;
using Domain.DTOs;
using Domain.Entities;  
public  class UserMapper
{
    public  UserDto ToDto(User entity)
    {
        return new UserDto
        {
            Id = entity.Id,
            Email = entity.Email,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
    
        };
    }

    public  User ToEntity(CreateUserDto dto)
    {
        return new User
        {
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Password  = dto.Password,
        };
    }
}
