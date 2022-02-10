using BL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IWeatherService
    {
        Task<string> GetWeatherAsync(string cityName);

        Task<List<WeatherForecastDTO>> GetWeatherForecastAsync(string cityName, int days);
    }
}
