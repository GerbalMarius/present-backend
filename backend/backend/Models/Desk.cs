using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

[Table("desks")] 
public sealed class Desk
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key, Column("id")]
    public long Id { get; set; }

    [Required, Column("is_in_maintenance")]
    public bool IsInMaintenance { get; set; } = false;

    public ICollection<Reservation> Reservations { get; set; } = [];
}