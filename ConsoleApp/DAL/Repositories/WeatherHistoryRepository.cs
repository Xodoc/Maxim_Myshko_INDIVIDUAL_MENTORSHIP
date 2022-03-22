using DAL.Database;
using DAL.Entities.WeatherHistoryEntities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class WeatherHistoryRepository : GenericRepository<WeatherHistory>, IWeatherHistoryRepository
    {
        public WeatherHistoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<WeatherHistory>> GetWeatherHistoriesAsync(string cityName, DateTime from, DateTime to)
        {
            return await _context.WeatherHistories.AsNoTracking()
                .Where(x => x.City.CityName.ToLower() == cityName.ToLower() 
                && x.Timestamp.Date >= from && x.Timestamp.Date <= to)
                .ToListAsync();
        }
    }
}
