using BL.Interfaces;
using BL.Services;
using BL.Validators;
using Shared.Config;
using Shared.Extensions;

namespace WebAPI.Extensions
{
    public static class RegisterServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IWeatherService, WeatherService>();
            services.AddScoped<IValidator, Validator>();
            services.AddSingleton<Shared.Interfaces.IConfiguration>(context =>
            {
                var config = new Configuration();
                config.GetConfig();

                return config;
            });

            return services;
        }
    }
}
