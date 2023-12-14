using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enumerables;
using Domain.DTOs.Validations;
namespace Domain.DTOs;

public class CreatePixDto
{
    [Required]
    [ValidEnumValue(typeof(KeyType), ErrorMessage = "Valor inválido para KeyType.")]
    public KeyType KeyType { get; set; }

    [Required]
    public string KeyValue { get; set; }

    public Guid? UserId { get; set; }
}

