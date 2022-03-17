using BL.DTOs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IWeatherHistoryService
    {
        Task AddWeatherHistoryAsync(CityDTO city, CancellationToken token);

        Task<List<WeatherHistoryDTO>> GetWeatherHistoriesAsync(string cityName, DateTime from, DateTime to);
    }
}
