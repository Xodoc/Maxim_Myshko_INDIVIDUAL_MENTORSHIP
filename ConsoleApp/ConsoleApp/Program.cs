using BL.Infrastructure;
using BL.Interfaces;
using ConsoleApp.Commands;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ConsoleApp
{
    public class Program
    {
        private static IWeatherService _weatherService;

        private static async Task Menu()
        {
            var getWeather = new GetWeatherCommand(_weatherService);
            var getWeatherForecast = new GetWeatherForecastCommand(_weatherService);
            var exitCommand = new ExitCommand();

            var commands = new List<ICommand>
            {
                getWeather, getWeatherForecast, exitCommand
            };

            var flag = true;

            while (flag)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("\t\t\t\t\t\tWeather Forecast");
                    foreach (var command in commands)
                    {
                        Console.WriteLine($"{commands.IndexOf(command)}) " + command.Title);
                    }

                    var input = int.Parse(Console.ReadLine());
                    Console.Clear();

                    if (input == 2)
                    {
                        flag = false; return;
                    }
                    else if (input >= 0 && input <= 1)
                    {
                        await commands[input].Execute();
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                    }
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("\t\t\t\t\tIncorrectly entered data!\n\nPlease, press any key to continue...");
                    Console.ReadKey();
                }

            }
        }

        private static void InjectNinject()
        {
            NinjectModule serviceModule = new ServiceModule();

            var kernel = new StandardKernel(serviceModule);
            kernel.Load(Assembly.GetExecutingAssembly());
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

            _weatherService = kernel.Get<IWeatherService>();
        }

        static async Task Main(string[] args)
        {
            InjectNinject();

            await Menu();
        }
    }
}
