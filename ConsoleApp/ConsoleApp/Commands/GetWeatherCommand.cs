using BL.Interfaces;
using Shared.Interfaces;
using System;
using System.Threading.Tasks;

namespace ConsoleApp.Commands
{
    public class GetWeatherCommand : ICommand
    {
        private readonly IWeatherService _weatherService;

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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\n");
            }
        }
    }
}
