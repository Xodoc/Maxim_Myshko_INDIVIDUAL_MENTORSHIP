using BL.Interfaces;
using BL.Services;
using BL.Validators;
using Microsoft.Extensions.DependencyInjection;
using Shared.Config;
using Shared.Extensions;

namespace WindowsBackgroundService.Extensions
{
    public static class RegisterServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<Shared.Interfaces.IConfiguration>(context =>
            {
                var config = new Configuration();
                config.GetWebAPIConfig();

                return config;
            });
            services.AddScoped<IWeatherService, WeatherService>();
            services.AddScoped<IValidator, Validator>();
            services.AddTransient<IWeatherHistoryService, WeatherHistoryService>();
            services.AddScoped<ICityService, CityService>();

            return services;
        }
    }
}
