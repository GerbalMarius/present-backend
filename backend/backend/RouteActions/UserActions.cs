using backend.Services.User;

namespace backend.RouteActions;

public static class UserActions
{
    public static async Task<IResult> GetCurrentUserAsync(IUserService userService)
    {
        return TypedResults.Ok(await userService.GetCurrentUserAsync());
    }

    public static async Task<IResult> GetReservationDataByUserAsync(long id, IUserService userService)
    {
        return TypedResults.Ok(await userService.GetReservationDataByUserAsync(id));
    }
}