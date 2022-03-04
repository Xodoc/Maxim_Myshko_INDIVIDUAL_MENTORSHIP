using BL.Interfaces;
using DAL.Entities.WeatherHistoryEntities;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BL.Services
{
    public class WeatherHistoryService : IWeatherHistoryService
    {
        private readonly IWeatherHistoryRepository _weatherHistoryRepository;
        private CancellationTokenSource _cts;

        public WeatherHistoryService(IWeatherHistoryRepository weatherHistoryRepository)
        {
            _weatherHistoryRepository = weatherHistoryRepository;
        }

        public async Task AddWeatherHistoryAsync()
        {
            _cts = new CancellationTokenSource();

            await _weatherHistoryRepository.AddWeatherHistoryAsync(_cts.Token);            
        }
    }
}
