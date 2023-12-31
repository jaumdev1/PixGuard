using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Assessment : BaseEntity
{
    private readonly List<double> validRates = new List<double> { 1, 1.5, 2, 2.5, 3, 3.5, 4, 4.5, 5 };
    
    private double Rating;
    [Required]

    public required Guid  PixId { get; set; } 

    [Required]
  

    public required Guid UserId { get; set; }
    [Required]
    [StringLength(500)] 
    public required string Comments { get; set; }

    [Required(ErrorMessage = "Rate must be a valid value (1, 1.5, 2, 2.5, ..., 5).")]
    public double Rate
    {
        get { return Rating; }
        set
        {
            if (!validRates.Contains(value))
            {
                throw new ArgumentException("Rate must be a valid value (1, 1.5, 2, 2.5, ..., 5).", nameof(Rate));
            }

            Rating = value;
        }
    }

    [Required]
    [Range(1, 5)] 
    public int Number { get; set; }
    
    [ForeignKey("PixId")]
    public  Pix Pix { get; set; }
    [ForeignKey("UserId")]
    public  User User { get; set; }
    
}