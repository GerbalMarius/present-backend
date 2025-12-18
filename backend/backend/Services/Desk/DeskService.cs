using backend.Migrations.Data;
using backend.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Desk;

public sealed class DeskService(DeskDbContext db) : IDeskService
{
    public Task<List<DeskData>> GetAllAsync()
    {
        return db.Desks
            .AsNoTracking()
            .Include(desk => desk.Reservations)
            .Select(desk => DeskData.OfDeskWithUser(desk))
            .ToListAsync();
    }

    public async Task<DeskData> GetByIdAsync(long id)
    {
        DeskData? result = await db.Desks
            .AsNoTracking()
            .Include(desk => desk.Reservations)
            .Select(desk => DeskData.OfDeskWithUser(desk))
            .FirstOrDefaultAsync(desk => desk.Id == id);
        
        return result ?? DeskData.Empty;
    }
}