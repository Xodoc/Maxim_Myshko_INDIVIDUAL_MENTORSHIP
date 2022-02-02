using BL.DTOs;
using System.Collections.Generic;

namespace Tests.Fixtures
{
    public class WeatherFixture
    {
        public string Description;

        public double Temp;

        private readonly List<WeatherDTO> _data;

        public WeatherFixture()
        {
            _data = new List<WeatherDTO>
            {
                new WeatherDTO{Temp = -12, Description = "Dress warmly." },
                new WeatherDTO{Temp = 0, Description = "It's fresh." },
                new WeatherDTO{Temp = 25, Description = "Good weather." },
                new WeatherDTO{Temp = 40, Description = "It's time to go to the beach." }
            };
        }

        public List<WeatherDTO> GetWeatherDescription() 
        {                      
            return _data;
        }
    }
}
