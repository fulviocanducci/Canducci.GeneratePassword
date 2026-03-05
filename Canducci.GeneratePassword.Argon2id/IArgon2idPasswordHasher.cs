namespace Canducci.GeneratePassword.Argon2id
{
    public interface IArgon2idPasswordHasher
    {
        IArgon2idValue Hash(string password);
        string HashEncoded(string password);
        bool Valid(string password, IArgon2idValue value);
        bool Valid(string password, string salt, string hashed);
        bool ValidEncoded(string password, string encodedValue);
    }
}
