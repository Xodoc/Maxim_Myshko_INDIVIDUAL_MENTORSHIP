using DAL.Interfaces;
using Shared.Config;
using Shared.Interfaces;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IWeatherRepository _weatherRepository;
        private IConfiguration _configuration;
        public IWeatherRepository WeatherRepository
        {
            get
            {
                if (_weatherRepository == null)
                    _weatherRepository = new WeatherRepository(_configuration);
                return _weatherRepository;
            }
        }

        public IConfiguration Configuration
        {
            get
            {
                if (_configuration == null)
                    _configuration = new Configuration();
                return _configuration;
            }
        }

        public UnitOfWork(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    }
}
