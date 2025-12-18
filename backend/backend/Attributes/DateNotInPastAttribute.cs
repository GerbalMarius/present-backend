using System.ComponentModel.DataAnnotations;

namespace backend.Attributes;

public sealed class DateNotInPastAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is not DateTime date) return true;
        
        return date.Date >= DateTime.Now.Date;
    }
}