using DAL.Database;
using DAL.Entities.WeatherHistoryEntities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class WeatherHistoryRepository : GenericRepository<WeatherHistory>, IWeatherHistoryRepository
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public WeatherHistoryRepository(ApplicationDbContext context, IServiceScopeFactory scopeFactory) : base(context)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task<List<WeatherHistory>> GetWeatherHistoriesAsync(string cityName, string date)
        {
            return await _context.WeatherHistories.AsNoTracking()
                .Where(x => x.City.CityName.ToLower() == cityName.ToLower() && x.Timestapm.Date == DateTime.Parse(date))
                .ToListAsync();
        }

        public async override Task<WeatherHistory> CreateAsync(WeatherHistory history)
        {
            using var scope = _scopeFactory.CreateScope();

            var db = scope.ServiceProvider.GetService<ApplicationDbContext>();

            await db.WeatherHistories.AddAsync(history);

            await db.SaveChangesAsync();

            return history;
        }
    }
}
