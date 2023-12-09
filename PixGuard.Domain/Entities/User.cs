using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Domain.Entities;

public class User
{
    [Key]
    public required string UserId { get; set; } 

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    public required string FirstName { get; set; }

    [Required]
    public required string LastName { get; set; }

    [Required]
    [MinLength(6)]
    public required string Password { get; set; } 
}