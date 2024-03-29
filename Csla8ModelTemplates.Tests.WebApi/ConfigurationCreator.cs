using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Csla8ModelTemplates.Tests.WebApi
{
    /// <summary>
    /// Provide methods to create the application configuration.
    /// </summary>
    internal static class ConfigurationCreator
    {
        /// <summary>
        /// Creates the application configuration.
        /// </summary>
        /// <returns>The application configuration.</returns>
        public static IConfiguration Create()
        {
            //var webRootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //var basePath = Path.Join(webRootPath, "../../..");
            //var appSettings = Path.Join(basePath, "AppSettings.json");
            //var sharedSettings = Path.Join(basePath, "../Shared/SharedSettings.json");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                //.AddJsonFile(appSettings, true, true)
                .AddJsonFile("AppSettings.json", true, true)
                //.AddJsonFile(sharedSettings, true, true)
                .AddEnvironmentVariables();

            IConfiguration configuration = builder.Build();
            return configuration;
        }
    }
}
