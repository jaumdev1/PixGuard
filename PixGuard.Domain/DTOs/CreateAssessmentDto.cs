using System.ComponentModel.DataAnnotations;
using Domain.DTOs.Validations;
using Domain.Enumerables;
namespace Domain.DTOs;

public class CreateAssessmentDto
{
    private readonly List<double> validRates = new List<double> { 1, 1.5, 2, 2.5, 3, 3.5, 4, 4.5, 5 };

    private double Rating;

    [Required]
    public Guid PixId { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    [StringLength(500)]
    public string Comments { get; set; }

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
    [Range(1, int.MaxValue)]
    public int Number { get; set; }
}
