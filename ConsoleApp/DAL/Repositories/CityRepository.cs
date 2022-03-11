using DAL.Database;
using DAL.Entities.WeatherHistoryEntities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        public CityRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<City>> CheckAndCreateCities(IEnumerable<string> cityNames)
        {
            var cities = new List<City>();

            foreach (var name in cityNames)
            {
                var city = await _context.Cities.AsNoTracking().FirstOrDefaultAsync(x => x.CityName == name);

                if (city == null)
                {
                    var newCity = new City { CityName = name };

                    cities.Add(await CreateAsync(newCity));
                }
                else 
                {
                    cities.Add(city);
                }
            }

            return cities;
        }
    }
}
