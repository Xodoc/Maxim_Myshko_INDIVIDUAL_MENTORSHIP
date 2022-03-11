using System;

namespace BL.DTOs
{
    public class WeatherHistoryDTO
    {
        public int CityId { get; set; }

        public DateTime Timestamp { get; set; }

        public double Temp { get; set; }
    }
}
