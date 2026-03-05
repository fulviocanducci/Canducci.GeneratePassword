using System;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Canducci.GeneratePassword
{
    /// <summary>
    /// Use Pbkdf2Configuration instead.
    /// </summary>
    [Obsolete("BCryptConfiguration is a legacy name. Use Pbkdf2Configuration instead.")]
    public class BCryptConfiguration : Pbkdf2Configuration
    {
        /// <summary>
        /// constructor BCryptConfiguration
        /// </summary>
        public BCryptConfiguration() : base() { }

        /// <summary>
        /// constructor BCryptConfiguration
        /// </summary>
        /// <param name="prf">KeyDerivationPrf</param>
        public BCryptConfiguration(KeyDerivationPrf prf) : base(prf) { }

        /// <summary>
        /// constructor BCryptConfiguration
        /// </summary>
        /// <param name="prf">KeyDerivationPrf</param>
        /// <param name="iterationCount">iterationCount</param>
        public BCryptConfiguration(KeyDerivationPrf prf, int iterationCount) : base(prf, iterationCount) { }

        /// <summary>
        /// constructor BCryptConfiguration
        /// </summary>
        /// <param name="prf">KeyDerivationPrf</param>
        /// <param name="iterationCount">iterationCount</param>
        /// <param name="saltBytesLength">saltBytesRequestLength</param>
        /// <param name="numBytesRequestedLength">numBytesRequestedLength</param>
        public BCryptConfiguration(
            KeyDerivationPrf prf,
            int iterationCount,
            int saltBytesLength,
            int numBytesRequestedLength)
            : base(prf, iterationCount, saltBytesLength, numBytesRequestedLength) { }
    }
}
