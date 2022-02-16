using Shared.Interfaces;
using System;
using System.Threading.Tasks;

namespace ConsoleApp.Commands
{
    public class ExitCommand : ICommand
    {
        public string Title => "Exit";

        public Task Execute()
        {
            Environment.Exit(0);

            return Task.CompletedTask;
        }
    }
}
