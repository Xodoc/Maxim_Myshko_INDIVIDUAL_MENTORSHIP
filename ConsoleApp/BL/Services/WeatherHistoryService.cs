using AutoMapper;
using BL.DTOs;
using BL.Interfaces;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BL.Services
{
    public class WeatherHistoryService : IWeatherHistoryService
    {
        private readonly IMapper _mapper;
        private readonly IWeatherHistoryRepository _weatherHistoryRepository;
        private CancellationTokenSource _cts;

        public WeatherHistoryService(IWeatherHistoryRepository weatherHistoryRepository, IMapper mapper)
        {
            _weatherHistoryRepository = weatherHistoryRepository;
            _mapper = mapper;
        }

        public async Task AddWeatherHistoryAsync()
        {
            _cts = new CancellationTokenSource();

            await _weatherHistoryRepository.AddWeatherHistoryAsync(_cts.Token);
        }

        public async Task<List<WeatherHistoryDTO>> GetWeatherHistoriesAsync(string cityName, string date)
        {
            var histories = await _weatherHistoryRepository.GetWeatherHistoriesAsync(cityName, date);

            return _mapper.Map<List<WeatherHistoryDTO>>(histories);
        }
    }
}
