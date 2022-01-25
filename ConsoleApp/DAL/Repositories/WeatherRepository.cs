using DAL.Entities;
using DAL.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        private const string APIKEY = "040b95fb163277b9ba8832454277fa9d";
        private const string UNITS = "metric";
        private const string LANG = "eng";

        private static readonly HttpClient _client = new HttpClient();

        public async Task<Root> GetWeatherAndParseAsync(string cityName)
        {
            try
            {
                var weather = await _client.GetStringAsync($"https://api.openweathermap.org/data/2.5/weather?q={cityName}&lang={LANG}&units={UNITS}&appid={APIKEY}");

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
