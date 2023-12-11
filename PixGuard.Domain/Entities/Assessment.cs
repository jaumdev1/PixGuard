using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Assessment : BaseEntity
{
    [Required]
  
    public required Guid  PixId { get; set; } 

    [Required]
    [StringLength(255)] 
    [ForeignKey("UserId")]
    public required Guid UserId { get; set; }
    [Required]
    [StringLength(500)] 
    public required string Comments { get; set; }

    [Required]
    [Range(1, int.MaxValue)] public int Rate { get; set; }

    [Required]
    [Range(1, int.MaxValue)] 
    public int Number { get; set; }
    [ForeignKey("PixId")]
    public required Pix Pix { get; set; }
    [ForeignKey("UserId")]
    public required User User { get; set; }
    
}