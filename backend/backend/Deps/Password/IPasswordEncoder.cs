namespace backend.Deps.Password;

public interface IPasswordEncoder
{
    bool Verify(string rawPassword, string encodedPassword);
    string Encode(string rawPassword);
}