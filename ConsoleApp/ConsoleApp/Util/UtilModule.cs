using BL.Interfaces;
using BL.Services;
using Ninject.Modules;
using Shared.Interfaces;
using Shared.Config;
using Shared.Extensions;

namespace ConsoleApp.Util
{
    public class UtilModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IWeatherService>().To<WeatherService>().InThreadScope();

            Bind<IConfiguration>().ToMethod(context =>
            {
                var config = new Configuration();
                config.GetConfig();

                return config;
            }).InSingletonScope();
        }
    }
}
