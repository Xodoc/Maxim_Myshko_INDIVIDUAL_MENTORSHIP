using Microsoft.Extensions.Configuration;
using System.Collections.Specialized;

namespace Shared.Extensions
{
    public static class ConfigurationTests
    {
        public static IConfigurationRoot GetConfigTest(this IConfiguration configuration)
        {
            return new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
        }

        public static NameValueCollection GetConfig(this NameValueCollection collection)
        {
            return System.Configuration.ConfigurationManager.AppSettings;
        }
    }
}
