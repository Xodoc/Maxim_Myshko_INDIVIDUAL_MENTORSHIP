using Newtonsoft.Json;
using System.Collections.Generic;

namespace DAL.Entities.WeatherForecastEntities
{   
    public class WeatherForecast
    {
        public string CityName { get; set; }

        [JsonProperty("list")]
        public List<Daily> Daily { get; set; }
    }
}
