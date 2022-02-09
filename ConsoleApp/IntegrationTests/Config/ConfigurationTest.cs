using Microsoft.Extensions.Configuration;

namespace IntegrationTests.Config
{
    public class ConfigurationTest : Shared.Interfaces.IConfiguration
    {
        public string APIKey
        {
            get
            {
                return new ConfigurationBuilder().AddJsonFile("appconfig.json").Build()["APIKey"];
            }
        }

        public string URL 
        {
            get
            {
                return new ConfigurationBuilder().AddJsonFile("appconfig.json").Build()["URL"];
            }
        }

        public string Units 
        {
            get
            {
                return new ConfigurationBuilder().AddJsonFile("appconfig.json").Build()["Units"];
            }
        }

        public string Lang 
        {
            get 
            {
                return new ConfigurationBuilder().AddJsonFile("appconfig.json").Build()["Lang"];
            }
        }
    }
}
