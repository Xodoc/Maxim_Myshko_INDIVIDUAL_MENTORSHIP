using Newtonsoft.Json;

namespace DAL.Entities
{
    public class Temperature
    {
        [JsonProperty("temp")]
        public double Temp { get; set; }

        [JsonProperty("temp_max")]
        public double MaxTemp { get; set; }
    }
}
