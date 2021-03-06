using BL.DTOs;
using BL.Interfaces;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Diagnostics;

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

        protected override async Task ExecuteAsync(CancellationToken token)
        {
            var stopwatch = new Stopwatch();

            while (!token.IsCancellationRequested)
            {
                stopwatch.Restart();
                await _weatherHistoryService.AddWeatherHistoryAsync(_city, token);
                stopwatch.Stop();

                var delta = _timeInterval - stopwatch.Elapsed.Seconds;

                Log.Information($"Request to the city of {_city.CityName} is complited!" +
                                $" Query execution time: {stopwatch.Elapsed.Seconds} seconds.");

                if (delta > 0)
                {
                    await Task.Delay(TimeSpan.FromSeconds(delta), token);
                }
            }
        }
    }
}
