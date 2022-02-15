using Shared.Extensions;
using Shared.Interfaces;
using System.Collections.Specialized;

namespace Shared.Config
{
    public class Configuration : IConfiguration
    {
        private readonly NameValueCollection _configuration;

        public string APIKey { get; set; }

        public string URL { get; set; }

        public string Units { get; set; }

        public string Lang { get; set; }

        public string URLGeo { get; set; }

        public string Forecast { get; set; }

        public int MaxDays { get; set; }

        public int MinDays { get; set; }

        public int Hours { get; set; }

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
