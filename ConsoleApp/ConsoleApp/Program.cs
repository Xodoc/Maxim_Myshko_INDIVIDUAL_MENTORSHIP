using BL.Infrastructure;
using BL.Interfaces;
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

        private static async Task ShowWeather()
        {
            try
            {
                Console.Write("Input city name: ");
                var cityName = Console.ReadLine();

                var weather = await _weatherService.GetWeatherAsync(cityName);

                Console.WriteLine(weather);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\n");
            }
        }

        private static int Menu()
        {
            int dataInput;

            try
            {
                Console.WriteLine("Menu\n0) Exit\n1) Show weather\n");
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

            var flag = true;

            while (flag)
            {
                switch (Menu())
                {
                    case 0: flag = false; break;
                    case 1: await ShowWeather(); break;
                    default: break;
                }
            }
        }
    }
}
