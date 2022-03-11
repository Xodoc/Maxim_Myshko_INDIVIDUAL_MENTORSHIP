using DAL.Entities.WeatherHistoryEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ICityRepository : IGenericRepository<City>
    {
        Task<List<City>> CheckAndCreateCities(IEnumerable<string> cities);
    }
}
