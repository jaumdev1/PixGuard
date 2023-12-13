using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public abstract class BaseEntity 
{
   [Key]
    public Guid Id { get; set; }

    public bool IsDeleted { get; set; } = false;
}