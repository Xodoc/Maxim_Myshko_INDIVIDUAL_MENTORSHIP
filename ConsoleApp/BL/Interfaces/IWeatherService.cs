using BL.DTOs;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherDTO> GetWeatherAsync(string cityName);
    }
}
