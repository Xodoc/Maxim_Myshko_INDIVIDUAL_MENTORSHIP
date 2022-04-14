using BL.Interfaces;
using BL.Services;
using BL.Validators;
using Shared.Config;
using Shared.Extensions;
using WebAPI.HangfireSettings;

namespace WebAPI.Extensions
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
            services.AddScoped<IWeatherHistoryService, WeatherHistoryService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<Settings>();

            return services;
        }
    }
}
