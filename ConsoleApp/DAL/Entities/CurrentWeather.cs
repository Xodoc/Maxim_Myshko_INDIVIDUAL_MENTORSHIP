using System.Collections.Generic;
using Newtonsoft.Json;

namespace DAL.Entities
{
    public class CurrentWeather
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("weather")]
        public List<Weather> Weather { get; set; }

        [JsonProperty("main")]
        public Temperature Main { get; set; }
    }
}
