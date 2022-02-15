using Microsoft.Extensions.Configuration;
using Shared.Extensions;

namespace Tests.Config
{
    public class ConfigurationTest : Shared.Interfaces.IConfiguration
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

        public int Hours {get; }

        public ConfigurationTest()
        {
            var config = _configuration.GetConfigTest();

            APIKey = config["APIKey"];
            URL = config["URL"];
            Units = config["Units"];
            Lang = config["Lang"];
            URLGeo = config["URLGeo"];
            Forecast = config["Forecast"];
            MaxDays = int.Parse(config["MaxDays"]);
            MinDays = int.Parse(config["MinDays"]);
            Hours = int.Parse(config["Hours"]);
        }
    }
}
