using BL.DTOs;
using BL.Interfaces;
using Microsoft.Extensions.Hosting;

namespace WindowsBackgroundService
{
    public sealed class WindowsService : BackgroundService
    {
        private readonly CityDTO _city;
        private readonly double _timeInterval;
        private readonly IWeatherHistoryService _weatherHistoryService;

        public WindowsService(IWeatherHistoryService weatherHistoryService, CityDTO city, double timeInterval)
        {
            _weatherHistoryService = weatherHistoryService;
            _city = city;
            _timeInterval = timeInterval;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _weatherHistoryService.AddWeatherHistoryAsync(_city, stoppingToken);
                //Console.WriteLine("Data was saved!");
                await Task.Delay(TimeSpan.FromSeconds(_timeInterval), stoppingToken);
            }
        }
    }
}
