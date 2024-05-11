using Csla8ModelTemplates.Contracts;
using Csla8ModelTemplates.Dal;
using Csla8ModelTemplates.Dal.PostgreSql;
using Csla8RestApi.Dal;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Csla8ModelTemplates.Configuration
{
    /// <summary>
    /// Provide methods to configure PostgreSQL databases.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Add the services to Entity Framewprk to use PostgreSQL.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">Teh application configuration.</param>
        /// <param name="detector">The dead lock detector.</param>
        public static void AddPostgreSqlDal(
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
            services.AddDbContext<PostgreSqlContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString(DAL.PostgreSQL)!)
                );

            // Configure data access layer.
            foreach (var dalType in PostgreSqlDalIndex.Items)
                services.AddTransient(dalType.Key, dalType.Value);

            // Configure dead lock checking.
            if (detector is not null)
                detector.RegisterCheckMethod(
                    DAL.PostgreSQL,
                    typeof(ConfigurationExtensions).GetMethod("IsPostgreSqlDeadlock")!
                    );
        }

        /// <summary>
        /// CHecks whether the reason of the exception is a deadlock.
        /// </summary>
        /// <param name="ex">The original exception thrown.</param>
        /// <returns>True when the reason is a deadlock; otherwise false;</returns>
        public static bool IsPostgreSqlDeadlock(
            Exception ex
            )
        {
            return ex is PostgresException && (ex as PostgresException)!.SqlState == "40P01";
        }

        /// <summary>
        /// Runs PostgreSQL seeders.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="isDevelopment">Indicates whether the hosting environment is development.</param>
        /// <param name="contentRootPath">The root path of the web site.</param>
        public static void RunPostgreSqlSeeders(
            this IApplicationBuilder app,
            bool isDevelopment,
            string contentRootPath
            )
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<PostgreSqlContext>();

                PostgreSqlSeeder.Run(context, isDevelopment, contentRootPath);
            }
        }
    }
}
