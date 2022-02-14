using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DAL.Entities.WeatherForecastEntities
{
    public class Daily
    {
        [JsonProperty("dt_txt")]
        public DateTime Date { get; set; }

        public Main Main { get; set; }

        public List<Weather> Weather { get; set; }
    }
}
