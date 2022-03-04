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

        public async Task<CurrentWeather> GetWeatherAsync(string cityName, CancellationToken ct)
        {
            try
            {
                var responseMessage = await _client.GetAsync($"{_config.URL}{cityName}&lang={_config.Lang}&units={_config.Units}&appid={_config.APIKey}");

                if (responseMessage.IsSuccessStatusCode == false)
                    return null;

                var weather = await responseMessage.Content.ReadAsStringAsync();

                if (ct.IsCancellationRequested == true)
                {
                    ct.ThrowIfCancellationRequested();
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

            var url = $"{_config.Forecast}lat={coord.Lat}&lon={coord.Lon}&units={_config.Units}&appid={_config.APIKey}";

            var responseMessage = await _client.GetAsync(url);

            var weather = await responseMessage.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<WeatherForecast>(weather);

            result.CityName = cityName;

            result.Daily = result.Daily.Where(x => x.Date.Hour == _config.Hours).Select(x => x).Take(days).ToList();

            return result;
        }

        public async Task<List<TemperatureInfo>> GetTemperaturesAsync(IEnumerable<string> cityNames, CancellationToken ct)
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
                    weather = await GetWeatherAsync(name, ct);
                    stopwatch.Stop();

                    if (ct.IsCancellationRequested == true)
                    {
                        temperatures.Add(new TemperatureInfo { Canceled = 1 });
                        return;
                    }

                    var info = SetTempInfo(weather, stopwatch, name);

                    temperatures.Add(info);

                }, ct);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Some operations have been canceled");
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

        private TemperatureInfo SetTempInfo(CurrentWeather weather, Stopwatch stopwatch, string cityName)
        {
            var info = new TemperatureInfo();

            if (weather == null)
            {
                info.FailedRequest++;
                info.RunTime = stopwatch.ElapsedMilliseconds;
                info.CityName = cityName;
            }
            else
            {
                info.CityName = weather.Name;
                info.Temp = weather.Main.Temp;
                info.SuccessfullRequest++;
                info.RunTime = stopwatch.ElapsedMilliseconds;
            }

            return info;
        }
    }
}
