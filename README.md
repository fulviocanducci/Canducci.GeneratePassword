# Canducci GeneratePassword


[![NuGet](https://img.shields.io/nuget/v/Canducci.GeneratePassword.svg?style=plastic&label=version)](https://www.nuget.org/packages/Canducci.GeneratePassword/)
[![NuGet](https://img.shields.io/nuget/dt/Canducci.GeneratePassword.svg)](https://www.nuget.org/packages/Canducci.GeneratePassword/)
[![.NET Core](https://github.com/fulviocanducci/Canducci.GeneratePassword/workflows/.NET%20Core/badge.svg)](https://www.nuget.org/packages/Canducci.GeneratePassword/)

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

Declare o namespace `using Canducci.GeneratePassword;` and right after declaring a variable with the class `BCrypt`, example:

__Generate the encrypted `password` and `salt`:__

```csharp
string password = "abc@#$%12";
BCryptConfiguration config = new BCryptConfiguration();
BCrypt crypt = new BCrypt(config);
BCryptValue cryptValue = bCrypt.Hash(password);
```


__Test the password__

```csharp
string password = "abc@#$%12";
BCryptConfiguration config = new BCryptConfiguration();
BCrypt crypt = new BCrypt(config);
BCryptValue cryptValue = bCrypt.Hash(password);
bool valid = crpt.Valid(password, bCryptValue); 
// ou
//bool valid = crpt.Valid(password, bCryptValue.Salt, bCryptValue.Hashed); 
```


### Web

[![Project Web Example]()](https://github.com/fulviocanducci/Canducci.GeneratePassword/tree/master/Test.WebApplication)

Configure o method `ConfigureServices` with

    services.AddGeneratePassword();

In the constructor

```csharp
public class HomeController
{
    private readonly BCrypt _crypt;
    public HomeController(BCrypt crypt)
    {
        _crypt = crypt;
    }
}
```