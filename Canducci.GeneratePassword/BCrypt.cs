using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Canducci.GeneratePassword
{
    /// <summary>
    /// class BCrypt
    /// </summary>
    public class BCrypt
    {
        /// <summary>
        /// class BCryptConfiguration
        /// </summary>
        private BCryptConfiguration Configuration { get; }
        
        /// <summary>
        /// constructor BCrypt
        /// </summary>
        /// <param name="configuration">BCryptConfiguration</param>
        public BCrypt(BCryptConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// constructor BCrypt
        /// </summary>
        /// <param name="password">string</param>
        /// <returns>BCryptValue</returns>

        public BCryptValue Hash(string password)
        {
            byte[] salt = new byte[Configuration.SaltBytesLength];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            string hashed = HashedValue(salt, password);
            return new BCryptValue(Convert.ToBase64String(salt), hashed);
        }

        /// <summary>
        /// Valid
        /// </summary>
        /// <param name="password">string</param>
        /// <param name="value">string</param>
        /// <returns>bool</returns>

        public bool Valid(string password, BCryptValue value)
        {
            return Valid(password, value.Salt, value.Hashed);
        }

        /// <summary>
        /// Valid
        /// </summary>
        /// <param name="password">string</param>
        /// <param name="salt">string</param>
        /// <param name="hashed">string</param>
        /// <returns>bool</returns>
        public bool Valid(string password, string salt, string hashed)
        {
            byte[] saltValid = Convert.FromBase64String(salt);
            string hashedValid = HashedValue(saltValid, password);
            return string.Equals(hashed, hashedValid);
        }

        /// <summary>
        /// HashedValue
        /// </summary>
        /// <param name="salt">string</param>
        /// <param name="password">string</param>
        /// <returns>string</returns>
        private string HashedValue(byte[] salt, string password)
        {
            return Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: Configuration.Prf,
                    iterationCount: Configuration.IterationCount,
                    numBytesRequested: Configuration.NumBytesRequestedLength
                )
            );
        }
    }
}
