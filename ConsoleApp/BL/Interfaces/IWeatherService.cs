using BL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IWeatherService
    {
        Task<string> GetWeatherAsync(string cityName);

        Task<string> GetWeatherForecastAsync(string cityName, int days);

        Task<string> GetMaxTemperatureAsync(List<string> cityNames, bool debugInfo);
    }
}
