using Microsoft.Extensions.Configuration;

namespace IntegrationTests.Config
{
    public class ConfigurationTest : Shared.Interfaces.IConfiguration
    {
        public string APIKey { get; set; }

        public string URL { get; set; }

        public string Units { get; set; }

        public string Lang { get; set; }

        public ConfigurationTest()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appconfig.json");
            IConfiguration config = builder.Build();
            
            APIKey = config["APIString:0:APIKey"];
            URL = config["APIString:0:URL"];
            Units = config["APIString:0:Units"];
            Lang = config["APIString:0:Lang"];
        }
    }
}
