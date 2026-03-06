using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Canducci.GeneratePassword
{
    public interface IPbkdf2EncodedValue
    {
        KeyDerivationPrf Prf { get; }
        int IterationCount { get; }
        int NumBytesRequestedLength { get; }
        string Salt { get; }
        string Hashed { get; }
    }
}
