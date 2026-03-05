using System;
using System.Security.Cryptography;

namespace Canducci.GeneratePassword.Argon2id
{
    public class Argon2idPasswordHasher : IArgon2idPasswordHasher
    {
        private IArgon2idConfiguration Configuration { get; }

        public Argon2idPasswordHasher(IArgon2idConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public IArgon2idValue Hash(string password)
        {
            ValidateConfiguration(Configuration);
            ValidatePassword(password, nameof(password));

            byte[] salt = new byte[Configuration.SaltBytesLength];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            byte[] hash = ComputeHash(
                password,
                salt,
                Configuration.IterationCount,
                Configuration.MemorySizeKb,
                Configuration.DegreeOfParallelism,
                Configuration.HashBytesLength);

            return new Argon2idValue(Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        public string HashEncoded(string password)
        {
            IArgon2idValue value = Hash(password);
            return Argon2idValue.Encode(
                Configuration.IterationCount,
                Configuration.MemorySizeKb,
                Configuration.DegreeOfParallelism,
                value.Salt,
                value.Hashed);
        }

        public bool Valid(string password, IArgon2idValue value)
        {
            if (value == null)
            {
                return false;
            }

            return Valid(password, value.Salt, value.Hashed);
        }

        public bool Valid(string password, string salt, string hashed)
        {
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            ValidateConfiguration(Configuration);

            if (!TryFromBase64(salt, out byte[] saltBytes))
            {
                return false;
            }

            if (!TryFromBase64(hashed, out byte[] storedHashBytes))
            {
                return false;
            }

            byte[] computedHash = ComputeHash(
                password,
                saltBytes,
                Configuration.IterationCount,
                Configuration.MemorySizeKb,
                Configuration.DegreeOfParallelism,
                Configuration.HashBytesLength);

            if (computedHash.Length != storedHashBytes.Length)
            {
                return false;
            }

            return FixedTimeEquals(computedHash, storedHashBytes);
        }

        public bool ValidEncoded(string password, string encodedValue)
        {
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            if (!Argon2idValue.TryDecode(encodedValue, out Argon2idEncodedValue decoded))
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

            if (decoded.IterationCount <= 0 ||
                decoded.MemorySizeKb <= 0 ||
                decoded.DegreeOfParallelism <= 0)
            {
                return false;
            }

            byte[] computedHash = ComputeHash(
                password,
                saltBytes,
                decoded.IterationCount,
                decoded.MemorySizeKb,
                decoded.DegreeOfParallelism,
                storedHashBytes.Length);

            if (computedHash.Length != storedHashBytes.Length)
            {
                return false;
            }

            return FixedTimeEquals(computedHash, storedHashBytes);
        }

        private static byte[] ComputeHash(
            string password,
            byte[] salt,
            int iterations,
            int memorySizeKb,
            int degreeOfParallelism,
            int hashBytesLength)
        {
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);

            using (var argon2 = new Konscious.Security.Cryptography.Argon2id(passwordBytes))
            {
                argon2.Salt = salt;
                argon2.DegreeOfParallelism = degreeOfParallelism;
                argon2.Iterations = iterations;
                argon2.MemorySize = memorySizeKb;
                return argon2.GetBytes(hashBytesLength);
            }
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

        private static void ValidateConfiguration(IArgon2idConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (configuration.IterationCount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(configuration.IterationCount));
            }

            if (configuration.MemorySizeKb <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(configuration.MemorySizeKb));
            }

            if (configuration.DegreeOfParallelism <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(configuration.DegreeOfParallelism));
            }

            if (configuration.SaltBytesLength <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(configuration.SaltBytesLength));
            }

            if (configuration.HashBytesLength <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(configuration.HashBytesLength));
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
