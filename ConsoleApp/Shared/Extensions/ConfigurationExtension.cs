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
            var configurationRoot = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            configuration.APIKey = configurationRoot["APIKey"];
            configuration.URL = configurationRoot["URL"];
            configuration.Units = configurationRoot["Units"];
            configuration.Lang = configurationRoot["Lang"];
            configuration.URLGeo = configurationRoot["URLGeo"];
            configuration.Forecast = configurationRoot["Forecast"];
            configuration.MaxDays = int.Parse(configurationRoot["MaxDays"]);
            configuration.MinDays = int.Parse(configurationRoot["MinDays"]);
            configuration.Hours = int.Parse(configurationRoot["Hours"]);
            configuration.IsDebug = bool.Parse(configurationRoot["IsDebug"]);

            return configuration;
        }
    }
}
