using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enumerables;
namespace Domain.DTOs;

public class CreateUserDto
{
    [Required] [EmailAddress] public string Email { get; set; }

    [Required] public string FirstName { get; set; }

    [Required] public string LastName { get; set; }
    [Required]
    [MinLength(6)]
    public required string Password { get; set; } 
    
    [Required]
    [MinLength(6)]
    public required string ConfirmPassword { get; set; } 

}
