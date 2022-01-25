using BL.DTOs;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherNowDTO> GetWeatherAndParseAsync(string cityName);
    }
}
