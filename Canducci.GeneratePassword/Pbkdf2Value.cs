using System;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Canducci.GeneratePassword
{
    /// <summary>
    /// class Pbkdf2Value
    /// </summary>
    public class Pbkdf2Value : IPbkdf2Value
    {
        public const string FormatVersion = "pbkdf2-v1";

        /// <summary>
        /// class Pbkdf2Value
        /// </summary>
        /// <param name="salt">Salt</param>
        /// <param name="hashed">Hashed</param>
        public Pbkdf2Value(string salt, string hashed)
        {
            Salt = salt;
            Hashed = hashed;
        }

        /// <summary>
        /// Salt
        /// </summary>
        public string Salt { get; }

        /// <summary>
        /// Hashed
        /// </summary>
        public string Hashed { get; }

        public static string Encode(
            KeyDerivationPrf prf,
            int iterationCount,
            int numBytesRequestedLength,
            string salt,
            string hashed)
        {
            return string.Join(
                "$",
                FormatVersion,
                prf.ToString(),
                iterationCount.ToString(),
                numBytesRequestedLength.ToString(),
                salt,
                hashed);
        }

        public static bool TryDecode(string encodedValue, out Pbkdf2EncodedValue decoded)
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

            if (!Enum.TryParse(parts[1], out KeyDerivationPrf prf))
            {
                return false;
            }

            if (!int.TryParse(parts[2], out int iterationCount))
            {
                return false;
            }

            if (!int.TryParse(parts[3], out int numBytesRequestedLength))
            {
                return false;
            }

            decoded = new Pbkdf2EncodedValue(
                prf,
                iterationCount,
                numBytesRequestedLength,
                parts[4],
                parts[5]);

            return true;
        }
    }

    public class Pbkdf2EncodedValue : IPbkdf2EncodedValue
    {
        public Pbkdf2EncodedValue(
            KeyDerivationPrf prf,
            int iterationCount,
            int numBytesRequestedLength,
            string salt,
            string hashed)
        {
            Prf = prf;
            IterationCount = iterationCount;
            NumBytesRequestedLength = numBytesRequestedLength;
            Salt = salt;
            Hashed = hashed;
        }

        public KeyDerivationPrf Prf { get; }
        public int IterationCount { get; }
        public int NumBytesRequestedLength { get; }
        public string Salt { get; }
        public string Hashed { get; }
    }
}
