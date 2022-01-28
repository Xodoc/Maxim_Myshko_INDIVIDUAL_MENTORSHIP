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
    }
}
