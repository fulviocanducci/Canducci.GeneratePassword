namespace Canducci.GeneratePassword
{
    public interface IPbkdf2Value
    {
        string Salt { get; }
        string Hashed { get; }
    }
}
