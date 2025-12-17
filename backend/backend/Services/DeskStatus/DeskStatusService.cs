using backend.Migrations.Data;
using backend.Models;
using backend.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.DeskStatus;

public sealed class DeskStatusService(DeskDbContext db) : IDeskStatusService
{
    public Task<List<DeskStatusData>> FindAllAsync()
    {
        return db.DeskStatuses.Select(status => new DeskStatusData(status.Id, status.Name))
                              .ToListAsync();
    }

    public async Task<DeskStatusData> FindByIdAsync(long id)
    {
        var deskStatus = await db.DeskStatuses.FindAsync(id);
        
        return deskStatus is null ? 
            DeskStatusData.Empty : 
            new DeskStatusData(deskStatus.Id, deskStatus.Name);
    }
}