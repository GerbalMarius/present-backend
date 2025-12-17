using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

[Table("reservations")]
public sealed class Reservation
{
    
    [Required, Column("desk_id")]
    public long DeskId { get; set; }
    
    [ForeignKey(nameof(DeskId))]
    public required Desk Desk { get; set; }
    
    [Required, Column("user_id")]
    public long UserId { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public required User User { get; set; }

    [Required, Column("reserved_at")]
    public DateTime ReservedAt { get; set; } = DateTime.UtcNow;
}