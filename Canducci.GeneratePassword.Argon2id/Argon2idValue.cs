using System;

namespace Canducci.GeneratePassword.Argon2id
{
    public class Argon2idValue : IArgon2idValue
    {
        public const string FormatVersion = "argon2id-v1";

        public Argon2idValue(string salt, string hashed)
        {
            Salt = salt;
            Hashed = hashed;
        }

        public string Salt { get; }
        public string Hashed { get; }

        public static string Encode(
            int iterationCount,
            int memorySizeKb,
            int degreeOfParallelism,
            string salt,
            string hashed)
        {
            return string.Join(
                "$",
                FormatVersion,
                iterationCount.ToString(),
                memorySizeKb.ToString(),
                degreeOfParallelism.ToString(),
                salt,
                hashed);
        }

        public static bool TryDecode(string encodedValue, out Argon2idEncodedValue decoded)
        {
            decoded = null;
            if (string.IsNullOrWhiteSpace(encodedValue))
            {
                return false;
            }

            string[] parts = encodedValue.Split('$');
            if (parts.Length != 6 || !string.Equals(parts[0], FormatVersion, StringComparison.Ordinal))
            {
                return false;
            }

            if (!int.TryParse(parts[1], out int iterationCount))
            {
                return false;
            }

            if (!int.TryParse(parts[2], out int memorySizeKb))
            {
                return false;
            }

            if (!int.TryParse(parts[3], out int degreeOfParallelism))
            {
                return false;
            }

            decoded = new Argon2idEncodedValue(
                iterationCount,
                memorySizeKb,
                degreeOfParallelism,
                parts[4],
                parts[5]);

            return true;
        }
    }

    public class Argon2idEncodedValue : IArgon2idEncodedValue
    {
        public Argon2idEncodedValue(
            int iterationCount,
            int memorySizeKb,
            int degreeOfParallelism,
            string salt,
            string hashed)
        {
            IterationCount = iterationCount;
            MemorySizeKb = memorySizeKb;
            DegreeOfParallelism = degreeOfParallelism;
            Salt = salt;
            Hashed = hashed;
        }

        public int IterationCount { get; }
        public int MemorySizeKb { get; }
        public int DegreeOfParallelism { get; }
        public string Salt { get; }
        public string Hashed { get; }
    }
}
