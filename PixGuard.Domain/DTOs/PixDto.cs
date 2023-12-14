using System.ComponentModel.DataAnnotations;
using Domain.DTOs.Validations;
using Domain.Enumerables;
namespace Domain.DTOs;

public class PixDto
{
    public Guid Id { get; set; }
    
    [Required]
    [ValidEnumValue(typeof(KeyType), ErrorMessage = "Valor inválido para KeyType.")]
    public KeyType KeyType { get; set; }

    [Required]
    public string KeyValue { get; set; }

    public Guid? UserId { get; set; }
    
   
}
