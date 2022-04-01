using Microsoft.Extensions.Configuration;
using Shared.Certificates;
using Shared.Config;
using System.Linq;

namespace Shared.Extensions
{
    public static class ConfigurationExtension
    {
        public static IConfigurationRoot PopulateConfigFromAppSettings(this IConfiguration configuration)
        {
            return new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
        }

        public static Configuration GetWebAPIConfig(this Configuration configuration)
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
            configuration.MaxWaitingTime = int.Parse(configurationRoot["MaxWaitingTime"]);
            configuration.CityNames = configurationRoot.GetSection("CityNames")
                                                       .GetChildren()
                                                       .Select(x => x.Value)
                                                       .ToArray();

            return configuration;
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
            configuration.MaxWaitingTime = int.Parse(configurationRoot["MaxWaitingTime"]);

            return configuration;
        }

        public static AuthServerConfig GetAuthServerConfig(this AuthServerConfig config) 
        {            
            config.SigningCredentials = new SigningAudienceCertificate().GetAudienceSigningKey();
            
            return config;
        }
    }
}
