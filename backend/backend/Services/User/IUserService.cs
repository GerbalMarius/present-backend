using backend.Models.DTO;

namespace backend.Services.User;

public interface IUserService
{
    Task<UserData> GetCurrentUserAsync(CancellationToken cancellationToken = default);
    
    Task<List<ReservationData>> GetReservationDataByUserAsync(long userId, CancellationToken cancellationToken = default);
}