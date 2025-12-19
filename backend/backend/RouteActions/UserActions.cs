using backend.Errors;
using backend.Models.DTO;
using backend.Services.User;

namespace backend.RouteActions;

public static class UserActions
{
    public static async Task<IResult> GetCurrentUserAsync(IUserService userService)
    {
        return TypedResults.Ok(await userService.GetCurrentUserAsync());
    }
    
    public static async Task<IResult> GetCurrentUserReservationsAsync(IUserService userService)
    {
        CurrentUserReservations reservations = await userService.GetCurrentUserReservationsAsync();
        if (ReferenceEquals(reservations, CurrentUserReservations.Empty))
        {
            return ApiError.Unauthorized("User is unauthenticated")
                           .ToResult();
        }
        return TypedResults.Ok(reservations);
    }

    public static async Task<IResult> GetReservationDataByUserAsync(long id, IUserService userService)
    {
        return TypedResults.Ok(await userService.GetReservationDataByUserAsync(id));
    }
}