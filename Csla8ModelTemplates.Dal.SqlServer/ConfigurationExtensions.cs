using Csla8ModelTemplates.Contracts;
using Csla8ModelTemplates.Dal;
using Csla8ModelTemplates.Dal.SqlServer;
using Csla8RestApi.Dal;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Csla8ModelTemplates.Configuration
{
    /// <summary>
    /// Provide methods to configure SQL Server databases.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Add the services to Entity Framewprk to use SQL Server.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">Teh application configuration.</param>
        /// <param name="detector">The dead lock detector.</param>
        public static void AddSqlServerDal(
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
            services.AddDbContext<SqlServerContext>(options =>
                options.UseSqlServer(configuration.GetValue<string>("SQLSERVER_CONNSTR")!)
                );

            // Configure data access layer.
            foreach (var dalType in SqlServerDalIndex.Items)
                services.AddTransient(dalType.Key, dalType.Value);

            // Configure dead lock checking.
            if (detector is not null)
                detector.RegisterCheckMethod(
                    DAL.SQLServer,
                    typeof(ConfigurationExtensions).GetMethod("IsSqlServerDeadlock")!
                    );
        }

        /// <summary>
        /// CHecks whether the reason of the exception is a deadlock.
        /// </summary>
        /// <param name="ex">The original exception thrown.</param>
        /// <returns>True when the reason is a deadlock; otherwise false;</returns>
        public static bool IsSqlServerDeadlock(
            Exception ex
            )
        {
            return ex is SqlException && (ex as SqlException)!.Number == 1205;
        }

        /// <summary>
        /// Runs SQL Server seeders.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="isDevelopment">Indicates whether the hosting environment is development.</param>
        /// <param name="contentRootPath">The root path of the web site.</param>
        public static async Task RunSqlServerSeeders(
            this IApplicationBuilder app,
            bool isDevelopment,
            string contentRootPath
            )
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SqlServerContext>();

                await SqlServerSeeder.Run(context, isDevelopment, contentRootPath);
            }
        }
    }
}
