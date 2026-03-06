namespace Canducci.GeneratePassword.Argon2id
{
    public interface IArgon2idValue
    {
        string Salt { get; }
        string Hashed { get; }
    }
}
