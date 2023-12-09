using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Assessment
{
    [Key]
    public int PixId { get; set; } 

    [Key]
    [Required]
    [StringLength(255)] 
    public required string UserId { get; set; }
    [Required]
    [StringLength(500)] 
    public required string Comments { get; set; }

    [Required]
    [Range(1, int.MaxValue)] 
    public int Rate { get; set; }

    [Required]
    [Range(1, int.MaxValue)] 
    public int Number { get; set; }
    
    public required Pix Pix { get; set; }
    public required User User { get; set; }
}