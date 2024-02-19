using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Csla8ModelTemplates.Contracts
{
    /// <summary>
    /// Provide methods to create the application configuration.
    /// </summary>
    public static class ConfigurationCreator
    {
        /// <summary>
        /// Creates the application configuration.
        /// </summary>
        /// <returns>The application configuration.</returns>
        public static IConfiguration Create()
        {
            var webRootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var basePath = Path.Join(webRootPath, "../../..");
            var settings = Path.Join(basePath, "../Csla8ModelTemplates.WebApi/appsettings.json");

            var builder = new ConfigurationBuilder()
                .AddJsonFile(settings, true, true);

            IConfiguration configuration = builder.Build();
            return configuration;
        }
    }
}
