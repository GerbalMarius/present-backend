using System.ComponentModel.DataAnnotations;
using backend.Models.DTO;

namespace backend.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class ReservationDateRangeAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not ReservationCreateView reservationData)
        {
            return ValidationResult.Success;
        }

        if (reservationData.ReservedTo!.Value.Date < reservationData.ReservedFrom!.Value.Date)
        {
            return new ValidationResult(
                "Reservation end date must be later or equal to start date",
                ["reservedTo"]);
        }
        
        return ValidationResult.Success;
    }
}