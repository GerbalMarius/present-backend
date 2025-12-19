namespace backend.Models.DTO;

public record CurrentUserReservations(
    List<ReservationData> CurrentReservations,
    List<ReservationData> PastReservations
)
{
    public static readonly CurrentUserReservations Empty = new([], []);
}