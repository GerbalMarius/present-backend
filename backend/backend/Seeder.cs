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

        // Day boundaries only
        var today = DateTime.Now.Date;
        var tomorrow = today.AddDays(1);

        // --- USERS ---
        List<User> users =
        [
            new() { Email = "marius@gmail.com", Password = "pass", FirstName = "Marius", LastName = "Ambrazevičius" },
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
            new() { IsInMaintenance = false },
            new() { IsInMaintenance = false },
            new() { IsInMaintenance = false }
        ];

        db.Desks.AddRange(desks);
        db.SaveChanges();

        List<Reservation> reservations =
        [
            new()
            {
                DeskId = desks[2].Id, Desk = desks[2],
                UserId = users[0].Id, User = users[0],
                ReservedFrom = today,
                ReservedTo = tomorrow
            },

            new()
            {
                DeskId = desks[3].Id, Desk = desks[3],
                UserId = users[1].Id, User = users[1],
                ReservedFrom = today.AddDays(-1),
                ReservedTo = tomorrow
            },

            new()
            {
                DeskId = desks[4].Id, Desk = desks[4],
                UserId = users[2].Id, User = users[2],
                ReservedFrom = today.AddDays(-3),
                ReservedTo = today.AddDays(-2)
            },

            new()
            {
                DeskId = desks[5].Id, Desk = desks[5],
                UserId = users[3].Id, User = users[3],
                ReservedFrom = tomorrow,
                ReservedTo = tomorrow.AddDays(2)
            },

            new()
            {
                DeskId = desks[6].Id, Desk = desks[6],
                UserId = users[2].Id, User = users[2],
                ReservedFrom = today.AddDays(-2),
                ReservedTo = tomorrow
            },


            new()
            {
                DeskId = desks[7].Id, Desk = desks[7],
                UserId = users[0].Id, User = users[0],
                ReservedFrom = today.AddDays(-14),
                ReservedTo = today.AddDays(-11)
            },

            new()
            {
                DeskId = desks[8].Id, Desk = desks[8],
                UserId = users[0].Id, User = users[0],
                ReservedFrom = today.AddDays(-6),
                ReservedTo = today.AddDays(-5)
            },

            new()
            {
                DeskId = desks[0].Id, Desk = desks[0],
                UserId = users[0].Id, User = users[0],
                ReservedFrom = today.AddDays(-1),
                ReservedTo = today
            }
        ];

        db.Reservations.AddRange(reservations);
        db.SaveChanges();
    }
}