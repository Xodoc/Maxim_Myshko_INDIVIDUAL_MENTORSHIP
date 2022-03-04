using System;

namespace DAL.Entities.WeatherHistoryEntities
{
    public class WeatherHistory
    {
        public int Id { get; set; }

        public string CityName { get; set; }

        public DateTime Time { get; set; }

        public double Temp { get; set; }
    }
}
