using DAL.Entities;
using DAL.Entities.WeatherForecastEntities;
using DAL.Interfaces;
using Newtonsoft.Json;
using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        public async Task<Root> GetWeatherAsync(string cityName)
        {
            try
            {
                var responseMessage = await _client.GetAsync($"{_config.URL}{cityName}&lang={_config.Lang}&units={_config.Units}&appid={_config.APIKey}");

                if (responseMessage.IsSuccessStatusCode == false)
                    return null;

                var weather = await responseMessage.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Root>(weather);
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

            result.Daily = result.Daily.Where(x => x.Date.Hour == _config.Hours).Select(x => x).ToList();

            return result;
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
