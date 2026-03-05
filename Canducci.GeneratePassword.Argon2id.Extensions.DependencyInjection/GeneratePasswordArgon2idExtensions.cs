using System;
using Canducci.GeneratePassword.Argon2id;
using Microsoft.Extensions.DependencyInjection;

namespace Canducci.GeneratePassword.Argon2id.Extensions.DependencyInjection
{
    public static class GeneratePasswordArgon2idExtensions
    {
        public static IServiceCollection AddArgon2idPasswordHasher(
            this IServiceCollection services,
            Action<Argon2idConfiguration> configuration = null)
        {
            Argon2idConfiguration config = new Argon2idConfiguration();
            configuration?.Invoke(config);

            services.AddScoped<IArgon2idConfiguration>(_ => config);
            services.AddScoped(_ => config);
            services.AddScoped<IArgon2idPasswordHasher, Argon2idPasswordHasher>();
            services.AddScoped<Argon2idPasswordHasher>();

            return services;
        }
    }
}
