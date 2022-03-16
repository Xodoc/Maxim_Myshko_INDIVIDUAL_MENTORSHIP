using BL.DTOs;
using BL.Interfaces;
using DAL.Entities.WeatherHistoryEntities;
using DAL.Interfaces;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BL.Services
{
    public class WeatherHistoryService : IWeatherHistoryService
    {
        private readonly IWeatherHistoryRepository _weatherHistoryRepository;
        private readonly IWeatherRepository _weatherRepository;

        public WeatherHistoryService(IWeatherHistoryRepository weatherHistoryRepository, IWeatherRepository weatherRepository)
        {
            _weatherHistoryRepository = weatherHistoryRepository;
            _weatherRepository = weatherRepository;
        }

        public async Task AddWeatherHistoryAsync(CityDTO city, CancellationToken token)
        {
            Log.Information("Method AddWeatherHistoryAsync has been run!");

            var weather = await _weatherRepository.GetWeatherAsync(city.CityName, token);
            var weatherHistory = new WeatherHistory
            {
                Timestapm = DateTime.Now,
                CityId = city.Id,
                Temp = weather.Main.Temp
            };

            await _weatherHistoryRepository.CreateAsync(weatherHistory);

            Log.Information("Method AddWeatherHistoryAsync is complited!");
        }
    }
}
