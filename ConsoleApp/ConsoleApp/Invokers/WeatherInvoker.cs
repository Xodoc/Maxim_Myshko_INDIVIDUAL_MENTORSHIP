using Shared.Interfaces;
using System.Threading.Tasks;

namespace ConsoleApp.Invokers
{
    public class WeatherInvoker
    {
        ICommand Command;

        public void SetCommand(ICommand command) 
        {
            Command = command;
        }

        public async Task GetWeather() 
        {
            await Command.Execute();
        }
    }
}
