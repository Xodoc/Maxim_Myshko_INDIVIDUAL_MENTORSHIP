using DAL.Interfaces;
using DAL.Repositories;
using Ninject.Modules;

namespace BL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IWeatherRepository>().To<WeatherRepository>();
        }
    }
}
