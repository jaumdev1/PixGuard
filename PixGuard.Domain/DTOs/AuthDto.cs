using System.ComponentModel.DataAnnotations;
namespace Domain.DTOs;

public class AuthDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [MinLength(6)]
    public string Password { get; set; }
    
}