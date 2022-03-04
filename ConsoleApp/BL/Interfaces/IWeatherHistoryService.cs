using BL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IWeatherHistoryService
    {
        Task AddWeatherHistoryAsync();

        Task<List<WeatherHistoryDTO>> GetWeatherHistoriesAsync(string cityName, string date);
    }
}
