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
}