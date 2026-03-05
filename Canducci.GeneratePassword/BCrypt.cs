using System;

namespace Canducci.GeneratePassword
{
    /// <summary>
    /// Use Pbkdf2PasswordHasher instead.
    /// </summary>
    [Obsolete("BCrypt is a legacy name. Use Pbkdf2PasswordHasher instead.")]
    public class BCrypt
    {
        private IPbkdf2PasswordHasher Hasher { get; }

        /// <summary>
        /// constructor BCrypt
        /// </summary>
        /// <param name="configuration">BCryptConfiguration</param>
        public BCrypt(BCryptConfiguration configuration)
        {
            Hasher = new Pbkdf2PasswordHasher(configuration);
        }

        /// <summary>
        /// Hash
        /// </summary>
        /// <param name="password">string</param>
        /// <returns>BCryptValue</returns>
        public BCryptValue Hash(string password)
        {
            IPbkdf2Value value = Hasher.Hash(password);
            return new BCryptValue(value.Salt, value.Hashed);
        }

        /// <summary>
        /// Valid
        /// </summary>
        /// <param name="password">string</param>
        /// <param name="value">BCryptValue</param>
        /// <returns>bool</returns>
        public bool Valid(string password, BCryptValue value)
        {
            return Hasher.Valid(password, value.Salt, value.Hashed);
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
            return Hasher.Valid(password, salt, hashed);
        }
    }
}
