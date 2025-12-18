using backend.Migrations.Data;
using backend.Models;

namespace backend;

public static class Seeder
{
    public static void SeedInitialData(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<DeskDbContext>();
        db.Database.EnsureCreated();

        var todayStart = DateTime.Now;
        var tomorrowStart = todayStart.AddDays(1);

        // --- USERS ---
        List<User> users =
        [
            new() { Email = "alice@demo.com", Password = "pass", FirstName = "Alice", LastName = "Anders" },
            new() { Email = "bob@demo.com", Password = "pass", FirstName = "Bob", LastName = "Baker" },
            new() { Email = "cara@demo.com", Password = "pass", FirstName = "Cara", LastName = "Carter" },
            new() { Email = "drew@demo.com", Password = "pass", FirstName = "Drew", LastName = "Doe" }
        ];

        db.Users.AddRange(users);

        // --- DESKS ---
        List<Desk> desks =
        [
            new() { IsInMaintenance = false },
            new() { IsInMaintenance = true },
            new() { IsInMaintenance = false },
            new() { IsInMaintenance = false },
            new() { IsInMaintenance = false },
            new() { IsInMaintenance = false },
            new() { IsInMaintenance = false }
        ];

        db.Desks.AddRange(desks);

        db.SaveChanges();

        // --- RESERVATIONS ---
        List<Reservation> reservations =
        [
            new()
            {
                DeskId = desks[2].Id, Desk = desks[2],
                UserId = users[0].Id, User = users[0],
                ReservedFrom = todayStart.AddHours(9),
                ReservedTo = todayStart.AddHours(17)
            },


            new()
            {
                DeskId = desks[3].Id, Desk = desks[3],
                UserId = users[1].Id, User = users[1],
                ReservedFrom = todayStart.AddHours(-1),
                ReservedTo = todayStart.AddHours(2)
            },


            new()
            {
                DeskId = desks[4].Id, Desk = desks[4],
                UserId = users[2].Id, User = users[2],
                ReservedFrom = todayStart.AddDays(-1).AddHours(10),
                ReservedTo = todayStart.AddDays(-1).AddHours(12)
            },


            new()
            {
                DeskId = desks[5].Id, Desk = desks[5],
                UserId = users[3].Id, User = users[3],
                ReservedFrom = tomorrowStart.AddHours(10),
                ReservedTo = tomorrowStart.AddHours(12)
            },


            new()
            {
                DeskId = desks[6].Id, Desk = desks[6],
                UserId = users[2].Id, User = users[2],
                ReservedFrom = todayStart.AddHours(-2), // yesterday late
                ReservedTo = todayStart.AddHours(2) // today early
            }
        ];

        db.Reservations.AddRange(reservations);
        db.SaveChanges();
    }
}