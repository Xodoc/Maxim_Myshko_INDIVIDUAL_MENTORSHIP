using DAL.Entities.WeatherHistoryEntities;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;

namespace DAL.Database
{
    public class ApplicationDbContext : DbContext
    {
        private readonly StreamWriter _logStream = new StreamWriter("log.txt", true);

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<WeatherHistory> WeatherHistories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.LogTo(_logStream.WriteLine);
        }

        public override void Dispose()
        {
            base.Dispose();
            _logStream.Dispose();
        }

        public override async ValueTask DisposeAsync()
        {
            await base.DisposeAsync();
            await _logStream.DisposeAsync();
        }
    }
}
