using Domain.Entities;

namespace Domain.Services;

public static class UserService
{
    public static User ConvertHashPassword(User user)
    {

        var convertHashPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
        user.Password = convertHashPassword;
        return user;
    }
}