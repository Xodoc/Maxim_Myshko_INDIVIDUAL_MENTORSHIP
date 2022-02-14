using BL.Interfaces;
using Shared.Interfaces;
using System;
using System.Threading.Tasks;

namespace ConsoleApp.Commands
{
    public class GetWeatherForecastCommand : ICommand
    {
        private readonly IWeatherService _weatherService;
        public string Title => "Show weather forecast";

        public GetWeatherForecastCommand(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }


        public async Task Execute()
        {
            Console.Write("Input city name: ");
            var cityName = Console.ReadLine();

            Console.Write("\nInput number of days: ");
            var days = int.Parse(Console.ReadLine());
            Console.Clear();

            var weatherForecast = await _weatherService.GetWeatherForecastAsync(cityName, days);

            Console.WriteLine(weatherForecast);
        }
    }
}
