using System;
using Microsoft.Extensions.DependencyInjection;

namespace Canducci.GeneratePassword.Extensions.DependencyInjection
{
    public static class GeneratePasswordExtensions
    {
        /// <summary>
        /// Registers PBKDF2 hasher services.
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="configuration">Pbkdf2Configuration</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddPbkdf2PasswordHasher(
            this IServiceCollection services,
            Action<Pbkdf2Configuration> configuration = null)
        {
            Pbkdf2Configuration config = new Pbkdf2Configuration();
            configuration?.Invoke(config);

            services.AddScoped<IPbkdf2Configuration>(_ => config);
            services.AddScoped(_ => config);
            services.AddScoped<IPbkdf2PasswordHasher, Pbkdf2PasswordHasher>();
            services.AddScoped<Pbkdf2PasswordHasher>();

            return services;
        }

        /// <summary>
        /// Legacy registration method.
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="configuration">BCryptConfiguration</param>
        /// <returns>IServiceCollection</returns>
        [Obsolete("AddGeneratePassword is a legacy name. Use AddPbkdf2PasswordHasher instead.")]
        public static IServiceCollection AddGeneratePassword(
            this IServiceCollection services,
            Action<BCryptConfiguration> configuration = null)
        {
            BCryptConfiguration config = new BCryptConfiguration();
            configuration?.Invoke(config);

            services.AddScoped<IPbkdf2Configuration>(_ => config);
            services.AddScoped(_ => config);
            services.AddScoped<IPbkdf2PasswordHasher, Pbkdf2PasswordHasher>();
            services.AddScoped<Pbkdf2PasswordHasher>();
            services.AddScoped<BCrypt>();

            return services;
        }
    }
}
