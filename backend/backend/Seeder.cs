using backend.Migrations.Data;
using backend.Models;

namespace backend;

public static class Seeder
{
    public static void SeedInitialData(this IServiceProvider serviceProvider)
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        
        var db = scope.ServiceProvider.GetRequiredService<DeskDbContext>();
        db.Database.EnsureCreated();
        
        db.SaveChanges();
    }
}