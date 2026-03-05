# Canducci GeneratePassword


[![NuGet](https://img.shields.io/nuget/v/Canducci.GeneratePassword.svg?style=plastic&label=version)](https://www.nuget.org/packages/Canducci.GeneratePassword/)
[![NuGet](https://img.shields.io/nuget/dt/Canducci.GeneratePassword.svg)](https://www.nuget.org/packages/Canducci.GeneratePassword/)
[![.NET Core](https://github.com/fulviocanducci/Canducci.GeneratePassword/workflows/.NET%20Core/badge.svg)](https://www.nuget.org/packages/Canducci.GeneratePassword/) [![Coverage Status](https://coveralls.io/repos/github/fulviocanducci/Canducci.GeneratePassword/badge.svg?branch=master)](https://coveralls.io/github/fulviocanducci/Canducci.GeneratePassword?branch=master)

[![NuGet](https://img.shields.io/nuget/v/Canducci.GeneratePassword.Extensions.DependencyInjection.svg?style=plastic&label=version)](https://www.nuget.org/packages/Canducci.GeneratePassword.Extensions.DependencyInjection/)
[![NuGet](https://img.shields.io/nuget/dt/Canducci.GeneratePassword.Extensions.DependencyInjection.svg)](https://www.nuget.org/packages/Canducci.GeneratePassword.Extensions.DependencyInjection/)

### Package Installation (NUGET)

```Csharp
PM> Install-Package Canducci.GeneratePassword
```

or Web Project Core

```Csharp
PM> Install-Package Canducci.GeneratePassword.Extensions.DependencyInjection
```

### How to use?

Declare o namespace `using Canducci.GeneratePassword;` and right after declaring a variable with the class `Pbkdf2PasswordHasher`, example:

__Generate the encrypted `password` and `salt`:__

```csharp
string password = "abc@#$%12";
Pbkdf2Configuration config = new Pbkdf2Configuration();
IPbkdf2PasswordHasher crypt = new Pbkdf2PasswordHasher(config);
IPbkdf2Value cryptValue = crypt.Hash(password);

// formato versionado para armazenamento:
// pbkdf2-v1$prf$iterations$numBytes$salt$hash
string encoded = crypt.HashEncoded(password);
```


__Test the password__

```csharp
string password = "abc@#$%12";
Pbkdf2Configuration config = new Pbkdf2Configuration();
IPbkdf2PasswordHasher crypt = new Pbkdf2PasswordHasher(config);
IPbkdf2Value cryptValue = crypt.Hash(password);
bool valid = crypt.Valid(password, cryptValue); 
// ou
//bool valid = crypt.Valid(password, cryptValue.Salt, cryptValue.Hashed); 
// ou
//bool valid = crypt.ValidEncoded(password, encoded);
```


### Web

[Project Web Complete](https://github.com/fulviocanducci/Canducci.GeneratePassword/tree/master/Test.WebApplication)

Configure o method `ConfigureServices` with

    services.AddPbkdf2PasswordHasher();

In the constructor

```csharp
public class HomeController
{
    private readonly IPbkdf2PasswordHasher _crypt;
    public HomeController(IPbkdf2PasswordHasher crypt)
    {
        _crypt = crypt;
    }
}
```

### Legacy Compatibility

`BCrypt`, `BCryptConfiguration`, `BCryptValue` e `AddGeneratePassword` continuam disponíveis somente por compatibilidade e foram marcados como `Obsolete`.
