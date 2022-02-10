using System.Collections.Generic;
using Newtonsoft.Json;

namespace DAL.Entities
{
    public class Root
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("weather")]
        public List<Weather> Weather { get; set; }

        [JsonProperty("main")]
        public Main Main { get; set; }
    }
}
