using DAL.Entities;
using DAL.Interfaces;
using Newtonsoft.Json;
using Shared.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using static Shared.Constants.RequestConstants;


namespace DAL.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        private IConfiguration _configuration;
        private readonly HttpClient _client;

        public WeatherRepository(IConfiguration configuration)
        {
            _client = new HttpClient();
            _configuration = configuration;
        }

        public async Task<Root> GetWeatherAsync(string cityName)
        {
            try
            {
                var weather = await _client.GetStringAsync($"{URL}{cityName}&lang={LANG}&units={UNITS}&appid={_configuration.APIKey}");

                var result = JsonConvert.DeserializeObject<Root>(weather);

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
