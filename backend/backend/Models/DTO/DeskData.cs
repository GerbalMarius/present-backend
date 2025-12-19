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
    
    public static DeskData OfDesk(Desk desk)
    {
        var today = DateTime.Now;
        var r = desk.Reservations
            .Where(r => r.ReservedTo >= today)
            .OrderBy(r => r.ReservedFrom)
            .FirstOrDefault();

        return r == null ? CreateDeskNoUser(desk) : CreateDeskWithUser(desk, r);
    }

    private static DeskData CreateDeskWithUser(Desk desk, Reservation r)
    {
        User u = r.User;

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

    private static DeskData CreateDeskNoUser(Desk desk)
    {
        return new DeskData(
            desk.Id,
            false,
            desk.IsInMaintenance,
            null,
            null
        );
    }
}