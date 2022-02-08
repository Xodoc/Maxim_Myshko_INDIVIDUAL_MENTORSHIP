using Shared.Interfaces;

namespace IntegrationTests.Config
{
    public class ConfigurationTest : IConfiguration
    {
        public string APIKey { get { return "040b95fb163277b9ba8832454277fa9d"; } }

        public string URL { get { return "https://api.openweathermap.org/data/2.5/weather?q="; } }

        public string Units { get { return "metric"; } }

        public string Lang { get { return "eng"; } }
    }
}
