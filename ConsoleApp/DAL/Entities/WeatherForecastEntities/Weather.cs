using Newtonsoft.Json;

namespace DAL.Entities.WeatherForecastEntities
{
    public class Weather
    {
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
