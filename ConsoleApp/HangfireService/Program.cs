using BL.DTOs;
using BL.Interfaces;
using BL.Mapping;
using DAL.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WindowsBackgroundService;
using WindowsBackgroundService.Extensions;
using static Shared.Constants.ConfigurationConstants;

class Program
{
    private static ICityService _cityService;
    private static IWeatherHistoryService _weatherHistoryService;
    private static IConfiguration _config;

    private static string GetConnectionString()
    {
        _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        return _config.GetConnectionString(ConnectionString);
    }

    private static void RegisterAndGetServices()
    {
        var connection = GetConnectionString();

        var serviceProvider = new ServiceCollection()
         .AddRepositories()
         .AddServices()
         .AddAutoMapper()
         .AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection))
         .BuildServiceProvider();

        _weatherHistoryService = serviceProvider.GetService<IWeatherHistoryService>();
        _cityService = serviceProvider.GetService<ICityService>();
    }

    private static async Task RunServiceAsync(CityDTO city, double timeInterval, CancellationToken token)
    {
        var service = new WindowsService(_weatherHistoryService, city, timeInterval);

        await service.StartAsync(token);
    }

    private static void Exit()
    {
        while (Console.ReadKey().Key != ConsoleKey.Q)
        {
            Console.Clear();
        }

        Console.WriteLine("\nOperation was canceled");
    }

    static async Task Main(string[] args)
    {
        RegisterAndGetServices();

        var cityNames = await _cityService.CheckAndCreateCitiesAsync();

        var timeStamps = _config.GetSection("TimeInterval").GetChildren().Select(x => double.Parse(x.Value)).ToArray();

        await Parallel.ForEachAsync(cityNames, async (name, token) =>
        {
            var index = cityNames.IndexOf(name);

            await RunServiceAsync(name, timeStamps[index], token);
        });

        Exit();
    }
}
