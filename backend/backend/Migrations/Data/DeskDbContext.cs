using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Migrations.Data;

public sealed class DeskDbContext(DbContextOptions<DeskDbContext> options) : DbContext(options)
{
    public DbSet<Desk> Desks => Set<Desk>();
    
    public DbSet<Reservation> Reservations => Set<Reservation>();
    
    public DbSet<User> Users => Set<User>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reservation>()
            .HasKey(reservation => new { reservation.DeskId, reservation.UserId });
        
        modelBuilder.Entity<Reservation>()
            .Property(reservation => reservation.ReservedFrom)
            .HasConversion(
                date => TimeZoneInfo.ConvertTimeToUtc(date), 
                date => TimeZoneInfo.ConvertTimeFromUtc(date, TimeZoneInfo.Local)
                );

        modelBuilder.Entity<Reservation>()
            .Property(reservation => reservation.ReservedTo)
            .HasConversion(
                date => TimeZoneInfo.ConvertTimeToUtc(date), 
                date => TimeZoneInfo.ConvertTimeFromUtc(date, TimeZoneInfo.Local)
            );
    }
}