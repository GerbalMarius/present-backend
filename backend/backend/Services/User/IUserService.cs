using backend.Models.DTO;

namespace backend.Services.User;

public interface IUserService
{
    Task<UserData> GetCurrentUserAsync(CancellationToken cancellationToken = default);
}