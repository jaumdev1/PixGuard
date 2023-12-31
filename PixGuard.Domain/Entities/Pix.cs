using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.DTOs;
using Domain.Enumerables;

namespace Domain.Entities;

public class Pix: BaseEntity
{

    [Required]
    public  KeyType KeyType { get; set; }

    [Required] 
    public  string KeyValue { get; set; }
    public  Guid? UserId { get; set; }
    [ForeignKey("UserId")]
    public  User? User { get; set; }
    
  
}