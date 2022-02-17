using BL.Interfaces;
using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp.Commands
{
    public class GetMaxTemperatureCommand : ICommand
    {
        private readonly IWeatherService _weatherService;

        public string Title => "Show max temperature among cities";

        public GetMaxTemperatureCommand(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public async Task Execute()
        {
            Console.Write("Input number of cities: ");
            var amountCities = int.Parse(Console.ReadLine());

            var cityNames = new List<string>();

            for (int i = 0; i < amountCities; i++)
            {
                Console.Write($"\nInput city name {i+1}: ");

                cityNames.Add(Console.ReadLine());
            }

            Console.WriteLine("\nEnable DebugInfo display? (true/false)");
            var debugInfo = bool.Parse(Console.ReadLine());

            Console.Clear();
            var result = await _weatherService.GetMaxTemperatureAsync(cityNames, debugInfo);
            Console.WriteLine(result);
        }
    }
}
