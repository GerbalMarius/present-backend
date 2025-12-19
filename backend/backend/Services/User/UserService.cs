using backend.Migrations.Data;
using backend.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.User;

public sealed class UserService(DeskDbContext db) : IUserService
{
    //since no auth is required, we take the first user from the db
    public async Task<UserData> GetCurrentUserAsync(CancellationToken cancellationToken = default)
    {
        var user = await db.Users.FirstOrDefaultAsync(cancellationToken);

        return user is null
            ? UserData.Empty
            : new UserData(user.Id, user.Email, user.FirstName, user.LastName);
    }

    public Task<List<ReservationData>> GetReservationDataByUserAsync(long userId, CancellationToken cancellationToken = default)
    {
        
        return db.Reservations
            .Where(r => r.UserId == userId)
            .Select(r => ReservationData.Of(r))
            .ToListAsync(cancellationToken);
    }

    public async Task<CurrentUserReservations> GetCurrentUserReservationsAsync(CancellationToken cancellationToken = default)
    {
        UserData me = await GetCurrentUserAsync(cancellationToken);

        if (me == UserData.Empty)
        {
            return CurrentUserReservations.Empty;
        }

        DateTime today = DateTime.Now.Date;
        
        List<ReservationData> allReservations = await db.Reservations
                .Where(r => r.UserId == me.Id)
                .Select(r => ReservationData.Of(r))
                .ToListAsync(cancellationToken);
        
        
        List<ReservationData> current = allReservations
            .Where(r => r.ReservedTo >= today) 
            .OrderBy(r => r.ReservedFrom)
            .ToList();

        List<ReservationData> past = allReservations
            .Where(r => r.ReservedTo < today)
            .OrderByDescending(r => r.ReservedFrom)
            .ToList();
        
        return new CurrentUserReservations(current, past);
    }
}