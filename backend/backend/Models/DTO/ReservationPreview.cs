namespace backend.Models.DTO;

public record ReservationPreview(
    long Id,
    DateTime ReservedFrom,
    DateTime ReservedTo
    );