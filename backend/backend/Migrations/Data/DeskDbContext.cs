using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Migrations.Data;

public sealed class DeskDbContext(DbContextOptions<DeskDbContext> options) : DbContext(options)
{
    public DbSet<DeskStatus> DeskStatuses => Set<DeskStatus>();
    
    public DbSet<Desk> Desks => Set<Desk>();
    
    public DbSet<Reservation> Reservations => Set<Reservation>();
    
    public DbSet<User> Users => Set<User>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reservation>()
            .HasKey(reservation => new { reservation.DeskId, reservation.UserId });
    }
}