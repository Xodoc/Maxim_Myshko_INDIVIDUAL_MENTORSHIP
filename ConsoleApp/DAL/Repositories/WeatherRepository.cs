using DAL.Entities;
using DAL.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        private const string UNITS = "metric";
        private const string LANG = "eng";

        private static readonly HttpClient _client = new HttpClient();

        public async Task<Root> GetWeatherAndParseAsync(string cityName)
        {
            try
            {
                var key = GetAPIKeyAsync();

                var weather = await _client.GetStringAsync($"https://api.openweathermap.org/data/2.5/weather?q={cityName}&lang={LANG}&units={UNITS}&appid={key}");

                var result = JsonConvert.DeserializeObject<Root>(weather);

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        private string GetAPIKeyAsync() 
        {
            var text = File.ReadAllText(@"C:\Users\mmyshko\Source\Repos\Maxim_Myshko_INDIVIDUAL_MENTORSHIP\ConsoleApp\DAL\Secrets.json");
            var json = JObject.Parse(text);

            var apiKey = json["credentials"].Select(x => x["APIKey"]).ToList();

            return apiKey[0].ToString();
        }
    }
}
