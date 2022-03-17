using DAL.Entities.WeatherHistoryEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IWeatherHistoryRepository : IGenericRepository<WeatherHistory>
    {
        Task<List<WeatherHistory>> GetWeatherHistoriesAsync(string cityName, DateTime from, DateTime to);
    }
}
