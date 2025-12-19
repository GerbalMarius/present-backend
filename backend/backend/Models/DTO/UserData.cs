namespace backend.Models.DTO;

public record UserData(
    long Id,
    string Email,
    string FirstName,
    string LastName
)
{
    public static UserData Empty => new(-1, string.Empty, string.Empty, string.Empty);
}