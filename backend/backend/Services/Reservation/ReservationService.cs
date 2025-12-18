using backend.Errors.Exceptions;
using backend.Migrations.Data;
using backend.Models;
using backend.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Reservation;

public sealed class ReservationService(DeskDbContext db) : IReservationService
{
    public Task<List<ReservationData>> GetAllByUserAsync(long userId, CancellationToken cancellationToken = default)
    {
        return db.Reservations.Where(reservation => reservation.UserId == userId)
            .Include(reservation => reservation.Desk)
            .Select(reservation => new ReservationData(userId, reservation.DeskId, reservation.ReservedFrom, reservation.ReservedTo))
            .ToListAsync(cancellationToken);
    }

    public async Task<Models.Reservation> CreateAsync(ReservationData reservationData,
        CancellationToken cancellationToken = default)
    {
        var desk = await db.Desks.SingleOrDefaultAsync(desk => desk.Id == reservationData.DeskId, cancellationToken);
        ThrowIfNotFound(desk, reservationData.DeskId);

        var user = await db.Users.SingleOrDefaultAsync(user => user.Id == reservationData.UserId, cancellationToken);
        ThrowIfNotFound(user, reservationData.UserId);

        var reservation = new Models.Reservation
        {
            Desk = desk!,
            User = user!,
            ReservedFrom = reservationData.ReservedFrom,
            ReservedTo = reservationData.ReservedTo
        };
        await db.Reservations.AddAsync(reservation, cancellationToken);

        await db.SaveChangesAsync(cancellationToken);

        return reservation;
    }

    private static void ThrowIfNotFound<T, TId>(T? item,
        TId id)
    {
        if (item != null) return;

        throw NotFoundException.NotFound(id!, typeof(T));
    }
}