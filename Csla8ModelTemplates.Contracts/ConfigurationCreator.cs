using Microsoft.Extensions.Configuration;

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
            // Read base configuration.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("AppSettings.json", false, true);

            IConfiguration configuration = builder.Build();

            // Set database environment variables.
            var envConfig = new EnvironmentConfig("../Csla8ModelTemplates.Tests.WebApi/Environment.cfg");
            var dalNames = configuration.GetSection("ActiveDals").Get<List<string>>();
            foreach (var dalName in dalNames!)
            {
                Environment.SetEnvironmentVariable(
                    envConfig.GetName(dalName),
                    envConfig.GetValue(dalName)
                    );
            }

            // Add environment variables to configuration.
            builder.AddEnvironmentVariables();
            configuration = builder.Build();

            // Return the application configuration.
            return configuration;
        }
    }
}
