using DAL.Entities;
using DAL.Interfaces;
using Newtonsoft.Json;
using Shared.Interfaces;
using System;
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

                var weather = await responseMessage.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<Root>(weather);

                if(result.cod == 404)
                    return null;

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
