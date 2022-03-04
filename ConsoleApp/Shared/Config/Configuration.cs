using Shared.Interfaces;

namespace Shared.Config
{
    public class Configuration : IConfiguration
    {
        public string APIKey { get; set; }

        public string URL { get; set; }

        public string Units { get; set; }

        public string Lang { get; set; }

        public string URLGeo { get; set; }

        public string Forecast { get; set; }

        public int MaxDays { get; set; }

        public int MinDays { get; set; }

        public int Hours { get; set; }

        public bool IsDebug { get; set; }

        public int MaxWaitingTime { get; set; }

        public string URLHistory { get; set; }

        public string[] CityNames { get; set; }
    }
}
