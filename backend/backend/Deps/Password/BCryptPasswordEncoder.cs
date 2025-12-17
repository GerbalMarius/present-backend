
namespace backend.Deps.Password;

public sealed class BCryptPasswordEncoder : IPasswordEncoder
{
    public bool Verify(string rawPassword, string encodedPassword)
    {
       return BCrypt.Net.BCrypt.Verify(rawPassword, encodedPassword);
    }

    public string Encode(string rawPassword)
    {
        return BCrypt.Net.BCrypt.HashPassword(rawPassword);
    }
}