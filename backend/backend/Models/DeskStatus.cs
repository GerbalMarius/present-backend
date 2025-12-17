using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace backend.Models;

[Table("desk_statuses")]  
[Index(nameof(Name), IsUnique = true)]
public sealed class DeskStatus
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key, Column("id")]
    public long Id { get; set; }
    
    [Required, Column("name")]
    [MaxLength(80)]
    public required string Name { get; set; }

    public ICollection<Desk> Desks { get; set; } = [];
}