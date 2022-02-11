using BL.Interfaces;
using Shared.Interfaces;
using System;
using System.Threading.Tasks;

namespace ConsoleApp.Commands
{
    public class GetWeatherCommand : ICommand
    {
        private readonly IWeatherService _weatherService;

        public string Title => "\n0) Show weather";

        public GetWeatherCommand(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public async Task Execute()
        {
            try
            {
                Console.Write("Input city name: ");
                var cityName = Console.ReadLine();

                var weather = await _weatherService.GetWeatherAsync(cityName);

                Console.WriteLine(weather);
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\n");
                Console.ReadKey();
            }
        }
    }
}
