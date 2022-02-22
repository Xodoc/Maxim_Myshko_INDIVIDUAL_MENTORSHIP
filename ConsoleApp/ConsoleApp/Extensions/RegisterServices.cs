using BL.Interfaces;
using BL.Services;
using Microsoft.Extensions.DependencyInjection;
using Shared.Config;
using Shared.Interfaces;
using Shared.Extensions;
using BL.Validators;

namespace ConsoleApp.Extensions
{
    public static class RegisterServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services) 
        {
            services.AddScoped<IWeatherService, WeatherService>();
            services.AddScoped<IValidator, Validator>();
            services.AddSingleton<IConfiguration>(context => 
            {
                var config = new Configuration();
                config.GetConfig();

                return config;
            });

            return services;
        }
    }
}
