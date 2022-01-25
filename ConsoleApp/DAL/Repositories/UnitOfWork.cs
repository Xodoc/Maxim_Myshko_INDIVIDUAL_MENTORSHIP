using DAL.Interfaces;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IWeatherRepository _weatherRepository;

        public IWeatherRepository WeatherRepository 
        {
            get 
            {
                if(_weatherRepository == null)
                    _weatherRepository = new WeatherRepository();
                return _weatherRepository; 
            }
        }
    }
}
