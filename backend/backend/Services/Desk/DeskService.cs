using backend.Migrations.Data;
using backend.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Desk;

public sealed class DeskService(DeskDbContext db) : IDeskService
{
    public Task<List<DeskData>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return db.Desks
            .AsNoTracking()
            .Include(desk => desk.Reservations)
            .ThenInclude(reservation => reservation.User)
            .Select(desk => DeskData.OfDesk(desk))
            .ToListAsync(cancellationToken);
    }

    public async Task<DeskData> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        Models.Desk? result = await db.Desks
            .Include(desk => desk.Reservations)
            .ThenInclude(reservation => reservation.User)
            .FirstOrDefaultAsync(desk => desk.Id == id, cancellationToken);
        
        return result == null ? DeskData.Empty : DeskData.OfDesk(result);
    }
}