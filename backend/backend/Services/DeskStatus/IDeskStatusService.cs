using backend.Models.DTO;

namespace backend.Services.DeskStatus;

public interface IDeskStatusService
{
    Task<List<DeskStatusData>> FindAllAsync();
    Task<DeskStatusData> FindByIdAsync(long id);
}