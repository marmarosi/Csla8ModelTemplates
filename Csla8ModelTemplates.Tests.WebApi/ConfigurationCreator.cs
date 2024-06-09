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
            // Read base configuration.
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Join(Directory.GetCurrentDirectory(), "../../.."))
                .AddJsonFile("AppSettings.json", true, true);

            IConfiguration configuration = builder.Build();

            // Set database environment variables.
            var envConfig = new EnvironmentConfig("Environment.cfg");
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

            // Return the configuration.
            return configuration;
        }

        private class EnvironmentConfig
        {
            private readonly Dictionary<string, string> _data = new Dictionary<string, string>();

            public EnvironmentConfig(
                string path
                )
            {
                var lines = File.ReadAllLines(path);
                foreach (var line in lines)
                {
                    var values = line.Split(':');
                    _data.Add($"{values[0]}.name", $"{values[0].ToUpper()}_CONNSTR");
                    _data.Add($"{values[0]}.value", $"{values[1]}");
                }
            }

            public string GetName(
                string database
                )
            {
                return _data[$"{database}.name"];
            }

            public string GetValue(
                string database
                )
            {
                return _data[$"{database}.value"];
            }
        }
    }
}
