using BL.DTOs;
using BL.Interfaces;
using BL.Mapping;
using DAL.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
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
        Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(_config).CreateLogger();
        var serviceProvider = new ServiceCollection()
         .AddRepositories()
         .AddServices()
         .AddAutoMapper()
         .AddLogging(x => x.AddSerilog())
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

    private static void Exit(CancellationTokenSource cts)
    {
        while (Console.ReadKey().Key != ConsoleKey.Q)
        {
            Console.Clear();
        }
        try
        {
            cts.Cancel();
            cts.Token.ThrowIfCancellationRequested();
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("\nOperation was canceled");
        }
    }

    static async Task Main(string[] args)
    {
        RegisterAndGetServices();

        var cities = await _cityService.CheckAndCreateCitiesAsync();

        var timeStamps = _config.GetSection("TimeInterval").GetChildren().Select(x => double.Parse(x.Value)).ToArray();
        var cts = new CancellationTokenSource();

        var tasks = cities.Select(async city =>
        {
            var index = cities.IndexOf(city);
            await RunServiceAsync(city, timeStamps[index], cts.Token);

        }).ToList();

        await Task.WhenAll(tasks);

        Exit(cts);
    }
}
