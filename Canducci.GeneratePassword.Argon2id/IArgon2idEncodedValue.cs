namespace Canducci.GeneratePassword.Argon2id
{
    public interface IArgon2idEncodedValue
    {
        int IterationCount { get; }
        int MemorySizeKb { get; }
        int DegreeOfParallelism { get; }
        string Salt { get; }
        string Hashed { get; }
    }
}
