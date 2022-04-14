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

        public async Task<List<City>> GetCitiesByCityNamesAsync(IEnumerable<string> cityNames)
        {
            var nameList = cityNames.ToList();

            return await _context.Cities.AsNoTracking().Where(c => nameList.Contains(c.CityName)).ToListAsync();
        }

        public async Task<List<City>> GetCitiesBySubscriptionIdAsync(int id)
        {
            return await _context.Cities.AsNoTracking()
                                        .Include(x => x.Subscriptions.Where(i => i.Id == id))
                                        .Where(x => x.Subscriptions.Count > 0)
                                        .ToListAsync();
        }
    }
}
