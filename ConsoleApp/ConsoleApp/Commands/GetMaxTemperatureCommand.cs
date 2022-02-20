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

        public string Title => "Find max temperature";

        public GetMaxTemperatureCommand(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public async Task Execute()
        {
            Console.Write("Input city names: ");
            var cities = Console.ReadLine();
            var array = cities.Split(new string[] {" "}, StringSplitOptions.RemoveEmptyEntries);
            
            var cityNames = new List<string>();

            foreach (var item in array) 
            {
                cityNames.Add(item);
            }
           
            Console.Clear();
            var result = await _weatherService.GetMaxTemperatureAsync(cityNames);

            Console.WriteLine(result);
        }
    }
}
