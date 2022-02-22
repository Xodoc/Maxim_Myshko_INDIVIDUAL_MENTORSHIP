using BL.Interfaces;
using ConsoleApp.Commands.HelperClasses;
using Shared.Interfaces;
using System;
using System.Threading.Tasks;

namespace ConsoleApp.Commands
{
    public class GetMaxTemperatureCommand : ICommand
    {
        private readonly IWeatherService _weatherService;

        public string Title => "Find max temperature";

        public GetMaxTemperatureCommand(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public async Task Execute()
        {
            Console.Write("Input city names: ");
            var cities = Console.ReadLine();

            var cityNames = new StringSplit(cities).SplitNames();            
           
            Console.Clear();

            var result = await _weatherService.GetMaxTemperatureAsync(cityNames);

            Console.WriteLine(result);
        }
    }
}
