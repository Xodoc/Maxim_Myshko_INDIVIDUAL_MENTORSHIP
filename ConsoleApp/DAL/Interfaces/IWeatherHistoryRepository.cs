using DAL.Entities.WeatherHistoryEntities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IWeatherHistoryRepository
    {
        Task AddWeatherHistoryAsync(CancellationToken ct);

        Task<List<WeatherHistory>> GetWeatherHistoriesAsync(string cityName, string date);
    }
}
