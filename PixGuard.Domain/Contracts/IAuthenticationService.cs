using Domain.DTOs;

namespace Domain.Contracts;

public interface IAuthenticationService
{
    Task<string> Authenticate(AuthDto user);
}