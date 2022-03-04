using DAL.Database;
using DAL.Entities.WeatherHistoryEntities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class WeatherHistoryRepository : GenericRepository<WeatherHistory>, IWeatherHistoryRepository
    {
        private readonly IConfiguration _config;
        private readonly IWeatherRepository _weatherRepository;

        public WeatherHistoryRepository(ApplicationDbContext context, IConfiguration config,
            IWeatherRepository weatherRepository) : base(context)
        {
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

            await BulkSaveAsync(weatherHistories);
        }

        public async Task<List<WeatherHistory>> GetWeatherHistoriesAsync(string cityName, string date)
        {
            return await _context.WeatherHistories.AsNoTracking()
                .Where(x => x.CityName.ToLower() == cityName.ToLower() && x.Time.Date == DateTime.Parse(date))
                .ToListAsync();
        }
    }
}
