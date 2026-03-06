using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Canducci.GeneratePassword
{
    public interface IPbkdf2Configuration
    {
        KeyDerivationPrf Prf { get; set; }
        int IterationCount { get; set; }
        int SaltBytesLength { get; set; }
        int NumBytesRequestedLength { get; set; }
    }
}
