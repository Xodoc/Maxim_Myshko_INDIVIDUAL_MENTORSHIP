using Newtonsoft.Json;

namespace DAL.Entities
{
    public class Main
    {
        [JsonProperty("temp")]
        public double Temp { get; set; }
    }
}
