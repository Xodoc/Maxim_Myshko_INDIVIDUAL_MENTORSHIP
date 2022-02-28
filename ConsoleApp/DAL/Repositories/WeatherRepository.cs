using DAL.Entities;
using DAL.Entities.WeatherForecastEntities;
using DAL.Interfaces;
using Dasync.Collections;
using Newtonsoft.Json;
using Shared.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _client;

        public WeatherRepository(IConfiguration config)
        {
            _client = new HttpClient();
            _config = config;
        }

        public async Task<CurrentWeather> GetWeatherAsync(string cityName, CancellationTokenSource cts)
        {
            try
            {
                var responseMessage = await _client.GetAsync($"{_config.URL}{cityName}&lang={_config.Lang}&units={_config.Units}&appid={_config.APIKey}");

                if (responseMessage.IsSuccessStatusCode == false)
                    return null;

                var weather = await responseMessage.Content.ReadAsStringAsync();

                if (cts.IsCancellationRequested == true)
                {
                    cts.Token.ThrowIfCancellationRequested();
                }

                return JsonConvert.DeserializeObject<CurrentWeather>(weather);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<WeatherForecast> GetWeatherForecastAsync(string cityName, int days)
        {
            var coord = await GetWeatherCoordAsync(cityName);

            if (coord == null)
                return null;

            var url = $"{_config.Forecast}&lat={coord.lat}&lon={coord.lon}&units={_config.Units}&appid={_config.APIKey}";

            var responseMessage = await _client.GetAsync(url);

            var weather = await responseMessage.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<WeatherForecast>(weather);

            result.CityName = cityName;

            result.Daily = result.Daily.Where(x => x.Date.Hour == _config.Hours).Select(x => x).Take(days).ToList();

            return result;
        }

        public async Task<List<TemperatureInfo>> GetTemperaturesAsync(IEnumerable<string> cityNames, CancellationTokenSource cts)
        {
            var temperatures = new ConcurrentBag<TemperatureInfo>();
            var amountCities = cityNames.Count();
            try
            {
                await cityNames.ParallelForEachAsync(async name =>
                {
                    var stopwatch = new Stopwatch();
                    var weather = new CurrentWeather();

                    stopwatch.Start();
                    weather = await GetWeatherAsync(name, cts);
                    stopwatch.Stop();

                    if (cts.IsCancellationRequested == true)
                    {
                        temperatures.Add(new TemperatureInfo
                        {
                            Canceled = 1,
                            RunTime = stopwatch.ElapsedMilliseconds,
                            FailedRequest = 1
                        });
                        return;
                    }

                    var info = SetTempInfo(weather, stopwatch);

                    temperatures.Add(info);

                }, cts.Token);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("1");
            }
            finally
            {
                cts.Dispose();
            }
            return temperatures.ToList();
        }

        private async Task<Geolocation> GetWeatherCoordAsync(string cityName)
        {
            try
            {
                var responseMessage = await _client.GetAsync($"{_config.URLGeo}{cityName}&appid={_config.APIKey}");

                if (responseMessage.IsSuccessStatusCode == false)
                    return null;

                var weatherCoord = await responseMessage.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<Geolocation>>(weatherCoord)[0];
            }
            catch (Exception)
            {
                return null;
            }
        }

        private TemperatureInfo SetTempInfo(CurrentWeather weather, Stopwatch stopwatch)
        {
            var info = new TemperatureInfo();
            
            info.CityName = weather.Name;
            info.Temp = weather.Main.Temp;
            info.SuccessfullRequest++;
            info.RunTime = stopwatch.ElapsedMilliseconds;

            return info;
        }
    }
}
