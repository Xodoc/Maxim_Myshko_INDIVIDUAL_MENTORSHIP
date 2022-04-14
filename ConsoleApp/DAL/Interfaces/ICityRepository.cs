using DAL.Entities.WeatherHistoryEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ICityRepository : IGenericRepository<City>
    {
        Task<List<City>> GetCitiesByCityNamesAsync(IEnumerable<string> cityNames);

        Task<List<City>> GetCitiesBySubscriptionIdAsync(int id);
    }
}
