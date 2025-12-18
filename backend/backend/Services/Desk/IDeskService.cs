using backend.Models.DTO;

namespace backend.Services.Desk;

public interface IDeskService
{
    Task<List<DeskData>> GetAllAsync();
    
    Task<DeskData> GetByIdAsync(long id);
}