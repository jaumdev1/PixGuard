using PixGuard.Api.Application.Contracts;
using Domain.Entities;
using Domain.DTOs;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Services;
using PixGuard.Api.Application.Contracts.Mappers;

namespace PixGuard.Api.Application;

public class UserAppService:  IAppService<UserDto, CreateUserDto>
{
    private IUserRepository _userRepository;
    private readonly UserMapper _userMapper;
    public UserAppService(IUserRepository userRepository, UserMapper userMapper)
    {
        _userRepository = userRepository;
        _userMapper = userMapper;
      
    }
    public async Task<UserDto> GetById(Guid id)
    {
        var user = await _userRepository.GetById(id);
        
        var userDto =  _userMapper.ToDto(user);
      
        return userDto;
    }

    public async Task<List<UserDto>> GetAll()
    {
        var userList = await _userRepository.GetAll();

        if (userList == null)
        {
            throw new Exception("Could not  get all objects from userRepository");
        }
        
        var usersDto = userList.Select(user => _userMapper.ToDto(user)).ToList();
        return usersDto;
    }

    public async Task<Guid> Add(CreateUserDto createDto)
    {
        
            var user = _userMapper.ToEntity(createDto);
        
            var userExists = await _userRepository.GetByEmail(user.Email);
            if (userExists != null)
            {
                throw new UserExistsException("User already exists");
            }
            
            user = UserService.ConvertHashPassword(user);
          
            await _userRepository.Add(user);

            return user.Id;
      
       
    }
    
  
}