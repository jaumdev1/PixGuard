using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enumerables;
namespace Domain.DTOs;

public class CreatePixDto
{
    [Required]
    public KeyType KeyType { get; set; }

    [Required]
    public string KeyValue { get; set; }

    public Guid? UserId { get; set; }
    
    
}
