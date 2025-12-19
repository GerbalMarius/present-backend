using backend.Errors;
using backend.Models.DTO;
using backend.Services.Reservation;

namespace backend.RouteActions;

public static class ReservationActions
{
    public static async Task<IResult> CreateAsync(ReservationCreateView reservationData, IReservationService reservationService)
    {
        var reservation = await reservationService.CreateAsync(reservationData);
        
        return TypedResults.CreatedAtRoute(
            routeName : "CreateReservation", 
            value : reservation
        );
    }
    
    public static async Task<IResult> CancelAsync(long id, IReservationService reservationService)
    {
        var reservationData = await reservationService.CancelAsync(id);
        if (reservationData == ReservationData.Empty)
        {
            return ApiError.NotFound(id, "Reservation not found")
                           .ToResult();
        }

        return TypedResults.NoContent();
    }
}