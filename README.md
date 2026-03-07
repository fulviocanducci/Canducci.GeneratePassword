# Canducci GeneratePassword

[![Run Unit Tests](https://github.com/fulviocanducci/Canducci.GeneratePassword/actions/workflows/test.yml/badge.svg)](https://github.com/fulviocanducci/Canducci.GeneratePassword/actions/workflows/test.yml)
[![Coverage Status](https://coveralls.io/repos/github/fulviocanducci/Canducci.GeneratePassword/badge.svg?branch=master)](https://coveralls.io/github/fulviocanducci/Canducci.GeneratePassword?branch=master)

Biblioteca para hash de senha com dois conjuntos separados:

#### Argon2id (padrao recomendado)

[![](https://img.shields.io/nuget/dt/Canducci.GeneratePassword.Argon2id.svg)](https://www.nuget.org/packages/Canducci.GeneratePassword.Argon2id/)
[![NuGet](https://img.shields.io/nuget/v/Canducci.GeneratePassword.Argon2id.svg?style=plastic&label=version)](https://www.nuget.org/packages/Canducci.GeneratePassword.Argon2id/)
 
#### PBKDF2 (compatibilidade e cenarios especificos)

[![](https://img.shields.io/nuget/dt/Canducci.GeneratePassword.svg)](https://www.nuget.org/packages/Canducci.GeneratePassword/)
[![NuGet](https://img.shields.io/nuget/v/Canducci.GeneratePassword.svg?style=plastic&label=version)](https://www.nuget.org/packages/Canducci.GeneratePassword/)
 
## Pacotes NuGet

### Argon2id (padrao recomendado)

```powershell
PM> Install-Package Canducci.GeneratePassword.Argon2id
PM> Install-Package Canducci.GeneratePassword.Argon2id.Extensions.DependencyInjection
```

### PBKDF2

```powershell
PM> Install-Package Canducci.GeneratePassword
PM> Install-Package Canducci.GeneratePassword.Extensions.DependencyInjection
```

## Uso rapido - Argon2id (padrao)

### Biblioteca (sem DI)

```csharp
using Canducci.GeneratePassword.Argon2id;

string password = "abc@#$%12";

IArgon2idConfiguration config = new Argon2idConfiguration();
IArgon2idPasswordHasher hasher = new Argon2idPasswordHasher(config);

// Gera salt + hash separados
IArgon2idValue value = hasher.Hash(password);

// Valida senha
bool valid1 = hasher.Valid(password, value);
bool valid2 = hasher.Valid(password, value.Salt, value.Hashed);

// Formato versionado para armazenamento:
// argon2id-v1$iterations$memoryKb$parallelism$salt$hash
string encoded = hasher.HashEncoded(password);
bool valid3 = hasher.ValidEncoded(password, encoded);
```

### ASP.NET Core com DI

```csharp
using Canducci.GeneratePassword.Argon2id;
using Canducci.GeneratePassword.Argon2id.Extensions.DependencyInjection;

public void ConfigureServices(IServiceCollection services)
{
    services.AddArgon2idPasswordHasher(config =>
    {
        // Opcionais: defaults seguros ja sao aplicados
        config.IterationCount = Argon2idConfiguration.DefaultIterationCount;
        config.MemorySizeKb = Argon2idConfiguration.DefaultMemorySizeKb;
        config.DegreeOfParallelism = Argon2idConfiguration.DefaultDegreeOfParallelism;
    });
}
```

```csharp
using Canducci.GeneratePassword.Argon2id;

public class HomeController
{
    private readonly IArgon2idPasswordHasher _hasher;

    public HomeController(IArgon2idPasswordHasher hasher)
    {
        _hasher = hasher;
    }
}
```

## Uso rapido - PBKDF2

```csharp
using Canducci.GeneratePassword;

IPbkdf2Configuration config = new Pbkdf2Configuration();
IPbkdf2PasswordHasher hasher = new Pbkdf2PasswordHasher(config);

IPbkdf2Value value = hasher.Hash("abc@#$%12");
bool valid = hasher.Valid("abc@#$%12", value);
```

## Migracao PBKDF2 -> Argon2id

Estratégia recomendada: migracao progressiva no login (sem reset de senha em massa).

Fluxo:

1. Tente validar primeiro com Argon2id.
2. Se falhar, tente validar com PBKDF2 legado.
3. Se PBKDF2 validar, gere novo hash Argon2id e atualize no banco.
4. Marque o registro como Argon2id (ou salve no formato encoded com prefixo de algoritmo).

Exemplo:

```csharp
using Canducci.GeneratePassword;
using Canducci.GeneratePassword.Argon2id;

public bool ValidateAndRehash(
    string password,
    string algorithm,           // "argon2id" ou "pbkdf2"
    string salt,
    string hash,
    Action<string, string, string> updatePassword) // (algorithm, salt, hash)
{
    IArgon2idPasswordHasher argon = new Argon2idPasswordHasher(new Argon2idConfiguration());
    IPbkdf2PasswordHasher pbkdf2 = new Pbkdf2PasswordHasher(new Pbkdf2Configuration());

    if (algorithm == "argon2id")
    {
        return argon.Valid(password, salt, hash);
    }

    if (algorithm == "pbkdf2")
    {
        bool validLegacy = pbkdf2.Valid(password, salt, hash);
        if (!validLegacy)
        {
            return false;
        }

        // Rehash automatico para Argon2id apos login valido
        IArgon2idValue newValue = argon.Hash(password);
        updatePassword("argon2id", newValue.Salt, newValue.Hashed);
        return true;
    }

    return false;
}
```

## Compatibilidade legada

`BCrypt`, `BCryptConfiguration`, `BCryptValue` e `AddGeneratePassword` continuam disponiveis somente por compatibilidade e estao marcados como `Obsolete`.
