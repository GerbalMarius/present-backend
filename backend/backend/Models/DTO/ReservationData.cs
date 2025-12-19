namespace backend.Models.DTO;

public record ReservationData(
    long Id,
    long UserId,
    long DeskId,
    DateTime ReservedFrom,
    DateTime ReservedTo
)
{
    public static ReservationData Empty => new(-1, -1, -1, DateTime.MinValue, DateTime.MinValue);
    public static ReservationData Of(Reservation reservation) => new(
        reservation.Id, reservation.UserId, reservation.DeskId, reservation.ReservedFrom, reservation.ReservedTo
    );
};