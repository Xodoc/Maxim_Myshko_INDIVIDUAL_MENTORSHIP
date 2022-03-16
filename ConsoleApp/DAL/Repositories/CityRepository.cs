using DAL.Database;
using DAL.Entities.WeatherHistoryEntities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace DAL.Repositories
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        public CityRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<City>> GetCitiesByCityNameAsync(IEnumerable<string> cityNames)
        {
            var nameList = cityNames.ToList();

            return await _context.Cities.Where(c => nameList.Contains(c.CityName)).ToListAsync();
        }
    }
}
