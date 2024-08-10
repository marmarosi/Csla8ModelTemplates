namespace Csla8ModelTemplates.WebApi.Extensions
{
    /// <summary>
    /// Provide methods to build the application configuration.
    /// </summary>
    internal static class ConfigExtensions
    {
        /// <summary>
        /// Adds shared seetings to application configuration.
        /// </summary>
        /// <param name="configuration">The configuration manager.</param>
        /// <param name="environment">The hosting environment.</param>
        public static void Add_Configuration(
            this ConfigurationManager configuration,
            IWebHostEnvironment environment
            )
        {
            configuration
                .AddJsonFile("AppSettings.json", false, true)
                .AddEnvironmentVariables();
        }
    }
}
