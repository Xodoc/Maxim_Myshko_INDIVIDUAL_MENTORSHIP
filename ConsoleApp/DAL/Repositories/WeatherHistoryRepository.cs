using DAL.Database;
using DAL.Entities.WeatherHistoryEntities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class WeatherHistoryRepository : GenericRepository<WeatherHistory>, IWeatherHistoryRepository
    {
        public WeatherHistoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
