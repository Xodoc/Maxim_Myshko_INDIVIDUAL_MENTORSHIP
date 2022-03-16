using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace WindowsBackgroundService.Extensions
{
    public static class RegisterRepositories
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IWeatherRepository, WeatherRepository>();
            services.AddTransient<IWeatherHistoryRepository, WeatherHistoryRepository>();
            services.AddScoped<ICityRepository, CityRepository>();

            return services;
        }
    }
}
