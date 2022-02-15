using Shared.Extensions;
using Shared.Interfaces;
using System.Collections.Specialized;

namespace Shared.Config
{
    public class Configuration : IConfiguration
    {
        private readonly NameValueCollection _configuration;

        public string APIKey { get; }

        public string URL { get; }

        public string Units { get; }

        public string Lang { get; }

        public string URLGeo { get; }

        public string Forecast { get; }

        public int MaxDays { get; }

        public int MinDays { get; }

        public int Hours { get; }

        public Configuration()
        {
            var configuration = _configuration.GetConfig();

            APIKey = configuration["APIKey"];
            URL = configuration["URL"];
            Units = configuration["Units"];
            Lang = configuration["Lang"];
            URLGeo = configuration["URLGeo"];
            Forecast = configuration["Forecast"];
            MaxDays = int.Parse(configuration["MaxDays"]);
            MinDays = int.Parse(configuration["MinDays"]);
            Hours = int.Parse(configuration["Hours"]);
        }
    }
}
