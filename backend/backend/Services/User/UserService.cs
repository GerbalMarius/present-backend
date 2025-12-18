using backend.Migrations.Data;
using backend.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.User;

public sealed class UserService(DeskDbContext db) : IUserService
{
    //since no auth is required, we take the first user from the db
    public async Task<UserData> GetCurrentUserAsync(CancellationToken cancellationToken = default)
    {
        var user = await db.Users.FirstOrDefaultAsync(cancellationToken);

        return user is null
            ? UserData.Empty
            : new UserData(user.Id, user.Email, user.FirstName, user.LastName);
    }
}