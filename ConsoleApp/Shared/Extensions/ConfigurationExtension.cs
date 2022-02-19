using Microsoft.Extensions.Configuration;
using Shared.Config;

namespace Shared.Extensions
{
    public static class ConfigurationExtension
    {
        public static IConfigurationRoot PopulateConfigFromAppSettings(this IConfiguration configuration)
        {
            return new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
        }

        public static Configuration GetConfig(this Configuration configuration)
        {
            var config = System.Configuration.ConfigurationManager.AppSettings;

            configuration.APIKey = config["APIKey"];
            configuration.URL = config["URL"];
            configuration.Units = config["Units"];
            configuration.Lang = config["Lang"];
            configuration.URLGeo = config["URLGeo"];
            configuration.Forecast = config["Forecast"];
            configuration.MaxDays = int.Parse(config["MaxDays"]);
            configuration.MinDays = int.Parse(config["MinDays"]);
            configuration.Hours = int.Parse(config["Hours"]);
            configuration.IsDebug = bool.Parse(config["IsDebug"]);

            return configuration;
        }
    }
}
