using Csla8ModelTemplates.Contracts;
using Csla8ModelTemplates.Dal;
using Csla8ModelTemplates.Dal.Sqlite;
using Csla8RestApi.Dal;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Csla8ModelTemplates.Configuration
{
    /// <summary>
    /// Provide methods to configure MySQL databases.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Add the services to Entity Framewprk to use MySQL.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">Teh application configuration.</param>
        /// <param name="detector">The dead lock detector.</param>
        public static void AddSqliteDal(
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
            services.AddDbContext<SqliteContext>(options =>
                options.UseSqlite(configuration.GetValue<string>("SQLITE_CONNSTR")!)
                );

            // Configure data access layer.
            foreach (var dalType in SqliteDalIndex.Items)
                services.AddTransient(dalType.Key, dalType.Value);

            // Configure dead lock checking.
            if (detector is not null)
                detector.RegisterCheckMethod(
                    DAL.MySQL,
                    typeof(ConfigurationExtensions).GetMethod("IsSqliteDeadlock")!
                    );
        }

        /// <summary>
        /// CHecks whether the reason of the exception is a deadlock.
        /// </summary>
        /// <param name="ex">The original exception thrown.</param>
        /// <returns>True when the reason is a deadlock; otherwise false;</returns>
        public static bool IsSqliteDeadlock(
            Exception ex
            )
        {
            return ex is SqliteException && ((SqliteException)ex).ErrorCode == 6; // SQLITE_LOCKED
        }

        /// <summary>
        /// Runs MySQL seeders.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="isDevelopment">Indicates whether the hosting environment is development.</param>
        /// <param name="contentRootPath">The root path of the web site.</param>
        public static async Task RunSqliteSeeders(
            this IApplicationBuilder app,
            bool isDevelopment,
            string contentRootPath
            )
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SqliteContext>();

                await SqliteSeeder.Run(context, isDevelopment, contentRootPath);
            }
        }
    }
}
