using DAL.Database;
using DAL.Entities.WeatherHistoryEntities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class WeatherHistoryRepository : IWeatherHistoryRepository
    {
        protected readonly ApplicationDbContext _context;
        private readonly DbSet<WeatherHistory> _dbSet;
        private readonly IConfiguration _config;
        private readonly IWeatherRepository _weatherRepository;

        public WeatherHistoryRepository(ApplicationDbContext context, IConfiguration config,
            IWeatherRepository weatherRepository)
        {
            _context = context;
            _dbSet = _context.Set<WeatherHistory>();
            _config = config;
            _weatherRepository = weatherRepository;
        }

        public async Task AddWeatherHistoryAsync(CancellationToken ct)
        {
            var weatherHistories = new List<WeatherHistory>();
            
            foreach (var name in _config.CityNames)
            {
                var weather = await _weatherRepository.GetWeatherAsync(name, ct);
                var weatherHistory = new WeatherHistory();

                weatherHistory.Time = DateTime.Now;
                weatherHistory.CityName = name;
                weatherHistory.Temp = weather.Main.Temp;

                weatherHistories.Add(weatherHistory);
            }

            await SaveWeatherHistoryAsync(weatherHistories);
        }
        private async Task SaveWeatherHistoryAsync(List<WeatherHistory> histories)
        {
            await _dbSet.AddRangeAsync(histories);
            await _context.SaveChangesAsync();
        }
    }
}
