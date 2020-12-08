# Canducci GeneratePassword


[![NuGet](https://img.shields.io/nuget/v/Canducci.GeneratePassword.svg?style=plastic&label=version)](https://www.nuget.org/packages/Canducci.GeneratePassword/)
[![NuGet](https://img.shields.io/nuget/dt/Canducci.GeneratePassword.svg)](https://www.nuget.org/packages/Canducci.GeneratePassword/)
[![.NET Core](https://github.com/fulviocanducci/Canducci.GeneratePassword/workflows/.NET%20Core/badge.svg)](https://www.nuget.org/packages/Canducci.GeneratePassword/)

### Package Installation (NUGET)

```Csharp

PM> Install-Package Canducci.GeneratePassword

```

### How to use?

Declare o namespace `using Canducci.GeneratePassword;` and right after declaring a variable with the class `BCrypt`, example:

__Generate the encrypted `password` and `salt`:__

```csharp
string password = "abc@#$%12";
BCrypt crypt = new BCrypt();
BCryptValue cryptValue = bCrypt.Hash(password);
```


__Test the password__

```csharp
string password = "abc@#$%12";
BCrypt crypt = new BCrypt();
BCryptValue cryptValue = bCrypt.Hash(password);
bool valid = crpt.Valid(password, bCryptValue); 
// ou
//bool valid = crpt.Valid(password, bCryptValue.Salt, bCryptValue.Hashed); 
```