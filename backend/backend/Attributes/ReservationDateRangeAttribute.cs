using System.ComponentModel.DataAnnotations;
using backend.Models.DTO;

namespace backend.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class ReservationDateRangeAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not ReservationData reservationData)
        {
            return ValidationResult.Success;
        }

        if (reservationData.ReservedTo.Date < reservationData.ReservedFrom.Date)
        {
            return new ValidationResult(
                "Reservation end date must be later or equal to start date",
                ["reservedTo"]);
        }
        
        return ValidationResult.Success;
    }
}