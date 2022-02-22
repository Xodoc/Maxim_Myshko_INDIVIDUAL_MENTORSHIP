using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp.Extensions
{
    public static class RegisterRepositories
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services) 
        {
            services.AddScoped<IWeatherRepository, WeatherRepository>();

            return services;
        }
    }
}
