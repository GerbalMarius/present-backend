using backend.Models.DTO;

namespace backend.Services.Desk;

public interface IDeskService
{
    Task<List<DeskData>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task<DeskData> GetByIdAsync(long id, CancellationToken cancellationToken = default);
}