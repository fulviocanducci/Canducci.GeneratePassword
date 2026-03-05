namespace Canducci.GeneratePassword
{
    public interface IPbkdf2PasswordHasher
    {
        IPbkdf2Value Hash(string password);
        string HashEncoded(string password);
        bool Valid(string password, IPbkdf2Value value);
        bool Valid(string password, string salt, string hashed);
        bool ValidEncoded(string password, string encodedValue);
    }
}
