using System;

namespace ConsoleApp.Commands.HelperClasses
{
    public class StringSplit
    {
        private readonly string _cityNames;

        public StringSplit(string cityNames)
        {
            _cityNames = cityNames;
        }

        public string[] SplitNames() 
        {
            return _cityNames.Split(new string[] { " ", "," }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
