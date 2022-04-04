using DAL.Interfaces;
using DAL.Repositories;

namespace WebAPI.Extensions
{
    public static class RegisterRepositories
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IWeatherRepository, WeatherRepository>();
            services.AddScoped<IWeatherHistoryRepository, WeatherHistoryRepository>();
            services.AddScoped<ICityRepository, CityRepository>();

            return services;
        }
    }
}
