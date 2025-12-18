using System.ComponentModel.DataAnnotations;
using backend.Attributes;

namespace backend.Models.DTO;

[ReservationDateRange]
public record ReservationData(
    
    [property: Required(ErrorMessage = "User Id is required")]
    [property: Range(1, long.MaxValue, ErrorMessage = "User Id must be positive")]
    long UserId,
    
    [property: Required(ErrorMessage = "Desk Id is required")]
    [property: Range(1, long.MaxValue, ErrorMessage = "Desk Id must be positive")]
    long DeskId,
    
    [property: Required(ErrorMessage = "Begin reservation date is required")]
    [property: DateNotInPast(ErrorMessage = "Reservation date must be in the present or the future")]
    DateTime ReservedFrom,
    
    [property: Required(ErrorMessage = "Reservation date is required")]
    DateTime ReservedTo
    );