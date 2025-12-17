using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

[Table("desks")] 
public sealed class Desk
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key, Column("id")]
    public long Id { get; set; }
    
    [Required, Column("status_id")]
    public long DeskStatusId { get; set; }

    public required DeskStatus Status { get; set; }

    public ICollection<Reservation> Reservations { get; set; } = [];
}