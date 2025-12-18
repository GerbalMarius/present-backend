using System.ComponentModel.DataAnnotations;
using backend.Attributes;

namespace backend.Models.DTO;


[ReservationDateRange]
public record ReservationCreateView(
    [property: Required(ErrorMessage = "User Id is required")]
    [property: Range(1, long.MaxValue, ErrorMessage = "User Id must be greater than 0")]
    long? UserId,
    
    [property: Required(ErrorMessage = "Desk Id is required")]
    [property: Range(1, long.MaxValue, ErrorMessage = "Desk Id must be greater than 0")]
    long? DeskId,
    
    [property: DateNotInPast(ErrorMessage = "Reservation date must be in the present or the future")]
    [property: Required(ErrorMessage = "Begin reservation date is required")]
    DateTime? ReservedFrom,
    
    [property: Required(ErrorMessage = "Reservation end date is required")]
    DateTime? ReservedTo
    );