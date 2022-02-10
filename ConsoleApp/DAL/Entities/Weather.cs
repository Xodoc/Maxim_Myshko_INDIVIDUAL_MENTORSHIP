using Newtonsoft.Json;

namespace DAL.Entities
{
    public class Weather
    {
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
