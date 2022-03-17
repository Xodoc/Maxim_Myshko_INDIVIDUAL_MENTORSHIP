using AutoMapper;
using BL.DTOs;
using BL.Interfaces;
using DAL.Entities.WeatherHistoryEntities;
using DAL.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BL.Services
{
    public class WeatherHistoryService : IWeatherHistoryService
    {
        private readonly IWeatherHistoryRepository _weatherHistoryRepository;
        private readonly IWeatherRepository _weatherRepository;
        private readonly IMapper _mapper;

        public WeatherHistoryService(IWeatherHistoryRepository weatherHistoryRepository, IWeatherRepository weatherRepository,
            IMapper mapper)
        {
            _weatherHistoryRepository = weatherHistoryRepository;
            _weatherRepository = weatherRepository;
            _mapper = mapper;
        }

        public async Task AddWeatherHistoryAsync(CityDTO city, CancellationToken token)
        {
            Log.Information("Method AddWeatherHistoryAsync has been run!");

            var weather = await _weatherRepository.GetWeatherAsync(city.CityName, token);
            var weatherHistory = new WeatherHistory
            {
                Timestamp = DateTime.Now,
                CityId = city.Id,
                Temp = weather.Main.Temp
            };

            await _weatherHistoryRepository.CreateAsync(weatherHistory);

            Log.Information("Method AddWeatherHistoryAsync is complited!");
        }

        public async Task<List<WeatherHistoryDTO>> GetWeatherHistoriesAsync(string cityName, DateTime from, DateTime to)
        {
            var histories = await _weatherHistoryRepository.GetWeatherHistoriesAsync(cityName, from, to);

            return _mapper.Map<List<WeatherHistoryDTO>>(histories);
        }
    }
}
