using backend.Migrations.Data;
using backend.Models;

namespace backend;

public static class Seeder
{
    private static readonly List<DeskStatus> InitStatuses =
    [
        new() { Id = 1, Name = "Open" },
        new() { Id = 2, Name = "Reserved" },
        new() { Id = 3, Name = "Maintenance" }
    ];
    
    public static void SeedInitialData(this IServiceProvider serviceProvider)
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        
        var db = scope.ServiceProvider.GetRequiredService<DeskDbContext>();
        db.Database.EnsureCreated();
        
        SeedDeskStatuses(db);
        
        db.SaveChanges();
    }

    private static void SeedDeskStatuses(DeskDbContext db)
    {
        db.DeskStatuses.AddRange(InitStatuses);
    }
}