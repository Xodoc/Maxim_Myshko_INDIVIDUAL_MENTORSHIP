using System;

namespace DAL.Entities.WeatherHistoryEntities
{
    public class WeatherHistory
    {
        public int Id { get; set; }

        public int CityId { get; set; }

        public DateTime Timestapm { get; set; }

        public double Temp { get; set; }

        public City City { get; set; }
    }
}
