using Newtonsoft.Json;

namespace DAL.Entities.WeatherForecastEntities
{
    public class Main
    {
        [JsonProperty("temp")]
        public double Temp { get; set; }
    }
}
