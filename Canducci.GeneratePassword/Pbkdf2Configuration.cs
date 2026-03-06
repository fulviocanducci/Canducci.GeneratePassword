using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Canducci.GeneratePassword
{
    /// <summary>
    /// Configuration for PBKDF2 password hashing.
    /// </summary>
    public class Pbkdf2Configuration : IPbkdf2Configuration
    {
        public const int DefaultIterationCount = 210000;
        public const int DefaultSaltBytesLength = 16;
        public const int DefaultNumBytesRequestedLength = 32;

        /// <summary>
        /// constructor Pbkdf2Configuration
        /// </summary>
        public Pbkdf2Configuration()
        {
            Prf = KeyDerivationPrf.HMACSHA512;
            IterationCount = DefaultIterationCount;
            SaltBytesLength = DefaultSaltBytesLength;
            NumBytesRequestedLength = DefaultNumBytesRequestedLength;
        }

        /// <summary>
        /// constructor Pbkdf2Configuration
        /// </summary>
        /// <param name="prf">KeyDerivationPrf</param>
        public Pbkdf2Configuration(KeyDerivationPrf prf)
        {
            Prf = prf;
            IterationCount = DefaultIterationCount;
            SaltBytesLength = DefaultSaltBytesLength;
            NumBytesRequestedLength = DefaultNumBytesRequestedLength;
        }

        /// <summary>
        /// constructor Pbkdf2Configuration
        /// </summary>
        /// <param name="prf">KeyDerivationPrf</param>
        /// <param name="iterationCount">iterationCount</param>
        public Pbkdf2Configuration(KeyDerivationPrf prf, int iterationCount)
        {
            Prf = prf;
            IterationCount = iterationCount;
            SaltBytesLength = DefaultSaltBytesLength;
            NumBytesRequestedLength = DefaultNumBytesRequestedLength;
        }

        /// <summary>
        /// constructor Pbkdf2Configuration
        /// </summary>
        /// <param name="prf">KeyDerivationPrf</param>
        /// <param name="iterationCount">iterationCount</param>
        /// <param name="saltBytesLength">saltBytesRequestLength</param>
        /// <param name="numBytesRequestedLength">numBytesRequestedLength</param>
        public Pbkdf2Configuration(
            KeyDerivationPrf prf,
            int iterationCount,
            int saltBytesLength,
            int numBytesRequestedLength)
        {
            Prf = prf;
            IterationCount = iterationCount;
            SaltBytesLength = saltBytesLength;
            NumBytesRequestedLength = numBytesRequestedLength;
        }

        /// <summary>
        /// KeyDerivationPrf
        /// </summary>
        public KeyDerivationPrf Prf { get; set; }

        /// <summary>
        /// IterationCount
        /// </summary>
        public int IterationCount { get; set; }

        /// <summary>
        /// SaltLength
        /// </summary>
        public int SaltBytesLength { get; set; }

        /// <summary>
        /// NumBytesRequestedLength
        /// </summary>
        public int NumBytesRequestedLength { get; set; }
    }
}
