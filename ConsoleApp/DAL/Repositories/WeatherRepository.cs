using DAL.Entities;
using DAL.Entities.WeatherForecastEntities;
using DAL.Interfaces;
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

        public async Task<CurrentWeather> GetWeatherAsync(string cityName)
        {
            try
            {
                var responseMessage = await _client.GetAsync($"{_config.URL}{cityName}&lang={_config.Lang}&units={_config.Units}&appid={_config.APIKey}");

                if (responseMessage.IsSuccessStatusCode == false)
                    return null;

                var weather = await responseMessage.Content.ReadAsStringAsync();

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

        public List<MaxTemperature> GetTemperatures(List<string> cityNames)
        {
            var temperatures = new ConcurrentBag<MaxTemperature>();
            var locker = new object();
            var amountCities = cityNames.Count;
            var x = 0;

            Parallel.For(0, amountCities, i =>
            {
                var stopwatch = new Stopwatch();

                lock (locker)
                {
                    stopwatch.Start();

                    var weather = GetWeatherAsync(cityNames[x]).Result;

                    stopwatch.Stop();

                    var maxTemp = new MaxTemperature();

                    if (weather == null)
                    {
                        maxTemp.CountFailedRequests++;
                        maxTemp.RunTime = stopwatch.ElapsedMilliseconds;
                    }
                    else
                    {
                        maxTemp.CityName = weather.Name;
                        maxTemp.Temp = weather.Main.Temp;
                        maxTemp.CountSuccessfullRequests++;
                        maxTemp.RunTime = stopwatch.ElapsedMilliseconds;
                    }

                    temperatures.Add(maxTemp);
                    x++;

                    Monitor.Pulse(locker);
                }
            });

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
    }
}
