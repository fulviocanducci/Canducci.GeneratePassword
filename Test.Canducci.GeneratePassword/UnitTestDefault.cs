using NUnit.Framework;
using Canducci.GeneratePassword;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Test.Canducci.GeneratePassword
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestBCryptConfigurationInstance()
        {
            BCryptConfiguration configuration = new BCryptConfiguration();
            Assert.IsInstanceOf<BCryptConfiguration>(configuration);
        }

        [Test]
        public void TestBCryptConfigurationDefault()
        {
            BCryptConfiguration configuration = new BCryptConfiguration();
            Assert.AreEqual(configuration.Prf, KeyDerivationPrf.HMACSHA1);
            Assert.AreEqual(configuration.IterationCount, 10000);
        }

        [Test]
        public void TestBCryptConfigurationConstructor256()
        {
            BCryptConfiguration configuration = new BCryptConfiguration(KeyDerivationPrf.HMACSHA256, 1);
            Assert.AreEqual(configuration.Prf, KeyDerivationPrf.HMACSHA256);
            Assert.AreEqual(configuration.IterationCount, 1);
        }

        [Test]
        public void TestBCryptConfigurationConstructor512()
        {
            BCryptConfiguration configuration = new BCryptConfiguration(KeyDerivationPrf.HMACSHA512, 2);
            Assert.AreEqual(configuration.Prf, KeyDerivationPrf.HMACSHA512);
            Assert.AreEqual(configuration.IterationCount, 2);
        }

        [Test]
        public void TestBCryptConfigurationConstructor512SaltAndHashed()
        {
            BCryptConfiguration configuration = new BCryptConfiguration(KeyDerivationPrf.HMACSHA512, 2, 1, 1);
            Assert.AreEqual(configuration.Prf, KeyDerivationPrf.HMACSHA512);
            Assert.AreEqual(configuration.IterationCount, 2);
            Assert.AreEqual(configuration.SaltBytesLength, 1);
            Assert.AreEqual(configuration.NumBytesRequestedLength, 1);
        }

        [Test]
        public void TestBCryptInstance()
        {
            BCrypt bCrypt = new BCrypt();            
            Assert.IsInstanceOf<BCrypt>(bCrypt);
        }

        [Test]
        public void TestBCryptConstructorReturnValue()
        {
            string password = "abcdef";
            BCryptConfiguration configuration = new BCryptConfiguration();
            BCrypt bCrypt = new BCrypt(configuration);
            BCryptValue bCryptValue = bCrypt.Hash(password);
            Assert.IsTrue(bCrypt.Valid(password, bCryptValue));            
        }

        [Test]
        public void TestBCryptConstructorReturnValueWithHMACSHA1()
        {
            string password = "abcdef";
            BCryptConfiguration configuration = new BCryptConfiguration(KeyDerivationPrf.HMACSHA1);
            BCrypt bCrypt = new BCrypt(configuration);
            BCryptValue bCryptValue = bCrypt.Hash(password);
            Assert.IsTrue(bCrypt.Valid(password, bCryptValue));
        }

        [Test]
        public void TestBCryptConstructorReturnValueWithHMACSHA256()
        {
            string password = "abcdef";
            BCryptConfiguration configuration = new BCryptConfiguration(KeyDerivationPrf.HMACSHA256);
            BCrypt bCrypt = new BCrypt(configuration);
            BCryptValue bCryptValue = bCrypt.Hash(password);
            Assert.IsTrue(bCrypt.Valid(password, bCryptValue));
        }

        [Test]
        public void TestBCryptConstructorReturnValueWithHMACSHA512()
        {
            string password = "abcdef";
            BCryptConfiguration configuration = new BCryptConfiguration(KeyDerivationPrf.HMACSHA512);
            BCrypt bCrypt = new BCrypt(configuration);
            BCryptValue bCryptValue = bCrypt.Hash(password);
            Assert.IsTrue(bCrypt.Valid(password, bCryptValue));
        }

        [Test]
        public void TestBCryptConstructorReturnValueWithHMACSHA1AndInterationCount()
        {
            string password = "abcdef";
            BCryptConfiguration configuration = new BCryptConfiguration(KeyDerivationPrf.HMACSHA1, 2000);
            BCrypt bCrypt = new BCrypt(configuration);
            BCryptValue bCryptValue = bCrypt.Hash(password);
            Assert.IsTrue(bCrypt.Valid(password, bCryptValue));
        }

        [Test]
        public void TestBCryptConstructorReturnValueWithHMACSHA256AndInterationCount()
        {
            string password = "abcdef";
            BCryptConfiguration configuration = new BCryptConfiguration(KeyDerivationPrf.HMACSHA256, 2000);
            BCrypt bCrypt = new BCrypt(configuration);
            BCryptValue bCryptValue = bCrypt.Hash(password);
            Assert.IsTrue(bCrypt.Valid(password, bCryptValue));
        }

        [Test]
        public void TestBCryptConstructorReturnValueWithHMACSHA512AndInterationCount()
        {
            string password = "@a$bcdef#";
            BCryptConfiguration configuration = new BCryptConfiguration(KeyDerivationPrf.HMACSHA512, 2000);
            BCrypt bCrypt = new BCrypt(configuration);
            BCryptValue bCryptValue = bCrypt.Hash(password);
            Assert.IsTrue(bCrypt.Valid(password, bCryptValue));
        }

        [Test]
        public void TestBCryptConstructorReturnValueWithHMACSHA1DescribeValue()
        {
            string password = "abcdef";
            BCryptConfiguration configuration = new BCryptConfiguration(KeyDerivationPrf.HMACSHA1);
            BCrypt bCrypt = new BCrypt(configuration);
            BCryptValue bCryptValue = bCrypt.Hash(password);
            Assert.IsTrue(bCrypt.Valid(password, bCryptValue.Salt, bCryptValue.Hashed));
        }

        [Test]
        public void TestBCryptConstructorReturnValueWithHMACSHA256DescribeValue()
        {
            string password = "abcdef";
            BCryptConfiguration configuration = new BCryptConfiguration(KeyDerivationPrf.HMACSHA256);
            BCrypt bCrypt = new BCrypt(configuration);
            BCryptValue bCryptValue = bCrypt.Hash(password);
            Assert.IsTrue(bCrypt.Valid(password, bCryptValue.Salt, bCryptValue.Hashed));
        }

        [Test]
        public void TestBCryptConstructorReturnValueWithHMACSHA512DescribeValue()
        {
            string password = "abcdef";
            BCryptConfiguration configuration = new BCryptConfiguration(KeyDerivationPrf.HMACSHA512);
            BCrypt bCrypt = new BCrypt(configuration);
            BCryptValue bCryptValue = bCrypt.Hash(password);
            Assert.IsTrue(bCrypt.Valid(password, bCryptValue.Salt, bCryptValue.Hashed));
        }

        [Test]
        public void TestBCryptConstructorReturnValueWithHMACSHA1AndInterationCountDescribeValue()
        {
            string password = "abcdef";
            BCryptConfiguration configuration = new BCryptConfiguration(KeyDerivationPrf.HMACSHA1, 2000);
            BCrypt bCrypt = new BCrypt(configuration);
            BCryptValue bCryptValue = bCrypt.Hash(password);
            Assert.IsTrue(bCrypt.Valid(password, bCryptValue.Salt, bCryptValue.Hashed));
        }

        [Test]
        public void TestBCryptConstructorReturnValueWithHMACSHA256AndInterationCountDescribeValue()
        {
            string password = "abcdef";
            BCryptConfiguration configuration = new BCryptConfiguration(KeyDerivationPrf.HMACSHA256, 2000);
            BCrypt bCrypt = new BCrypt(configuration);
            BCryptValue bCryptValue = bCrypt.Hash(password);
            Assert.IsTrue(bCrypt.Valid(password, bCryptValue.Salt, bCryptValue.Hashed));
        }

        [Test]
        public void TestBCryptConstructorReturnValueWithHMACSHA512AndInterationCountDescribeValue()
        {
            string password = "@a$bcdef#";
            BCryptConfiguration configuration = new BCryptConfiguration(KeyDerivationPrf.HMACSHA512, 2000);
            BCrypt bCrypt = new BCrypt(configuration);
            BCryptValue bCryptValue = bCrypt.Hash(password);
            Assert.IsTrue(bCrypt.Valid(password, bCryptValue.Salt, bCryptValue.Hashed));
        }

        [Test]
        public void TestBCryptConstructorReturnValueWithHMACSHA512AndInterationCountDescribeValueSaltAndHashedLength()
        {
            const int length = 75;
            string password = "@a$bcdef#";
            BCryptConfiguration configuration = 
                new BCryptConfiguration(KeyDerivationPrf.HMACSHA512, 2000, length, length);
            BCrypt bCrypt = new BCrypt(configuration);
            BCryptValue bCryptValue = bCrypt.Hash(password);
            Assert.IsTrue(bCrypt.Valid(password, bCryptValue.Salt, bCryptValue.Hashed));
            Assert.AreEqual(2000, configuration.IterationCount);
            Assert.AreEqual(length, configuration.SaltBytesLength);
            Assert.AreEqual(length, configuration.NumBytesRequestedLength);
            Assert.AreEqual(bCryptValue.Salt.Length, 100);
            Assert.AreEqual(bCryptValue.Hashed.Length, 100);
        }
    }
}