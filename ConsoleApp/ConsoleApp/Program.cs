using BL.Infrastructure;
using BL.Interfaces;
using ConsoleApp.Commands;
using ConsoleApp.Invokers;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ConsoleApp
{
    public class Program
    {
        private static IWeatherService _weatherService;

        private static int Menu()
        {
            int dataInput;

            try
            {
                Console.WriteLine("Menu\n0) Exit\n1) Show weather\n2) Show weather forecast");
                dataInput = int.Parse(Console.ReadLine());
                Console.Clear();

                return dataInput;
            }
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("Incorrectly entered data! Please, try again.\n");
                return Menu();
            }
        }

        static async Task Main(string[] args)
        {
            NinjectModule serviceModule = new ServiceModule();

            var kernel = new StandardKernel(serviceModule);
            kernel.Load(Assembly.GetExecutingAssembly());
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

            _weatherService = kernel.Get<IWeatherService>();

            var weatherInvoker = new WeatherInvoker();

            var flag = true;

            while (flag)
            {
                switch (Menu())
                {
                    case 0: flag = false; break;

                    case 1: weatherInvoker.SetCommand(new GetWeatherCommand(_weatherService));
                            await weatherInvoker.GetWeather(); break;

                    case 2: weatherInvoker.SetCommand(new GetWeatherForecastCommand(_weatherService));
                            await weatherInvoker.GetWeather(); break;

                    default: break;
                }
            }
        }
    }
}
