using BL.Interfaces;
using BL.Services;
using Ninject.Modules;

namespace ConsoleApp.Util
{
    public class UtilModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IWeatherService>().To<WeatherService>();
        }
    }
}
