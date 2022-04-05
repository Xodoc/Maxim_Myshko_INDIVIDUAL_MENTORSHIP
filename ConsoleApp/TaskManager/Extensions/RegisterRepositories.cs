using DAL.Interfaces;
using DAL.Repositories;

namespace TaskManager.Extensions
{
    public static class RegisterRepositories
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IWeatherHistoryRepository, WeatherHistoryRepository>();
            services.AddScoped<ICityRepository, CityRepository>();

            return services;
        }
    }
}
