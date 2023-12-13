using System.ComponentModel.DataAnnotations;
using Domain.Enumerables;

namespace Domain.DTOs;

public class UserDto
{
    public Guid Id { get; set; }

    [Required] [EmailAddress] public string Email { get; set; }

    [Required] public string FirstName { get; set; }

    [Required] public string LastName { get; set; }
}

