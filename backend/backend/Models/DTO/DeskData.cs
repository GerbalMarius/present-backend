namespace backend.Models.DTO;

public record DeskData(
    long Id,
    bool IsReserved,
    bool IsInMaintenance,
    UserData? ReservedBy,
    ReservationPreview? ReservationPreview
)
{
    public static DeskData Empty => new(-1, false, false, null, null);
    
    public static DeskData OfDeskWithUser(Desk desk)
    {
        var today = DateTime.Now;

        // Pick the most relevant reservation to show:
        // "current or next upcoming" = any reservation not finished yet,
        // ordered by start date.
        var r = desk.Reservations
            .Where(r => r.ReservedTo >= today)
            .OrderBy(r => r.ReservedFrom)
            .FirstOrDefault();

        if (r == null)
        {
            return new DeskData(
                desk.Id,
                false,
                desk.IsInMaintenance,
                null,
                null
            );
        }

        var u = r.User;

        return new DeskData(
            desk.Id,
            true,
            desk.IsInMaintenance,
            new UserData(u.Id, u.Email, u.FirstName, u.LastName),
            new ReservationPreview(
                r.Id,
                r.ReservedFrom,
                r.ReservedTo
            )
        );
    }
}