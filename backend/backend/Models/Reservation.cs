using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace backend.Models;

[Table("reservations")]
[Index(nameof(DeskId), nameof(UserId), IsUnique = true)]
[Index(nameof(ReservedTo), nameof(ReservedFrom))]
public sealed class Reservation
{
    //we still use this id for easier control over cancellations
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key, Column("id")]
    public long Id { get; set; }
    
    [Required, Column("desk_id")]
    public long DeskId { get; set; }
    
    [ForeignKey(nameof(DeskId))]
    public required Desk Desk { get; set; }
    
    [Required, Column("user_id")]
    public long UserId { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public required User User { get; set; }

    [Required, Column("reserved_from")]
    public required DateTime ReservedFrom { get; set; }
    
    [Required, Column("reserved_to")]
    public required DateTime ReservedTo { get; set; }
}