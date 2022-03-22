using Microsoft.Extensions.Configuration;
using Shared.Extensions;
using System.Linq;
using static Shared.Constants.ConfigurationConstants;

namespace Shared.Config
{
    public class ConfigurationTest : Interfaces.IConfiguration
    {
        private readonly IConfigurationRoot _configuration;

        public string APIKey { get; }

        public string URL { get; }

        public string Units { get; }

        public string Lang { get; }

        public string URLGeo { get; }

        public string Forecast { get; }

        public int MaxDays { get; }

        public int MinDays { get; }

        public int Hours { get; }

        public bool IsDebug { get; }

        public int MaxWaitingTime { get; }

        public string[] CityNames { get; }

        public string Connection { get; }

        public ConfigurationTest()
        {
            var configuration = _configuration.PopulateConfigFromAppSettings();

            APIKey = configuration["APIKey"];
            URL = configuration["URL"];
            Units = configuration["Units"];
            Lang = configuration["Lang"];
            URLGeo = configuration["URLGeo"];
            Forecast = configuration["Forecast"];
            MaxDays = int.Parse(configuration["MaxDays"]);
            MinDays = int.Parse(configuration["MinDays"]);
            Hours = int.Parse(configuration["Hours"]);
            IsDebug = bool.Parse(configuration["IsDebug"]);
            MaxWaitingTime = int.Parse(configuration["MaxWaitingTime"]);
            Connection = configuration.GetConnectionString(ConnectionString);
            CityNames = configuration.GetSection("CityNames")
                                                       .GetChildren()
                                                       .Select(x => x.Value)
                                                       .ToArray();
        }
    }
}
