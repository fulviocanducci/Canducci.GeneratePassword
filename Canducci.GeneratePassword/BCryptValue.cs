namespace Canducci.GeneratePassword
{
    /// <summary>
    /// class BCryptValue
    /// </summary>
    public class BCryptValue
    {
        /// <summary>
        /// class BCryptValue
        /// </summary>
        /// <param name="salt">Salt</param>
        /// <param name="hashed">Hashed</param>
        public BCryptValue(string salt, string hashed)
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
    }
}
