using backend.Errors.Exceptions;
using backend.Migrations.Data;
using backend.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Reservation;

public sealed class ReservationService(DeskDbContext db) : IReservationService
{
    public async Task<ReservationData> CreateAsync(ReservationCreateView createData,
        CancellationToken cancellationToken = default)
    {
        var desk = await db.Desks.SingleOrDefaultAsync(desk => desk.Id == createData.DeskId, cancellationToken);
        ThrowIfNotFound(desk, createData.DeskId);

        var user = await db.Users.SingleOrDefaultAsync(user => user.Id == createData.UserId, cancellationToken);
        ThrowIfNotFound(user, createData.UserId);

        var reservation = new Models.Reservation
        {
            Desk = desk!,
            User = user!,
            ReservedFrom = createData.ReservedFrom!.Value,
            ReservedTo = createData.ReservedTo!.Value
        };
        await db.Reservations.AddAsync(reservation, cancellationToken);

        await db.SaveChangesAsync(cancellationToken);

        return ReservationData.Of(reservation);
    }

    public async Task<ReservationData> CancelAsync(long reservationId, CancellationToken cancellationToken = default)
    {
        var reservation = await db.Reservations.FindAsync([reservationId], cancellationToken);
        if (reservation == null)
        {
            return ReservationData.Empty;
        }
        
        db.Reservations.Remove(reservation);
        
        await db.SaveChangesAsync(cancellationToken);
        
        return ReservationData.Of(reservation);
    }

    private static void ThrowIfNotFound<T, TId>(T? item,
        TId id)
    {
        if (item != null) return;

        throw NotFoundException.NotFound(id!, typeof(T));
    }
}