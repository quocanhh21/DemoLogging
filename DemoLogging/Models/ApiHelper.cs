using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net.Http;

namespace DemoLogging.Models
{
    public class ApiHelper
    {
        public static HttpClient ApiClient { get; set; }

        public static string UrlFromConfig()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration config = builder.Build();
            string url = config.GetValue<string>("Configuration:UrlApi");
            return url.ToString();
        }
    }
}
