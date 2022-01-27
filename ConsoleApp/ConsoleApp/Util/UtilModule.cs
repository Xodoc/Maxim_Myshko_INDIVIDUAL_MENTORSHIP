using BL.Interfaces;
using BL.Services;
using Ninject.Modules;
using Shared.Interfaces;
using Shared.Config;

namespace ConsoleApp.Util
{
    public class UtilModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IWeatherService>().To<WeatherService>();
            Bind<IConfiguration>().To<Configuration>().InSingletonScope();
        }
    }
}
