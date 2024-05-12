using Csla8ModelTemplates.Contracts;
using Csla8ModelTemplates.Dal;
using Csla8ModelTemplates.Dal.Oracle;
using Csla8RestApi.Dal;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Oracle.ManagedDataAccess.Client;

namespace Csla8ModelTemplates.Configuration
{
    /// <summary>
    /// Provide methods to configure Oracle databases.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Add the services to Entity Framewprk to use Oracle.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">Teh application configuration.</param>
        /// <param name="detector">The dead lock detector.</param>
        public static void AddOracleDal(
            this IServiceCollection services,
            IConfiguration? configuration = null,
            IDeadLockDetector? detector = null
            )
        {
            // Configure database.
            if (configuration is null)
            {
                configuration = ConfigurationCreator.Create();
            }
            services.AddDbContext<OracleContext>(options =>
                options.UseOracle(configuration.GetConnectionString(DAL.Oracle)!)
                );

            // Configure data access layer.
            foreach (var dalType in OracleDalIndex.Items)
                services.AddTransient(dalType.Key, dalType.Value);

            // Configure dead lock checking.
            if (detector is not null)
                detector.RegisterCheckMethod(
                    DAL.Oracle,
                    typeof(ConfigurationExtensions).GetMethod("IsOracleDeadlock")!
                    );
        }

        /// <summary>
        /// CHecks whether the reason of the exception is a deadlock.
        /// </summary>
        /// <param name="ex">The original exception thrown.</param>
        /// <returns>True when the reason is a deadlock; otherwise false;</returns>
        public static bool IsOracleDeadlock(
            Exception ex
            )
        {
            return ex is OracleException && (ex as OracleException)!.Number == 1213;
        }

        /// <summary>
        /// Runs Oracle seeders.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="isDevelopment">Indicates whether the hosting environment is development.</param>
        /// <param name="contentRootPath">The root path of the web site.</param>
        public static void RunOracleSeeders(
            this IApplicationBuilder app,
            bool isDevelopment,
            string contentRootPath
            )
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<OracleContext>();

                OracleSeeder.Run(context, isDevelopment, contentRootPath);
            }
        }
    }
}
