using System;

namespace ConsoleApp.Commands.HelperClasses
{
    public class StringSplitter
    {
        public string[] SplitNames(string cityNames)
        {
            return cityNames.Split(new string[] { " ", "," }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
