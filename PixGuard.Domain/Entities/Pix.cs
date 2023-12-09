using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enumerables;

namespace Domain.Entities;

public class Pix
{
    [Key]
    public required string PixId { get; set; }

    [Required]
    public required KeyType KeyType { get; set; }

    [Required] public required string KeyValue { get; set; }

    [Required] public required string UserId { get; set; }

    [Required]
    public required User User { get; set; }
}