using Newtonsoft.Json;

namespace DAL.Entities.WeatherForecastEntities
{
    public class Geolocation
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("Lon")]
        public double Lon { get; set; }
    }
}
