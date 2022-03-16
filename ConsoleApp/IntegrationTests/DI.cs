using BL.Interfaces;
using BL.Mapping;
using BL.Services;
using BL.Validators;
using DAL.Database;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Config;
using Shared.Extensions;

namespace IntegrationTests
{
    public static class DI
    {
        public static IServiceCollection AddDI(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=mentorshipdb;Trusted_Connection=True;"));

            services.AddScoped<IWeatherRepository, WeatherRepository>();
            services.AddTransient<IWeatherHistoryRepository, WeatherHistoryRepository>();
            services.AddScoped<ICityRepository, CityRepository>();

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

            services.AddAutoMapper();

            return services;
        }
    }
}
