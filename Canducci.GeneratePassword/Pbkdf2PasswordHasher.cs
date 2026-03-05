using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Canducci.GeneratePassword
{
    /// <summary>
    /// class Pbkdf2PasswordHasher
    /// </summary>
    public class Pbkdf2PasswordHasher : IPbkdf2PasswordHasher
    {
        /// <summary>
        /// class IPbkdf2Configuration
        /// </summary>
        private IPbkdf2Configuration Configuration { get; }

        /// <summary>
        /// constructor Pbkdf2PasswordHasher
        /// </summary>
        /// <param name="configuration">IPbkdf2Configuration</param>
        public Pbkdf2PasswordHasher(IPbkdf2Configuration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Hash
        /// </summary>
        /// <param name="password">string</param>
        /// <returns>IPbkdf2Value</returns>
        public IPbkdf2Value Hash(string password)
        {
            ValidateConfiguration(Configuration);
            ValidatePassword(password, nameof(password));

            byte[] salt = new byte[Configuration.SaltBytesLength];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            string hashed = HashedValue(
                salt,
                password,
                Configuration.Prf,
                Configuration.IterationCount,
                Configuration.NumBytesRequestedLength);
            return new Pbkdf2Value(Convert.ToBase64String(salt), hashed);
        }

        /// <summary>
        /// Hash with versioned encoded output: pbkdf2-v1$prf$iterations$numBytes$salt$hash
        /// </summary>
        /// <param name="password">string</param>
        /// <returns>string</returns>
        public string HashEncoded(string password)
        {
            IPbkdf2Value value = Hash(password);
            return Pbkdf2Value.Encode(
                Configuration.Prf,
                Configuration.IterationCount,
                Configuration.NumBytesRequestedLength,
                value.Salt,
                value.Hashed);
        }

        /// <summary>
        /// Valid
        /// </summary>
        /// <param name="password">string</param>
        /// <param name="value">IPbkdf2Value</param>
        /// <returns>bool</returns>
        public bool Valid(string password, IPbkdf2Value value)
        {
            if (value == null)
            {
                return false;
            }
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
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            ValidateConfiguration(Configuration);

            if (!TryFromBase64(salt, out byte[] saltValid))
            {
                return false;
            }

            if (!TryFromBase64(hashed, out byte[] storedHashBytes))
            {
                return false;
            }

            byte[] computedHashBytes = KeyDerivation.Pbkdf2(
                password: password,
                salt: saltValid,
                prf: Configuration.Prf,
                iterationCount: Configuration.IterationCount,
                numBytesRequested: Configuration.NumBytesRequestedLength);

            if (computedHashBytes.Length != storedHashBytes.Length)
            {
                return false;
            }

            return FixedTimeEquals(computedHashBytes, storedHashBytes);
        }

        /// <summary>
        /// Validate a password against encoded value:
        /// pbkdf2-v1$prf$iterations$numBytes$salt$hash
        /// </summary>
        /// <param name="password">string</param>
        /// <param name="encodedValue">string</param>
        /// <returns>bool</returns>
        public bool ValidEncoded(string password, string encodedValue)
        {
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            if (!Pbkdf2Value.TryDecode(encodedValue, out Pbkdf2EncodedValue decoded))
            {
                return false;
            }

            if (!TryFromBase64(decoded.Salt, out byte[] saltBytes))
            {
                return false;
            }

            if (!TryFromBase64(decoded.Hashed, out byte[] storedHashBytes))
            {
                return false;
            }

            if (decoded.IterationCount <= 0 || decoded.NumBytesRequestedLength <= 0)
            {
                return false;
            }

            byte[] computedHashBytes = KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: decoded.Prf,
                iterationCount: decoded.IterationCount,
                numBytesRequested: decoded.NumBytesRequestedLength);

            if (computedHashBytes.Length != storedHashBytes.Length)
            {
                return false;
            }

            return FixedTimeEquals(computedHashBytes, storedHashBytes);
        }

        /// <summary>
        /// HashedValue
        /// </summary>
        /// <param name="salt">byte[]</param>
        /// <param name="password">string</param>
        /// <returns>string</returns>
        private static string HashedValue(
            byte[] salt,
            string password,
            KeyDerivationPrf prf,
            int iterationCount,
            int numBytesRequestedLength)
        {
            return Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: prf,
                    iterationCount: iterationCount,
                    numBytesRequested: numBytesRequestedLength
                )
            );
        }

        private static bool TryFromBase64(string value, out byte[] bytes)
        {
            bytes = null;
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            try
            {
                bytes = Convert.FromBase64String(value);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private static bool FixedTimeEquals(byte[] left, byte[] right)
        {
            if (left == null || right == null || left.Length != right.Length)
            {
                return false;
            }

            int diff = 0;
            for (int index = 0; index < left.Length; index++)
            {
                diff |= left[index] ^ right[index];
            }

            return diff == 0;
        }

        private static void ValidateConfiguration(IPbkdf2Configuration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (configuration.IterationCount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(configuration.IterationCount));
            }

            if (configuration.SaltBytesLength <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(configuration.SaltBytesLength));
            }

            if (configuration.NumBytesRequestedLength <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(configuration.NumBytesRequestedLength));
            }
        }

        private static void ValidatePassword(string password, string paramName)
        {
            if (password == null)
            {
                throw new ArgumentNullException(paramName);
            }

            if (password.Length == 0)
            {
                throw new ArgumentException("Password cannot be empty.", paramName);
            }
        }
    }
}
