using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Domain.Entities;

public class User : BaseEntity
{

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
    
    public required string PhoneNumber { get; set; }
    public ICollection<Pix> PixKeys { get; set; }
    
    
}