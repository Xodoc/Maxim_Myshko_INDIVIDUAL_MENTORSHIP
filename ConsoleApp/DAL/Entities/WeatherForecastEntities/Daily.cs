using System.Collections.Generic;

namespace DAL.Entities.WeatherForecastEntities
{
    public class Daily
    {
        public Temp Temp { get; set; }

        public List<Weather> Weather { get; set; }
    }
}
