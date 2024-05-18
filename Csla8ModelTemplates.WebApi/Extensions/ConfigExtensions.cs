using System.Reflection;
using static Org.BouncyCastle.Math.EC.ECCurve;

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
        /// <param name="builder">Configuration builder.</param>
        public static void Add_JsonConfiguratioFile(
            this ConfigureHostBuilder builder
            )
        {
#pragma warning disable ASP0013
            builder.ConfigureAppConfiguration(config => {
                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                //var webRootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                //var basePath = Path.Join(webRootPath, "../../..");
                //var sharedSettings = Path.Join(basePath, "../Shared/SharedSettings.json");

                //config.SetBasePath(basePath);

                //config.AddJsonFile(sharedSettings, false, true);
                //config.AddJsonFile("appsettings.json", false, true);
                //config.AddJsonFile($"appsettings.{environment}.json", true, true);

                //config.AddJsonFile($"appsettings.{Environment.MachineName}.json", true, true);
                config.AddJsonFile($"appsettings.{environment}.json", true, true);

                config.AddEnvironmentVariables();
            });
#pragma warning disable ASP0013
        }

        public static void Add_ConnectionStrings(
            this ConfigurationManager configuration
        )
        {
            var dalNames = configuration.GetSection("ActiveDals").Get<List<string>>();
            foreach (var dalName in dalNames!)
            {
                configuration.AddIniFile($"./bin/Debug/net8.0/{dalName}.ini", optional: false, reloadOnChange: true);
            }
        }
    }
}
