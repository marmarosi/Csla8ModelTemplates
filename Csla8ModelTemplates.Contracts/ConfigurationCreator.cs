using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Csla8ModelTemplates.Contracts
{
    /// <summary>
    /// Provide methods to create the application configuration (design time).
    /// </summary>
    public static class ConfigurationCreator
    {
        /// <summary>
        /// Creates the application configuration.
        /// </summary>
        /// <returns>The application configuration.</returns>
        public static IConfiguration Create()
        {
            // Add application settings.
            var currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var apiPath = Path.Join(currentPath, "../../../../Csla8ModelTemplates.WebApi");
            var settingsPath = Path.Join(apiPath, "/AppSettings.json");

            var builder = new ConfigurationBuilder()
                .AddJsonFile(settingsPath, false, true);

            IConfiguration configuration = builder.Build();

            // Return the application configuration.
            return configuration;
        }
    }
}
