using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace backend.Models;

[Table("users")]
[Index(nameof(LastName))]
[Index(nameof(Email), IsUnique = true)]
public sealed class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key, Column("id")]
    public long Id { get; set; }
    
    [Required, Column("email")]
    [MaxLength(120)]
    public required string Email { get; set; }
    
    [Required, Column("password")]
    [MaxLength(80)]
    public required string Password { get; set; }

    [Required, Column("first_name")]
    [MaxLength(80)]
    public required string FirstName { get; set; }
    
    [Required, Column("last_name")]
    [MaxLength(120)]
    public required string LastName { get; set; }
    
    public ICollection<Reservation> Reservations { get; set; } = [];
}