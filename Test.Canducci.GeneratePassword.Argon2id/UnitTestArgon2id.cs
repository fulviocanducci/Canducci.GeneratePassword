using Canducci.GeneratePassword.Argon2id;
using NUnit.Framework;

namespace Test.Canducci.GeneratePassword.Argon2id
{
    public class UnitTestArgon2id
    {
        [Test]
        public void TestArgon2idConfigurationDefaults()
        {
            Argon2idConfiguration configuration = new Argon2idConfiguration();
            Assert.AreEqual(Argon2idConfiguration.DefaultIterationCount, configuration.IterationCount);
            Assert.AreEqual(Argon2idConfiguration.DefaultMemorySizeKb, configuration.MemorySizeKb);
            Assert.AreEqual(Argon2idConfiguration.DefaultDegreeOfParallelism, configuration.DegreeOfParallelism);
            Assert.AreEqual(Argon2idConfiguration.DefaultSaltBytesLength, configuration.SaltBytesLength);
            Assert.AreEqual(Argon2idConfiguration.DefaultHashBytesLength, configuration.HashBytesLength);
        }

        [Test]
        public void TestArgon2idHashAndValid()
        {
            IArgon2idPasswordHasher hasher =
                new Argon2idPasswordHasher(new Argon2idConfiguration());

            IArgon2idValue value = hasher.Hash("abc@123");
            Assert.IsTrue(hasher.Valid("abc@123", value));
            Assert.IsFalse(hasher.Valid("other", value));
        }

        [Test]
        public void TestArgon2idEncodedHashAndValid()
        {
            IArgon2idPasswordHasher hasher =
                new Argon2idPasswordHasher(new Argon2idConfiguration());

            string encoded = hasher.HashEncoded("abc@123");
            Assert.IsTrue(hasher.ValidEncoded("abc@123", encoded));
            Assert.IsFalse(hasher.ValidEncoded("other", encoded));
        }
    }
}
