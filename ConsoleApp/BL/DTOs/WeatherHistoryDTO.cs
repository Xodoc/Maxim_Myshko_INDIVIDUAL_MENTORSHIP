using System;

namespace BL.DTOs
{
    public class WeatherHistoryDTO
    {
        public string CityName { get; set; }

        public DateTime Time { get; set; }

        public double Temp { get; set; }
    }
}
