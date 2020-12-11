using Microsoft.Extensions.DependencyInjection;
using System;

namespace Canducci.GeneratePassword.Extensions.DependencyInjection
{
    public static class GeneratePasswordExtensions
    {
        /// <summary>
        /// static class GeneratePasswordExtensions
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="configuration">BCryptConfiguration</param>
        /// <returns></returns>
        public static IServiceCollection AddGeneratePassword(
            this IServiceCollection services, 
            Action<BCryptConfiguration> configuration = null)
        {
            BCryptConfiguration config = new BCryptConfiguration();

            if (configuration != null)
            { 
                configuration?.Invoke(config); 
            }

            services.AddScoped(_ => config);
            services.AddScoped<BCrypt>();

            return services;
        }
    }
}
