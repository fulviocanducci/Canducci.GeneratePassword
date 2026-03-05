namespace Canducci.GeneratePassword.Argon2id
{
    public class Argon2idConfiguration : IArgon2idConfiguration
    {
        public const int DefaultIterationCount = 3;
        public const int DefaultMemorySizeKb = 65536;
        public const int DefaultDegreeOfParallelism = 2;
        public const int DefaultSaltBytesLength = 16;
        public const int DefaultHashBytesLength = 32;

        public Argon2idConfiguration()
        {
            IterationCount = DefaultIterationCount;
            MemorySizeKb = DefaultMemorySizeKb;
            DegreeOfParallelism = DefaultDegreeOfParallelism;
            SaltBytesLength = DefaultSaltBytesLength;
            HashBytesLength = DefaultHashBytesLength;
        }

        public Argon2idConfiguration(
            int iterationCount,
            int memorySizeKb,
            int degreeOfParallelism,
            int saltBytesLength,
            int hashBytesLength)
        {
            IterationCount = iterationCount;
            MemorySizeKb = memorySizeKb;
            DegreeOfParallelism = degreeOfParallelism;
            SaltBytesLength = saltBytesLength;
            HashBytesLength = hashBytesLength;
        }

        public int IterationCount { get; set; }
        public int MemorySizeKb { get; set; }
        public int DegreeOfParallelism { get; set; }
        public int SaltBytesLength { get; set; }
        public int HashBytesLength { get; set; }
    }
}
