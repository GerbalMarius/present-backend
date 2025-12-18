namespace backend.Models.DTO;

public record DeskData(
    long Id,
    bool IsReserved,
    bool IsInMaintenance,
    UserData? ReservedBy
)
{
    public static DeskData Empty => new(-1, false, false, null);
    
    public static DeskData OfDeskWithUser(Desk desk)
    {
        var today = DateTime.Now;
        var tomorrow = today.AddDays(1);

        var reservationRecipient = desk.Reservations
            .Where(r => r.ReservedFrom < tomorrow && r.ReservedTo >= today)
            .Select(r => r.User)
            .FirstOrDefault();

        return new DeskData(
            desk.Id,
            reservationRecipient != null,
            desk.IsInMaintenance,
            reservationRecipient == null
                ? null
                : new UserData(
                    reservationRecipient.Id,
                    reservationRecipient.Email,
                    reservationRecipient.FirstName,
                    reservationRecipient.LastName
                )
        );
    }
}