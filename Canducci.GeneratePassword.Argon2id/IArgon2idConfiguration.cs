namespace Canducci.GeneratePassword.Argon2id
{
    public interface IArgon2idConfiguration
    {
        int IterationCount { get; set; }
        int MemorySizeKb { get; set; }
        int DegreeOfParallelism { get; set; }
        int SaltBytesLength { get; set; }
        int HashBytesLength { get; set; }
    }
}
