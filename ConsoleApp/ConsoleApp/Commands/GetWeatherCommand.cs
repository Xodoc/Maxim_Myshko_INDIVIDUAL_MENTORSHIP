using BL.Interfaces;
using Shared.Interfaces;
using System;
using System.Threading.Tasks;

namespace ConsoleApp.Commands
{
    public class GetWeatherCommand : ICommand
    {
        private readonly IWeatherService _weatherService;

        public string Title => "Show weather";

        public GetWeatherCommand(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public async Task Execute()
        {
            Console.Write("Input city name: ");
            var cityName = Console.ReadLine();

            var weather = await _weatherService.GetWeatherAsync(cityName);

            Console.WriteLine(weather);
        }
    }
}
