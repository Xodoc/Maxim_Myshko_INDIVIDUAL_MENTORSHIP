using BL.Interfaces;
using BL.Validators;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using Ninject.Modules;

namespace BL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IWeatherRepository>().To<WeatherRepository>().InThreadScope();
            Bind<IValidator<Root>>().To<Validator<Root>>().InThreadScope();
        }
    }
}
