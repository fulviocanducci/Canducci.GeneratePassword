using System;

namespace Canducci.GeneratePassword
{
    /// <summary>
    /// Use Pbkdf2Value instead.
    /// </summary>
    [Obsolete("BCryptValue is a legacy name. Use Pbkdf2Value instead.")]
    public class BCryptValue : Pbkdf2Value
    {
        /// <summary>
        /// class BCryptValue
        /// </summary>
        /// <param name="salt">Salt</param>
        /// <param name="hashed">Hashed</param>
        public BCryptValue(string salt, string hashed) : base(salt, hashed) { }
    }
}
