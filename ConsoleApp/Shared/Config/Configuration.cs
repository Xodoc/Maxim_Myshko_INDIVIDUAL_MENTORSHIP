using Shared.Interfaces;
using System.Configuration;

namespace Shared.Config
{
    public class Configuration : IConfiguration
    {
        public string APIKey { get { return ConfigurationManager.AppSettings["APIKey"]; } }

        public string URL { get { return ConfigurationManager.AppSettings["URL"]; } }

        public string Units { get { return ConfigurationManager.AppSettings["Units"]; } }

        public string Lang { get { return ConfigurationManager.AppSettings["Lang"]; } }
        
        public string URLGeo { get { return ConfigurationManager.AppSettings["URLGeo"]; } }

        public string Forecast { get { return ConfigurationManager.AppSettings["Forecast"]; } }

        public int MaxDays { get { return int.Parse(ConfigurationManager.AppSettings["MaxDays"]); } }

        public int MinDays { get { return int.Parse(ConfigurationManager.AppSettings["MinDays"]); } }
    }
}
