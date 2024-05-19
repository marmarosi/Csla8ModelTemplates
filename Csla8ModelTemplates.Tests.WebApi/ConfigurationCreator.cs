using Microsoft.Extensions.Configuration;

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
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Join(Directory.GetCurrentDirectory(), "../../.."))
                //.AddJsonFile(appSettings, true, true)
                .AddJsonFile("AppSettings.json", true, true)
                //.AddJsonFile(sharedSettings, true, true)
                .AddEnvironmentVariables();

            IConfiguration configuration = builder.Build();

            var dalNames = configuration.GetSection("ActiveDals").Get<List<string>>();
            foreach (var dalName in dalNames!)
            {
                builder.AddIniFile($"./bin/Debug/net8.0/{dalName}.ini", false, true);
            }
            return configuration;
        }
    }
}
