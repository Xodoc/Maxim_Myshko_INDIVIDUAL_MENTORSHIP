using DAL.Entities.WeatherHistoryEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Database
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ILoggerFactory loggerFactory)
            : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<WeatherHistory> WeatherHistories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseLoggerFactory(_loggerFactory);
        }
    }
}
