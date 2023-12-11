using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public abstract class BaseEntity 
{
   [Key]
    public Guid Id { get; set; }

    public bool IsDeleted { get; set; } = false;
}