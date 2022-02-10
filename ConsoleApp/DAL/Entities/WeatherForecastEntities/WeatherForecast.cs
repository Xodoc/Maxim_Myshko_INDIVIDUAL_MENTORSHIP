using System.Collections.Generic;

namespace DAL.Entities.WeatherForecastEntities
{   
    public class WeatherForecast
    {
        public string CityName { get; set; }

        public List<Daily> Daily { get; set; }
    }
}
