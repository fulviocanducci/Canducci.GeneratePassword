using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Canducci.GeneratePassword
{
    /// <summary>
    /// class BCryptConfiguration
    /// </summary>
    public class BCryptConfiguration
    {
        /// <summary>
        /// constructor BCryptConfiguration
        /// </summary>
        public BCryptConfiguration()
        {
            Prf = KeyDerivationPrf.HMACSHA1;
            IterationCount = 10000;
        }

        /// <summary>
        /// constructor BCryptConfiguration
        /// </summary>
        /// <param name="prf">KeyDerivationPrf</param>

        public BCryptConfiguration(KeyDerivationPrf prf)
        {
            Prf = prf;
            IterationCount = 10000;
        }

        /// <summary>
        /// constructor BCryptConfiguration
        /// </summary>
        /// <param name="prf">KeyDerivationPrf</param>
        /// <param name="iterationCount">iterationCount</param>
        public BCryptConfiguration(KeyDerivationPrf prf, int iterationCount)
        {
            Prf = prf;
            IterationCount = iterationCount;
        }

        /// <summary>
        /// KeyDerivationPrf
        /// </summary>
        public KeyDerivationPrf Prf { get; }

        /// <summary>
        /// IterationCount
        /// </summary>
        public int IterationCount { get; }
    }
}
